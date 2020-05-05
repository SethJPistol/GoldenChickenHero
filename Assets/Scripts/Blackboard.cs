using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackboard : MonoBehaviour
{
    // Start is called before the first frame update
    public static Blackboard m_Instance = null;
    [SerializeField] GameObject m_TimerObject;
    [SerializeField] GameObject m_EggCountObject;
    [SerializeField] GameObject m_EndGameUI;
    [SerializeField] GameObject m_EndCountText;
    [SerializeField] float m_TimeLeftMinutes = 2;
    float m_TimeLeft = 0;
    float m_Timer = 0;
    
    Text m_TimerText;
    Text m_CountText;
    private int m_EggCount = 0;
    private void Awake()
    {
        if(!m_Instance)
        {
            m_Instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        if (m_TimerObject)
        {
          m_TimerText = m_TimerObject.GetComponent<Text>();
        }
        if (m_EggCountObject)
        {
           m_CountText = m_EggCountObject.GetComponent<Text>();
        }
        m_TimeLeft = m_TimeLeftMinutes * 60;
        m_EndGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Add to the timer
        m_Timer += Time.deltaTime;

       //If the text script for the counter exists
        if(m_CountText)
        {
            //Set the text
            m_CountText.text = "Eggs Total: " +  m_EggCount.ToString();
        }
        //If it has been one second
        if(m_Timer >= 1.0f)
        {
            //reset the timer
            m_Timer = 0.0f;

            //remove from the time left 
            m_TimeLeft -= 1;
        }

        //If the text script for the timer exists
        if (m_TimerText)
        {
            //Get the number of minutes left by dividing by 60 and cutting off the decimal eg. 156 / 60 = 2.6, cutt off decimal, = 2
            int minutes = Mathf.FloorToInt(m_TimeLeft / 60);

            //Get the number of seconds by using the modulus operator. Modulus gets the remaining decimal after dividing, so really its m_TimeLeft / 60, but it returns the remainder instead
            float seconds = (m_TimeLeft % 60);
           
            //If the time is two digits, display normally
            if(seconds > 9)
            {
                m_TimerText.text = minutes + ":" + seconds;

            }
            else //Else add a 0, so instead of 2:1, its 2:01, which is what is expected
            {
                m_TimerText.text = minutes + ":" + "0" + seconds;
            }
        }

        if(m_TimeLeft <= 0)
        {
            m_EndGameUI.SetActive(true);
            Text t = m_EndCountText.GetComponent<Text>();
            t.text = "Eggs Protected: \r\n" + m_EggCount;
            Time.timeScale = 0;
        }
    }


   public int GetEggCounter()
    {
        return m_EggCount;
    }

    public void SetEggCounter(int count)
    {
        m_EggCount = count;
    }
}
