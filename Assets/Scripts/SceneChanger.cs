using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneIndex);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Link(string url)
    {
        Application.OpenURL(url);
    }
}
