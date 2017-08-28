using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goscene : MonoBehaviour
{

    public void gomenu()
    {
        SceneManager.LoadScene("menu");
    }
    public void gooption()
    {
        SceneManager.LoadScene("option");
    }

    public void gostage(int i)
    {
        SceneManager.LoadScene("stage"+i.ToString());
    }
}

