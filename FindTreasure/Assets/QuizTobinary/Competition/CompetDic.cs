using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using serializeDic;
using TTM.Classes;

public class Compet
{
    public string name;
    public bool Mode;
    public int MaxMember;
    public string Password;
    public DateTime StartTime;
    public DateTime EndTime;
    public string info;
    public int Userword;
    public string nowname;
}

[System.Serializable]
public class CompetitionDictionary : SerializableDictionary<string, Competition>
{
    public CompetitionDictionary() { }
    public List<string> getCompetitionList()
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

    #region Monobehavior Methods
    CompetitionDictionary m_Competition = new CompetitionDictionary();
    public bool isSet = false;

    private void Update()
    {
        if (isSet)
        {
            m_Competition.CopyFrom(adminManager.Instance.CallCompetDic());
            isSet = false;
        }
        
    }

    #endregion

    #region Public Methods
    //변수 추가용 함수 : Dictionary에 대회 더하고, 파일로 저장
    public void AddContest(string Title,Competition com)
    {
        m_Competition.Add(Title, com);
        adminManager.Instance.CompetitionCommunication(m_Competition);
    }

    public List<string> getCurrentList()
    {
        if (m_Competition == null) return null;

        return m_Competition.getCompetitionList();
    }
   
    public void DelCompt(string key)
    {
        if (m_Competition.Remove(key))
        {
            adminManager.Instance.CompetitionCommunication(m_Competition);
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

}
