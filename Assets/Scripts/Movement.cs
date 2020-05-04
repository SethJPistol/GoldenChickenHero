using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    [SerializeField] private float m_Speed = 0;
    [SerializeField] private ForceMode m_ForceMode = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody 
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Find the direction by using the Unity input system
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));

        //magnitude can't be negative, use 0.0001 to hopefully avoid any floating point error stuff
        if(direction.magnitude > 0.0001f)
        {
            m_Rigidbody.AddForce(direction * m_Speed *Time.fixedDeltaTime,m_ForceMode);
            m_Rigidbody.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
