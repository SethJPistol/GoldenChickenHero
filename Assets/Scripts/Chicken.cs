using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chicken : MonoBehaviour
{

    //Where to start the spherecast relative to the position of the chicken
    [SerializeField] Vector3 m_OriginOffset = new Vector3(0,0,0);

    //Position of the egg, when picked up by the chicken
    [SerializeField] Vector3 m_EggPosition = new Vector3(0, 0, 0);

    //The radius of the sphere cast
    [SerializeField] float m_Radius = 1.0f;

    //The distance of the spherecast
    [SerializeField] float m_Distance = 2.0f;


    private bool m_HasEgg = false;
    private GameObject m_AttachedEgg = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     
    }

    //--------------------------------------------DROPPING--------EGGS--------------------------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.gameObject;

        //If the trigger is a coop
        if(otherObject.tag == "Coop")
        {
            // and the player has an egg
            if(m_HasEgg)
            {
                //Not sure if this will work, NEEDS TESTING!!!
                m_AttachedEgg.transform.SetParent(null);
            }
        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {

        //-------------------------------------------------Picking---Up---Eggs--------------------------------------------------------------------
        //Hit
        RaycastHit hit;

        //If the pickup button is pressed
        if (Input.GetAxis("Pickup") > 0.0001f)
        {
            //Do a spherecast from the player outwards
            Physics.SphereCast(transform.position + m_OriginOffset,m_Radius,transform.forward, out hit, m_Distance,0);

            //Get the gameobject of object hit
            GameObject other = hit.collider.gameObject;

            //If other exists
            if (other)
            {
                //And it is a nest
                if (other.tag == "Nest")
                {

                   
                    //Get nest script
                    Nest n = other.GetComponent<Nest>();

                    //Finish section of code when nest code is completed
                    //--------------------------------------------------------------

                    //Get reference to the egg
                    m_AttachedEgg = n.TakeEgg();
                    //--------------------------------------------------------------

                    //Parent the egg to this 
                    m_AttachedEgg.transform.SetParent(transform);
                    
                    //Move the egg
                    m_AttachedEgg.transform.position = m_EggPosition;

                    //Set has egg to true
                    m_HasEgg = true;
                }
            }

        }

        //---------------------------------------------------------------------------------------------------------------------------
    }
}
