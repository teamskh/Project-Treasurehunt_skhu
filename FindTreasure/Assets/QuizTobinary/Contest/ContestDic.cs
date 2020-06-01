using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using serializeDic;

[System.Serializable]
public class Contest
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
public class ContestDictionary : SerializableDictionary<string, Contest>
{
    protected ContestDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }
    public ContestDictionary() { }
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


public class ContestDic : MonoBehaviour
{
    #region Save Varaiable
    [SerializeField]
    ContestDictionary m_Contest;
    #endregion

    #region Public Variable
    #endregion

    #region Instance
    public static ContestDic instance;
    #endregion

    #region Private Variable
    private string ContestdataPath;
    #endregion

    #region Private Methods
    private void Initialized()
    {
        ContestdataPath = Application.persistentDataPath + "Contest.dat";
    }

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
    }

    // save 함수
    private void ContestSave()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(ContestdataPath);
        ContestDictionary data = new ContestDictionary();
        data.CopyFrom(m_Contest);

        data.OnBeforeSerialize();
        bf.Serialize(file, data);
        file.Close();


    }
    #endregion

    #region For Instance Methods
    public static ContestDic Instance
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
    public void AddContest(string Title,Contest con)
    {
        m_Contest.Add(Title, con);
        ContestSave();
    }

    public List<string> getCurrentList()
    {
        return m_Contest.getContestList();
    }
    #endregion

    public IDictionary<string, Contest> ContestDictionary
    {
        get { return m_Contest; }
        set { m_Contest.CopyFrom(value); }
    }
}
