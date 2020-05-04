using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
	public GameObject eggPrefab;

	[HideInInspector]
	public bool hasEgg;

	private GameObject m_egg;

	private void Start()
	{
		MakeEgg();
	}

	private void MakeEgg()
	{
		hasEgg = true;
		m_egg = Instantiate(eggPrefab);
		m_egg.transform.position = transform.position + new Vector3(0.0f, transform.localScale.y + 0.025f, 0.0f);
		m_egg.transform.SetParent(transform);
	}

	public GameObject TakeEgg()
	{
		hasEgg = false;
		return m_egg;
	}
}
