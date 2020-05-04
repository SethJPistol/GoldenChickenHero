using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour
{
	private static Collider m_bounds = null;

	private void Start()
	{
		if (!m_bounds)
			m_bounds = GetComponent<Collider>();
	}

	public static bool InsideLevel(Vector3 point)
	{
		return m_bounds.bounds.Contains(point);
	}
}
