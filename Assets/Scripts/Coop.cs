using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coop : MonoBehaviour
{
	public GameObject eggPrefab;
	public Transform coopTop;
	private List<GameObject> m_eggs;

	private void Start()
	{
		m_eggs = new List<GameObject>(10);
	}

	public void AddEgg(GameObject egg)
	{
		Blackboard.m_Instance.SetEggCounter(Blackboard.m_Instance.GetEggCounter() + 1);
		egg.transform.position = coopTop.position + (egg.transform.position - coopTop.position).normalized * 0.5f;
		egg.transform.SetParent(null);
		egg.GetComponent<Rigidbody>().isKinematic = false;
		m_eggs.Add(egg);
	}

	public GameObject TakeEgg()
	{
		if (Blackboard.m_Instance.GetEggCounter() > 0)
		{
			Blackboard.m_Instance.SetEggCounter(Blackboard.m_Instance.GetEggCounter() - 1);
			int eggIndex = m_eggs.Count - 1;
			if (m_eggs[eggIndex] != null)
			{
				m_eggs[eggIndex].GetComponent<Rigidbody>().isKinematic = true;
				m_eggs[eggIndex].transform.rotation = Quaternion.identity;
				GameObject egg = m_eggs[eggIndex];
				m_eggs.RemoveAt(eggIndex);
				return egg;
			}
		}
		return null;
	}
}
