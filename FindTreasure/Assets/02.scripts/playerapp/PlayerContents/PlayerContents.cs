using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TTM.Classes;
using UnityEngine;

public class PlayerContents
{
    PCompetitionDictionary Compets;
    Competition Cur;
    int CurCompet;
    public string CurCompetName; //[yjh]20.10.10 11:15 수정 public으로 바꿈 (주석 삭제plz)
    public string CurCompetition { get => CurCompetName; } 
    PQuizDicitionary CurLib;
    static event Action DicUpdate;
    List<ShortInfo> CurOpenCompets;
    public event Action Library;

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
           // DicUpdate();
            return _Instance;
        }
    }
    #endregion

    public int CheckAnswer(int code, string ans) => SAnswer.CheckAnswer(CurCompet, code,ans);

    public Q FindQ(string key)
    {
        Q Quiz;

        CurLib.TryGetValue(key, out Quiz);
        
        return Quiz;
    }

    public Q FindQ(string name, out int quizid)
    {
        CurLib.transCode.TryGetValue(name, out quizid);

        Q Quiz;

        CurLib.TryGetValue(name, out Quiz);

        return Quiz;
    }

    public List<string> CompetitionList()
    {
        DicUpdate();
        List<string> arr = new List<string>();
        foreach (var key in Compets.Keys)
            arr.Add(key);
        return arr;
    }

    public void setLib(Action func)
    {
        Library = func;
    }

    public void GetLogAdd( out int Max)
    {
        Max = CurLib.Count;
    }

    public List<Texture2D> getLib()
    {
        List<Texture2D> libs = new List<Texture2D>();
        
        DataPath path = new DataPath("JPG/" + CurCompetition);
        path.SetJPG();

        List<string> quiznames = FileList();

        foreach (var quiz in quiznames)
        {
            if (!Player.Instance.clearlist.Contains(quiz))
                libs.Add(new Texture2D(0, 0).Load(CurCompetition, quiz));
        }
        return libs;
    }

    public int GetUserPass() => Cur.UserPass;

    public void SetCompetName(string comp)
    {
        CurCompetName = comp;
        Compets.TryGetValue(comp, out Cur);
    }

    public void ClickListener()
    {
        CurCompet = Compets.CurrentCode(CurCompetName);
        CurLib.GetQuizz(CurCompet);
        if (CurOpenCompets != null)
        {
            Player.Instance.UpdateUserCompets(CurOpenCompets.Find(CurCompetName));
        }

        Player.Instance.StartCompet(CurCompetName);
        ReadytoStart.Ready(CurCompetName);
        FTP.ImageServerAllDownload(CurCompetName, CurLib.Keys.ToList());

        Library();
        SetendTime();
    }

    public void SetendTime()
    {
        foreach (var Short in CurOpenCompets)
        {
            if (Short.ConName == CurOpenCompets.Find(CurCompetName).ConName)
            {
                gameman.Instance.conName = Short.ConName;
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
    
    public TimeSpan startTimelimit()
    {
        var sInfo = Cur.StartTime;
        if (sInfo == null) Debug.Log("Error");
        return sInfo - DateTime.Now;
    }

    public TimeSpan endTimelimit()
    {
        var sInfo = Cur.EndTime;
        return sInfo - DateTime.Now;
    }

    public void repackageLib() => Library();
}
