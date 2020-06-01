using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//저장용 변수 저장-로드용 함수 및 클래스

public class SerializeDic : MonoBehaviour
{
    [SerializeField]
    QuizInfoDictionary m_titleQuiz;

    [SerializeField]
    ContestDictionary m_Contest;

    public QuizInfo ans;
    public string title;
    private string QuizdataPath;
    private string ContestdataPath;

    public void Initialized()
    {
        QuizdataPath = Application.persistentDataPath + "Quiz.dat";
        ContestdataPath = Application.persistentDataPath + "Contest.dat";
    }

    //퀴즈 dictionary 변수 저장용 함수
    public void QuizSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(QuizdataPath);
        QuizInfoDictionary data = new QuizInfoDictionary();
        data.CopyFrom(m_titleQuiz);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }

    //대회 dictionary 변수 저장용 함수 -> 대회 리스트를 얻는 함수는 userQuiz.cs 파일 내에 구현
    public void ContestSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(ContestdataPath);
        ContestDictionary data = new ContestDictionary();
        data.CopyFrom(m_Contest);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();
    }

    //퀴즈 dictionary 변수 로드용 함수
    private void QuizLoad()
    {
        if (File.Exists(QuizdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(QuizdataPath, FileMode.Open);
            m_titleQuiz = (QuizInfoDictionary)bf.Deserialize(file);
            m_titleQuiz.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_titleQuiz = new QuizInfoDictionary();
        }

        return;
    }

    // 대회 로드용 함수
    private void ContestLoad()
    {
        if (File.Exists(ContestdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(ContestdataPath, FileMode.Open);
            m_Contest = (ContestDictionary)bf.Deserialize(file);
            m_Contest.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_Contest = new ContestDictionary();
        }

        return;
    }

    public IDictionary<string, QuizInfo> QuizInfoDictionary
    {
        get { return m_titleQuiz; }
        set { m_titleQuiz.CopyFrom(value); }
    }

    /*
    public IDictionary<string, Contest> ContestDictionary
    {
        get { return m_Contest; }
        set { m_Contest.CopyFrom(value); }
    }*/

    public void setTitleQuiz()
    {
        m_titleQuiz.Add(title, ans);

    }
    private void Start()
    {
        //ans = new Answer();
        Initialized();
        QuizLoad();

    }
}
