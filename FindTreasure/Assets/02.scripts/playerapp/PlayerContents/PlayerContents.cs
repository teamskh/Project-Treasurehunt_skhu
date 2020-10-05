using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TTM.Classes;
using UnityEngine;

public class PlayerContents
{
    PCompetitionDictionary Compets;
    int CurCompet;
    string CurCompetName;
    public string CurCompetition { get => CurCompetName; } //얘 가지고 사용하면 될듯
    PQuizDicitionary CurLib;
    static event Action DicUpdate;
    List<ShortInfo> CurOpenCompets;

    #region Singleton
    PlayerContents()
    {
        Compets = new PCompetitionDictionary();
        CurOpenCompets = new List<ShortInfo>();
        CurLib = new PQuizDicitionary();
        DicUpdate = () => Compets.GetCompetitions();
        DicUpdate += () => CurOpenCompets = Compets.GetShorts();
    }

    private static PlayerContents _Instance;

    public static PlayerContents Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new PlayerContents();
            DicUpdate();
            return _Instance;
        }
    }
    #endregion

    public int CheckAnswer(KeyValuePair<int, string> ans) => SAnswer.CheckAnswer(CurCompet, ans);

    public Q FindQ(string key)
    {
        Q Quiz;

        CurLib.TryGetValue(key, out Quiz);
        
        return Quiz;
    }

    public void FindQ(string name, out int quizid)
    {
        CurLib.transCode.TryGetValue(name, out quizid);
    }

    public List<string> CompetitionList()
    {
        List<string> arr = new List<string>();
        foreach (var key in Compets.Keys)
            arr.Add(key);
        return arr;
    }

    public void ClickListener(string com)
    {
        CurCompet = Compets.CurrentCode(com);
        CurCompetName = com;
        CurLib.GetQuizz(CurCompet);
        if (CurOpenCompets != null)
        {
            Player.Instance.UpdateUserCompets(CurOpenCompets.Find(CurCompetName));
        }

        ReadytoStart.Ready(com);
        FTP.ImageServerAllDownload(com, CurLib.Keys.ToList());

        SetendTime();

    }

    public void SetendTime()
    {
        foreach (var Short in CurOpenCompets)
        {
            if (Short.ConName == CurOpenCompets.Find(CurCompetName).ConName)
            {
                gameman.Instance.Opentime = CurOpenCompets.Find(CurCompetName).StartTime;
                gameman.Instance.endtime = CurOpenCompets.Find(CurCompetName).EndingTime;
                gameman.Instance.EndScore = CurLib.sum;
            }
        }
    }

    public List<string> FileList()
    {
        return CurLib.Keys.ToList();
    }
    
    public TimeSpan startTimelimit(string compet)
    {
        var sInfo = CurOpenCompets.Find(compet);

        return sInfo.StartTime - DateTime.Now;
    }

    public TimeSpan endTimelimit(string compet)
    {
        var sInfo = CurOpenCompets.Find(compet);
        return sInfo.EndingTime - DateTime.Now;
    }
}
