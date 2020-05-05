using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coop : MonoBehaviour
{
	public GameObject eggPrefab;

	public void AddEgg()
	{
		Blackboard.m_Instance.SetEggCounter(Blackboard.m_Instance.GetEggCounter() + 1);
	}

	public GameObject TakeEgg()
	{
		if (Blackboard.m_Instance.GetEggCounter() > 0)
		{
			Blackboard.m_Instance.SetEggCounter(Blackboard.m_Instance.GetEggCounter() - 1);

			GameObject egg = Instantiate(eggPrefab);
			return egg;
		}
		return null;
	}
}
