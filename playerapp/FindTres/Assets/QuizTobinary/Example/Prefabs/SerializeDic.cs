using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SerializeDic : MonoBehaviour
{
    [SerializeField]
    TitleQuizDictionary m_titleQuiz;

    public Answer ans;
    public string title;
    private string dataPath;

    public void Initialized()
    {
        dataPath = Application.persistentDataPath + "/gameData.dat";
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(dataPath);
        TitleQuizDictionary data = new TitleQuizDictionary();
        data.CopyFrom(m_titleQuiz);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }

    private void Load()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(dataPath, FileMode.Open);
            m_titleQuiz = (TitleQuizDictionary)bf.Deserialize(file);
            m_titleQuiz.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_titleQuiz = new TitleQuizDictionary();
        }

        return;
    }

    public IDictionary<string,Answer> TitleQuizDictionary
    {
        get { return m_titleQuiz; }
        set { m_titleQuiz.CopyFrom(value); }
    }

    public void setTitleQuiz()
    {
        m_titleQuiz.Add(title, ans);
        ans = new Answer();

    }
    private void Start()
    {
        //ans = new Answer();
        Initialized();
        Load();
        
    }
}
