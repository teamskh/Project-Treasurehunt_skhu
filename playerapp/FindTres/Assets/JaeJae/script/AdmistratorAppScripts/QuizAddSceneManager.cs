using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizAddSceneManager : MonoBehaviour
{
    public GameObject OXQuizP, MultiCQuizP, ShortAnswerQuizP, ConnectedQuizP, PlacementQuizP;
    
    public void ChangeSceneToOXQuiz()
    {
        Application.LoadLevel("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(true);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
    }
    
    public void ChangeSceneToMultiCQuiz()
    {
        Application.LoadLevel("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(true);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
    }
    
    public void ChangeSceneToShortAnswerQuiz()
    {
        Application.LoadLevel("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(true);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
    }

    public void ChangeSceneToConnectedQuiz()
    {
        Application.LoadLevel("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(true);
        PlacementQuizP.SetActive(false);
    }

    public void ChangeSceneToPlacementQuiz()
    {
        Application.LoadLevel("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(true);
    }
    void Update()

    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                Application.LoadLevel("QuizMenu");
            }
        }
    }
}
