using DataInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerQuizLoad : MonoBehaviour
{
    private string PlayerdataPath;
    public QuizDictionary m_QuizDictionary;


    // Start is called before the first frame update
    void Start()
    {
        PlayerdataPath = Application.persistentDataPath + "PlayerQuiz.dat";
        //QuizLoad();
        initLoad(PlayerdataPath, out m_QuizDictionary);
        CheckKeys();
    }

    static public void initLoad(string path, out QuizDictionary Qdic)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);
            Qdic = (QuizDictionary)bf.Deserialize(file);
            Qdic.OnAfterDeserialize();
            file.Close();
            
        }
        else
        {
            Debug.Log("File Does't Exist");
            Qdic = new QuizDictionary();
        }

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
            Debug.Log("File Does't Exist");
            m_QuizDictionary = new QuizDictionary();
        }

        return;
    }

    private void CheckKeys()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
