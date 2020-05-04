using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
	[HideInInspector]
	public bool hasEgg;

	private void Start()
	{
		hasEgg = true;
	}
}
