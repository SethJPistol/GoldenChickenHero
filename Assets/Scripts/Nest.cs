using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
	public GameObject eggPrefab;

	public AudioSource eggSpawnSound;
	public AudioSource pickupEggSound;

	[HideInInspector]
	public bool hasEgg;

	private GameObject m_egg;

	private void Start()
	{
		MakeEgg(false);
	}

	public void MakeEgg(bool playSound)
	{
		hasEgg = true;
		m_egg = Instantiate(eggPrefab);
		m_egg.transform.position = transform.position + new Vector3(0.0f, transform.localScale.y + 0.025f, 0.0f);
		m_egg.transform.SetParent(transform);

		if (playSound)
			eggSpawnSound.Play();
	}

	public GameObject TakeEgg()
	{
		if (hasEgg)
		{
			hasEgg = false;
			GameObject egg = m_egg;
			m_egg = null;

			pickupEggSound.Play();

			return egg;
		}
		return null;
	}
}
