using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using serializeDic;

[System.Serializable]
public class Competition
{
    public bool Mode;
    public int MaxMember;
    public string Password;
    public DateTime StartTime;
    public DateTime EndTime;
    public string info;
    public int Userword;
}

[System.Serializable]
public class CompetitionDictionary : SerializableDictionary<string, Competition>
{
    protected CompetitionDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public CompetitionDictionary() { }
    public List<string> getContestList()
    {
        List<string> vs = new List<string>();

        foreach (string k in this.Keys)
        {
            vs.Add(k);
        }

        return vs;
    }
}


public class CompetDic : MonoBehaviour
{
    #region Save Varaiable
    [SerializeField]
    CompetitionDictionary m_Competition;
    #endregion

    #region Public Variable
    #endregion

    #region Instance
    public static CompetDic instance;
    #endregion

    #region Private Variable
    private string CompetdataPath;
    #endregion

    #region Private Methods
    private void Initialized()
    {
        CompetdataPath = Application.persistentDataPath + "Contest.dat";
    }

    private void ContestLoad()
    {
        if (File.Exists(CompetdataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(CompetdataPath, FileMode.Open);
            m_Competition = (CompetitionDictionary)bf.Deserialize(file);
            m_Competition.OnAfterDeserialize();
            file.Close();
        }
        else
        {
            m_Competition = new CompetitionDictionary();
        }
    }

    // save 함수
    private void ContestSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(CompetdataPath);
        CompetitionDictionary data = new CompetitionDictionary();
        data.CopyFrom(m_Competition);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();


    }
    #endregion

    #region For Instance Methods
    public static CompetDic Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        //FindTreasure Initialize;
        Initialized();
        ContestLoad();
    }

    #endregion

    #region Public Methods
    //변수 추가용 함수 : Dictionary에 대회 더하고, 파일로 저장
    public void AddContest(string Title,Competition com)
    {
        m_Competition.Add(Title, com);
        ContestSave();
    }

    public List<string> getCurrentList()
    {
        return m_Competition.getContestList();
    }
    public void DelCompt(string key)
    {
        if (m_Competition.Remove(key))
        {
            ContestSave();
#if UNITY_EDITOR
            Debug.Log("Detele Competition Key: " + key);
#endif
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("Can't Remove Competition Key: " + key);
#endif
        }
    }
#endregion

public IDictionary<string, Competition> ContestDictionary
    {
        get { return m_Competition; }
        set { m_Competition.CopyFrom(value); }
    }
}
