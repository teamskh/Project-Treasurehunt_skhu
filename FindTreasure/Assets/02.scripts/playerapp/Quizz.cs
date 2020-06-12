using DataInfo;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Quizz : MonoBehaviour
{
    public TextMesh qu; //문제 출력
    //public string t; //저장된 aa 테스트하기 위해서 aa 입력
    private QuizDictionary m_Dics;

    // Start is called before the first frame update
    void Start()
    {
        string LoadPath = Application.persistentDataPath + "PlayerQuiz.dat";

        PlayerQuizLoad.initLoad(LoadPath, out m_Dics);

        PlayerQuizLoad script = GetComponent<PlayerQuizLoad>();
        if (script != null)
        {
            Quiz current = m_Dics.FindQuiz(gameman.Instance.imageText);
            //Quiz current = GetComponent<PlayerQuizLoad>().m_QuizDictionary.FindQuiz(t);
            if (current != null)
                qu.text = current.str;
        }
        else
        {
            Debug.Log("Can't find scripts");
        }
    }

}
