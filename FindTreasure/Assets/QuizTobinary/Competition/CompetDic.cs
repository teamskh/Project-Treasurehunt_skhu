using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using BackEnd;
using static BackEnd.BackendAsyncClass;
using serializeDic;
using TTM.Classes;
using TTM.Save;

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

    #region Private Variable
   
    #endregion

    #region Private Methods
  
    private void SaveCompetition()
    {
        Param param = new Param();

    }


    #endregion

    #region Monobehavior Methods

    private void Start()
    {
        JsonLoadSave.LoadCompetitions(out m_Competition);
    }

    #endregion

    #region Public Methods
    //변수 추가용 함수 : Dictionary에 대회 더하고, 파일로 저장
    public void AddContest(string Title,Competition com)
    {
        m_Competition.Add(Title, com);
        JsonLoadSave.SaveCompetitions(m_Competition);
    }

   
    public void DelCompt(string key)
    {
        if (m_Competition.Remove(key))
        {
            JsonLoadSave.SaveCompetitions(m_Competition);
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

    //-------Backend-------
    
    public void GetTableList()
    {
        BackendReturnObject tablelist = Backend.GameInfo.GetTableList();

        if (tablelist.IsSuccess()) {
            SetTable(tablelist.GetReturnValuetoJSON());
        };
    }

    List<string> PublicTables = new List<string>();

    void SetTable(JsonData data)
    {
        JsonData publics = data["publicTables"];
        foreach(JsonData row in publics)
        {
            PublicTables.Add(row.ToString());
        }
    }

    public List<string> getCurrentList()
    {
        return PublicTables;
    }

    public void GetPublicContents(string public_table_name)
    {
        BackendReturnObject backendReturnObject = Backend.GameInfo.GetPublicContents(public_table_name, 20);
    }


    #endregion

    void GetGameInfo(JsonData returnData)
    {
        // ReturnValue가 존재하고, 데이터가 있는지 확인
        if (returnData != null)
        {
            Debug.Log("returnvalue is not null");
            // for the rows 
            if (returnData.Keys.Contains("rows"))
            {
                Debug.Log("returnvalue contains rows");
                JsonData rows = returnData["rows"];

                for (int i = 0; i < rows.Count; i++)
                {
                    GetData(rows[i]);
                }
            }
            // for an row
            else if (returnData.Keys.Contains("row"))
            {
                Debug.Log("returnvalue contains row");
                JsonData row = returnData["row"];

                GetData(row[0]);
            }
        }
        else
        {
            Debug.Log("contents has no data");
        }
    }

    void GetData(JsonData data)
    {
        Competition comp = new Competition();
        string name;
        string nameKey = "name";
        string modeKey = "mode";
        string listKey = "list_string";

        // name 라는 key가 존재하는지 확인
        if (data.Keys.Contains(nameKey))
        {
            name = data[nameKey]["S"].ToString();
            Debug.Log("Competition Name: " + name);
        }
        else
        {
            Debug.Log("there is no key " + nameKey);
            return;
        }

        // mode 라는 key가 존재하는지 확인
        if (data.Keys.Contains(modeKey))
        {
            comp.Mode = data[modeKey]["B"].ToString() == "true" ? true : false;
            Debug.Log("Mode: " + ((comp.Mode) ? "Team" : "Individual"));
            if (comp.Mode)
            {
                string maxmemberkey = "maxmember";
                if (data.Keys.Contains(maxmemberkey))
                {
                    comp.MaxMember = int.Parse(data[maxmemberkey]["N"].ToString());
                    Debug.Log("MaxMember: " + comp.MaxMember);
                }
                else
                {
                    Debug.Log("there is no key " + maxmemberkey);
                    return;
                }

            }
        }
        else
        {
            Debug.Log("there is no key " + modeKey);
            return;
        }

        // list_string 라는 key가 존재하는지 확인
        if (data.Keys.Contains(listKey))
        {
            List<string> returnlist = new List<string>();
            JsonData list = data[listKey]["L"];
            var listCount = list.Count;
            if (listCount > 0)
            {
                for (int j = 0; j < listCount; j++)
                {
                    var listdata = list[j]["S"].ToString();
                    returnlist.Add(listdata);
                }
                Debug.Log(JsonMapper.ToJson(returnlist));
            }
            else
            {
                Debug.Log("list has no data");
            }
        }
        else
        {
            Debug.Log("there is no key " + listKey);
        }
    }

}
