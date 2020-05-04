using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform objectToFollow;

	private Vector3 m_cameraOffset;

	private void Start()
	{
		m_cameraOffset = transform.position - objectToFollow.position;
	}

	private void Update()
	{
		transform.position = objectToFollow.position + m_cameraOffset;
	}
}
