using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizAddSceneManager : MonoBehaviour
{
    public static int ButtonClick=-1;
    
    public void ChangeSceneToOXQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        ButtonClick = 0;
    }
    
    public void ChangeSceneToMultiCQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        ButtonClick = 1;
    }
    
    public void ChangeSceneToShortAnswerQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        ButtonClick = 2;
    }

    public void ChangeSceneToConnectedQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        //ButtonClick = 3;
    }

    public void ChangeSceneToPlacementQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        //ButtonClick = 4;
}
    void Update()

    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("QuizMenu");
            }
        }
    }
}
