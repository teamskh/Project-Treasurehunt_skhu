using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    void Update()

    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                Application.LoadLevel("ContestList");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            Application.LoadLevel("ContestList");
        }
    }*/
    public void BackToContestList()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("ContestList");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            SceneManager.LoadScene("ContestList");
        }
    }
    /*
    public void BackToadministratorMenu()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("administratorMenu");
            }
        }
    }*/
    public void BackToQuizType()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("QuizType");
            }
        }
    }
    public void BackToQuizMenu()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("QuizMenu");
            }
        }
    }
    public void BackToContestMenu()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("ContestMenu");
            }
        }
    }
}
