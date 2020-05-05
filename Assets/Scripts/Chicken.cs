using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Chicken : MonoBehaviour
{


    //Position of the egg, when picked up by the chicken
    [SerializeField] Transform m_HoldTransform = null;

    //The radius of the sphere cast
    [SerializeField] float m_Radius = 1.0f;

    [SerializeField] KeyCode m_PickupButton = KeyCode.Space;
    [SerializeField] KeyCode m_AttackButton = KeyCode.Mouse0;

	public AudioSource randomCluckSound;
	private float m_cluckTimer;
	public AudioSource peckSound;

    private bool m_HasEgg = false;
    private GameObject m_AttachedEgg = null;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();
        if(r)
        {
            r.freezeRotation = true;
        }

		m_cluckTimer = 5.0f;
	}

    // Update is called once per frame
    void Update()
    {
		if (m_cluckTimer > 0.0f)
			m_cluckTimer -= Time.deltaTime;
		else
		{
			randomCluckSound.pitch = Random.Range(0.8f, 1.2f);
			randomCluckSound.Play();
			m_cluckTimer = 5.0f;
		}
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
                if(m_AttachedEgg)
                {
                    //Not sure if this will work, NEEDS TESTING!!!
                    m_AttachedEgg.transform.SetParent(null);

                    //Add to the counter on the blackboard
                    Blackboard.m_Instance.SetEggCounter(Blackboard.m_Instance.GetEggCounter() + 1);
                    m_HasEgg = false;

                }
                else
                {
                    Debug.Log("Attached Egg reference is null");
                }

            }

        }
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {

        //-------------------------------------------------Picking---Up---Eggs--------------------------------------------------------------------

        //If the pickup button is pressed
        if (Input.GetKeyDown(m_PickupButton))
        {
            if (!m_HasEgg)
            {
                //Do a spherecast from the player outwards
                //Physics.SphereCast(transform.position + m_OriginOffset,m_Radius,transform.forward, out hit, m_Distance);
                Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, m_Radius);
                for (int i = 0; i < colliders.Length; i++)
                {
                    //If other exists
                    if (colliders[i])
                    {
                        //Get the gameobject of object hit
                        GameObject other = colliders[i].gameObject;
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

                            if (m_AttachedEgg)
                            {
                                m_AttachedEgg.transform.position = m_HoldTransform.position;

                                //Parent the egg to this 
                                m_AttachedEgg.transform.SetParent(m_HoldTransform);
                            }




                            //Set has egg to true
                            m_HasEgg = true;
                        }
                    }
                }
            }



        }

        //---------------------------------------------------------------------------------------------------------------------------

        //Scare
        //---------------------------------------------------------------------------------------------------------------------------
        if (Input.GetKeyDown(m_AttackButton))
        {
            if (!m_HasEgg)
            {
                //Do a spherecast
                // Physics.SphereCast(transform.position + m_OriginOffset, m_Radius, transform.forward, out hit, m_Distance);
                // Debug.DrawRay(transform.position + m_OriginOffset, transform.forward * m_Distance, Color.blue,10);

                Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward, m_Radius);
                for (int i = 0; i < colliders.Length; i++)
                {
                    //If the collider exist (meaning it hit something)
                    if (colliders[i])
                    {
                        GameObject other = colliders[i].gameObject;

                        if (other.tag == "Farmer")
                        {
                            //Call Scare function
                            m_AttachedEgg = other.GetComponent<EnemyMovement>().Scare();

                            //If the scare() didn't return null
                            if(m_AttachedEgg)
                            {
                                m_AttachedEgg.transform.position = m_HoldTransform.position;
                                m_AttachedEgg.transform.SetParent(m_HoldTransform);
                                m_HasEgg = true;
                            }
                           
                            //Debug.Log("Stole egg");
                        }

                    }
                }


				//Regardless of whether hit anything or not, play peck sound
				peckSound.Play();
				m_cluckTimer = 5.0f;
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + transform.forward,m_Radius);
    }
}
