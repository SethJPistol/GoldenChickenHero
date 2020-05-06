using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public float eggSpawnTime = 5.0f;
	public float spawnTimeVariation = 2.5f;
	private float m_eggSpawnTimer;
	private Nest[] m_nests;

	private void Start()
	{
		GameObject[] nestObjects = GameObject.FindGameObjectsWithTag("Nest");
		m_nests = new Nest[nestObjects.Length];
		for (int i = 0; i < nestObjects.Length; ++i)
			m_nests[i] = nestObjects[i].GetComponent<Nest>();

		SetSpawnTimer();
	}

	private void Update()
	{
		if (m_eggSpawnTimer > 0.0f)
			m_eggSpawnTimer -= Time.deltaTime;
		else
		{
			SpawnEgg();
			SetSpawnTimer();
		}
	}

	private void SetSpawnTimer()
	{
		m_eggSpawnTimer = eggSpawnTime + Random.Range(-spawnTimeVariation, spawnTimeVariation);
	}

	private void SpawnEgg()
	{
		bool[] nestChecked = new bool[m_nests.Length];
		for (int i = 0; i < m_nests.Length; ++i)
		{
			int randomNest = Random.Range(0, m_nests.Length);
			if (!nestChecked[randomNest])
			{
				if (!m_nests[randomNest].hasEgg)
				{
					m_nests[randomNest].MakeEgg(true);
					return;
				}
				nestChecked[randomNest] = true;
			}
			else
				--i;
		}
	}
}
