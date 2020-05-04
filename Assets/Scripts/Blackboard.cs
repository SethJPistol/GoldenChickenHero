using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    // Start is called before the first frame update
    public static Blackboard m_Instance = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
