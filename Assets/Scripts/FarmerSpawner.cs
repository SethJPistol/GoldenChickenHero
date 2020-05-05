using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerSpawner : MonoBehaviour
{
	public GameObject farmerPrefab;

	public float farmerSpawnTime = 5.0f;
	public int maxFarmerAmount = 3;
	private float m_farmerSpawnTimer;

	private float m_levelXMax;
	private float m_levelZMax;

	private void Start()
	{
		m_farmerSpawnTimer = farmerSpawnTime;

		m_levelXMax = LevelBounds.LevelWidth() * 0.5f;
		m_levelZMax = LevelBounds.LevelHeight() * 0.5f;

		SpawnFarmer();
	}

	private void Update()
	{
		if (m_farmerSpawnTimer > 0.0f)
			m_farmerSpawnTimer -= Time.deltaTime;
		else
		{
			GameObject[] farmers = GameObject.FindGameObjectsWithTag("Farmer");
			if (farmers.Length < maxFarmerAmount)
				SpawnFarmer();

			m_farmerSpawnTimer = farmerSpawnTime;
		}
	}

	private void SpawnFarmer()
	{
		Vector3 spawnpoint = Vector3.zero;
		int side = Random.Range(0, 4);
		switch (side)
		{
			case 0: //Front
				spawnpoint = new Vector3(Random.Range(-m_levelXMax, m_levelXMax), 0.0f, -m_levelZMax);
				break;

			case 1: //Back
				spawnpoint = new Vector3(Random.Range(-m_levelXMax, m_levelXMax), 0.0f, m_levelZMax);
				break;

			case 2: //Left
				spawnpoint = new Vector3(-m_levelXMax, 0.0f, Random.Range(-m_levelZMax, m_levelZMax));
				break;

			case 3: //Right
				spawnpoint = new Vector3(m_levelXMax, 0.0f, Random.Range(-m_levelZMax, m_levelZMax));
				break;

			default:
				Debug.Log("Didn't find a side to spawn on");
				return;
		}

		GameObject farmer = Instantiate(farmerPrefab);
		farmer.transform.position = spawnpoint;
	}
}
