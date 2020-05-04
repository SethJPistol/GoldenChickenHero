using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
	private Transform m_seekTarget;
	private NavMeshAgent m_agent;
	private Nest[] m_nests;

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
			//If not at a nest
			Nest closestNest = ClosestNest();
			m_agent.SetDestination(closestNest.transform.position);
			//If at the nest
		}
    }

	private Nest ClosestNest()
	{
		bool first = true;
		Nest closestNest = null;
		float closestDistance = 0;

		foreach (Nest nest in m_nests)
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

		return closestNest;
	}
}
