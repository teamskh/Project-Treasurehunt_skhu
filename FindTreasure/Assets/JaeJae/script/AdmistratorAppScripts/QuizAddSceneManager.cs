using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizAddSceneManager : MonoBehaviour
{
    public GameObject OXQuizP, MultiCQuizP, ShortAnswerQuizP, ConnectedQuizP, PlacementQuizP;
    public GameObject TakePicture_b;

    public void ChangeSceneToOXQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(true);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
        TakePicture_b.SetActive(false);
    }
    
    public void ChangeSceneToMultiCQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(true);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
        TakePicture_b.SetActive(false);
    }
    
    public void ChangeSceneToShortAnswerQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(true);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(false);
        TakePicture_b.SetActive(false);
    }

    public void ChangeSceneToConnectedQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(true);
        PlacementQuizP.SetActive(false);
        TakePicture_b.SetActive(false);
    }

    public void ChangeSceneToPlacementQuiz()
    {
        SceneManager.LoadScene("QuizAdd");//Quiz추가화면 OX
        OXQuizP.SetActive(false);
        MultiCQuizP.SetActive(false);
        ShortAnswerQuizP.SetActive(false);
        ConnectedQuizP.SetActive(false);
        PlacementQuizP.SetActive(true);
        TakePicture_b.SetActive(true);
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
