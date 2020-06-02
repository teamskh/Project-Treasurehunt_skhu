using DataInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class QuizAnswerLoad : MonoBehaviour
{
    private string ServerdataPath;
    private AnswerDictionary m_AnswerDictionary;
    // Start is called before the first frame update
    void Start()
    {
        ServerdataPath = Application.persistentDataPath + "ServerQuiz.dat";
        AnswersLoad();
    }

    private void AnswersLoad()
    {
        if (File.Exists(ServerdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(ServerdataPath, FileMode.Open);
            m_AnswerDictionary = (AnswerDictionary)bf.Deserialize(file);
            m_AnswerDictionary.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_AnswerDictionary = new AnswerDictionary();
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
