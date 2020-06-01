using DataInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerQuizLoad : MonoBehaviour
{
    private string PlayerdataPath;
    private QuizDictionary m_QuizDictionary;

    // Start is called before the first frame update
    void Start()
    {
        PlayerdataPath = Application.persistentDataPath + "PlayerQuiz.dat";
        QuizLoad();
    }

    private void QuizLoad()
    {
        if (File.Exists(PlayerdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(PlayerdataPath, FileMode.Open);
            m_QuizDictionary = (QuizDictionary)bf.Deserialize(file);
            m_QuizDictionary.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_QuizDictionary = new QuizDictionary();
        }

        return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
