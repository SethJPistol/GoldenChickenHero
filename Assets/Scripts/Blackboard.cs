using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackboard : MonoBehaviour
{
    // Start is called before the first frame update
    public static Blackboard m_Instance = null;
    [SerializeField] GameObject m_Timer;
    [SerializeField] GameObject m_EggCounter;
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
        if(m_Timer)
        {
          m_TimerText = m_Timer.GetComponent<Text>();
        }
        if (m_EggCounter)
        {
           m_CountText = m_EggCounter.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_TimerText)
        {
            //Timer
        }
        if(m_CountText)
        {
            m_CountText.text = "Eggs Total: " +  m_EggCount.ToString();
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
