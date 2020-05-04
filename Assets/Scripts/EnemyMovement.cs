using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	private Transform m_seekTarget;
	private NavMeshAgent m_agent;
	private Nest[] m_nests;

	private GameObject m_egg = null;

    void Start()
    {
		m_agent = GetComponent<NavMeshAgent>();

		GameObject[] nestObjects = GameObject.FindGameObjectsWithTag("Nest");
		m_nests = new Nest[nestObjects.Length];
		for (int i = 0; i < nestObjects.Length; ++i)
			m_nests[i] = nestObjects[i].GetComponent<Nest>();
	}

    void Update()
    {
        if (!m_agent.hasPath)
		{
			//If inside the level,
			if (LevelBounds.InsideLevel(transform.position))
			{
				Nest closestNest = ClosestNest();

				//If at a nest,
				if (Vector3.Distance(closestNest.transform.position, transform.position) < 1.0f)
				{
					m_egg = closestNest.TakeEgg();
					if (m_egg != null)
						m_agent.SetDestination(FleePosition());
				}
				//If not at a nest
				else
					m_agent.SetDestination(closestNest.transform.position);
			}
			//If outside the level,
			else
			{
				//If have an egg,
				if (m_egg != null)
				{
					//hehehehe stole the egg
					gameObject.SetActive(false);
				}
				//If don't have an egg
				else
				{
					m_agent.SetDestination(ClosestNest().transform.position);
				}
			}
		}
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
		Vector3 directionFromCenter = transform.position.normalized;
		return (directionFromCenter * 30);
	}
}
