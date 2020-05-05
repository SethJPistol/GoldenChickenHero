using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	public Transform handPosition;

	private NavMeshAgent m_agent;
	private Nest[] m_nests;
	private Coop m_coop;

	private GameObject m_egg = null;
	private bool m_scared = false;

	private float m_pickupDistance = 4.0f;

    void Start()
    {
		m_agent = GetComponent<NavMeshAgent>();

		GameObject[] nestObjects = GameObject.FindGameObjectsWithTag("Nest");
		m_nests = new Nest[nestObjects.Length];
		for (int i = 0; i < nestObjects.Length; ++i)
			m_nests[i] = nestObjects[i].GetComponent<Nest>();

		m_coop = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>();
	}

    void Update()
    {
        if (!m_agent.hasPath)
		{
			//If inside the level,
			if (LevelBounds.InsideLevel(transform.position))
			{
				if (!m_scared && !m_egg)
				{
					Nest closestNest = ClosestNest();

					//If there is a nest with an egg
					if (closestNest != null)
					{
						bool closerThanCoop = Vector3.Distance(closestNest.transform.position, transform.position) < Vector3.Distance(m_coop.transform.position, transform.position);

						if (closerThanCoop)
						{
							//If at the nest,
							if (Vector3.Distance(closestNest.transform.position, transform.position) < m_pickupDistance)
							{
								m_egg = closestNest.TakeEgg();
								if (m_egg != null)
								{
									m_agent.SetDestination(FleePosition());
									m_egg.transform.position = handPosition.position;
									m_egg.transform.SetParent(handPosition);
								}
								else
									m_agent.SetDestination(closestNest.transform.position);
							}
							//If not at the nest
							else
								m_agent.SetDestination(closestNest.transform.position);
						}
						//If the coop is closer,
						else
						{
							//If at the coop,
							if (Vector3.Distance(m_coop.transform.position, transform.position) < m_pickupDistance)
							{
								m_egg = m_coop.TakeEgg();
								if (m_egg != null)
								{
									m_agent.SetDestination(FleePosition());
									m_egg.transform.position = handPosition.position;
									m_egg.transform.SetParent(handPosition);
								}
								else
									m_agent.SetDestination(m_coop.transform.position);
							}
							//If not at the coop,
							else
								m_agent.SetDestination(m_coop.transform.position);
						}
					}
					//If there is no nest with an egg,
					else
					{
						//If at the coop,
						if (Vector3.Distance(m_coop.transform.position, transform.position) < m_pickupDistance)
						{
							m_egg = m_coop.TakeEgg();
							if (m_egg != null)
							{
								m_agent.SetDestination(FleePosition());
								m_egg.transform.position = handPosition.position;
								m_egg.transform.SetParent(handPosition);
							}
							else
								m_agent.SetDestination(m_coop.transform.position);
						}
						//If not at the coop,
						else
							m_agent.SetDestination(m_coop.transform.position);
					}
				}
			}
			//If outside the level,
			else
			{
				if (m_scared)
				{
					//aaaaaa
					gameObject.SetActive(false);
				}
				//If have an egg,
				else if (m_egg != null)
				{
					//hehehehe stole the egg
					gameObject.SetActive(false);
				}
				//If don't have an egg
				else
				{
					//go on the hunt
					Nest closestNest = ClosestNest();
					if (closestNest != null)
						m_agent.SetDestination(closestNest.transform.position);
				}
			}
		}
    }

	public GameObject Scare()
	{
		m_agent.SetDestination(FleePosition());
		m_scared = true;

		if (m_egg != null)
		{
			GameObject egg = m_egg;
			m_egg = null;
			return egg;
		}
		return null;
	}

	private Nest ClosestNest()
	{
		bool first = true;
		Nest closestNest = null;
		float closestDistance = 0;

		foreach (Nest nest in m_nests)
		{
			if (nest.hasEgg)
			{
				float distance = Vector3.Distance(nest.transform.position, transform.position);

				if (first)
				{
					closestDistance = distance;
					closestNest = nest;
					first = false;
				}
				else if (distance < closestDistance)
				{
					closestDistance = distance;
					closestNest = nest;
				}
			}
		}

		return closestNest;
	}
	private Vector3 FleePosition()
	{
		Vector3 directionFromCenter = new Vector3(transform.position.x, 0.0f, transform.position.z);
		directionFromCenter.Normalize();
		return (directionFromCenter * 30);
	}
}
