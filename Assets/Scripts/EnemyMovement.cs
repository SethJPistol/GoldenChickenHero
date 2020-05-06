using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	//public Transform handPosition;

	public AudioSource eggPickupSound;
	public AudioSource whistling;

	private NavMeshAgent m_agent;
	private Nest[] m_nests;
	private Coop m_coop;
	private GameObject[] m_exits;

	private GameObject m_egg = null;
	private bool m_scared = false;

	private float m_pickupDistance = 4.0f;

	[SerializeField] private ParticleSystem m_Exclamation;
    void Start()
    {
		m_agent = GetComponent<NavMeshAgent>();

		GameObject[] nestObjects = GameObject.FindGameObjectsWithTag("Nest");
		m_nests = new Nest[nestObjects.Length];
		for (int i = 0; i < nestObjects.Length; ++i)
			m_nests[i] = nestObjects[i].GetComponent<Nest>();

		m_coop = GameObject.FindGameObjectWithTag("Coop").GetComponent<Coop>();

		m_exits = GameObject.FindGameObjectsWithTag("Exit");
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
									m_egg.SetActive(false);

									eggPickupSound.Play();
								}
								else
									m_agent.SetDestination(NestPosition(closestNest.transform));
							}
							//If not at the nest
							else
								m_agent.SetDestination(NestPosition(closestNest.transform));
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
									m_egg.SetActive(false);

									eggPickupSound.Play();
								}
								else
									m_agent.SetDestination(NestPosition(m_coop.transform));
							}
							//If not at the coop,
							else
								m_agent.SetDestination(NestPosition(m_coop.transform));
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
								m_egg.SetActive(false);

								eggPickupSound.Play();
							}
							else
								m_agent.SetDestination(NestPosition(m_coop.transform));
						}
						//If not at the coop,
						else
							m_agent.SetDestination(NestPosition(m_coop.transform));
					}
				}
				//If scared or have an egg,
				else
				{
					m_agent.SetDestination(FleePosition());
				}
			}
			//If outside the level,
			else
			{
				if (m_scared)
				{
					//aaaaaa
					Destroy(gameObject);
				}
				//If have an egg,
				else if (m_egg != null)
				{
					//hehehehe stole the egg
					Destroy(gameObject);
				}
				//If don't have an egg
				else
				{
					//go on the hunt
					Nest closestNest = ClosestNest();
					if (closestNest != null)
					{
						bool closerThanCoop = Vector3.Distance(closestNest.transform.position, transform.position) < Vector3.Distance(m_coop.transform.position, transform.position);

						if (closerThanCoop)
							m_agent.SetDestination(NestPosition(closestNest.transform));
						else
							m_agent.SetDestination(NestPosition(m_coop.transform));
					}
					else
						m_agent.SetDestination(NestPosition(m_coop.transform));
				}
			}
		}
    }

	public GameObject Scare()
	{
		if(m_Exclamation)
		{
			m_Exclamation.Play();
		}
		m_agent.SetDestination(FleePosition());
		m_scared = true;

		whistling.Stop();

		if (m_egg != null)
		{
			m_egg.SetActive(true);
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
	private Vector3 NestPosition(Transform nestTransform)
	{
		Vector3 directionToFarmer = (transform.position - nestTransform.position).normalized;
		return (nestTransform.position + directionToFarmer);	//Move the seek position a little close to farmer
	}
	private Vector3 FleePosition()
	{
		//Vector3 directionFromCenter = new Vector3(transform.position.x, 0.0f, transform.position.z);
		//directionFromCenter.Normalize();
		//return (directionFromCenter * 20);

		//int exit = Random.Range(0, m_exits.Length);

		bool first = true;
		GameObject closestExit = null;
		float closestDistance = 0;
		foreach (GameObject exit in m_exits)
		{
			float distance = Vector3.Distance(exit.transform.position, transform.position);

			if (first)
			{
				closestDistance = distance;
				closestExit = exit;
				first = false;
			}
			else if (distance < closestDistance)
			{
				closestDistance = distance;
				closestExit = exit;
			}
		}

		return closestExit.transform.position;
	}
}
