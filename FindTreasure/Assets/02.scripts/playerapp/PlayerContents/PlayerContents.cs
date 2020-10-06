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
    string CurCompetName;
    public string CurCompetition { get => CurCompetName; } //얘 가지고 사용하면 될듯
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

    public int CheckAnswer(KeyValuePair<int, string> ans) => SAnswer.CheckAnswer(CurCompet, ans);

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

    public List<Texture2D> getLib()
    {
        List<Texture2D> libs = new List<Texture2D>();
        
        DataPath path = new DataPath("JPG/" + CurCompetition);
        path.SetJPG();

        List<string> quiznames = FileList();

        foreach (var quiz in quiznames)
        {
            var p = path.Files(quiz);
            byte[] bytetexture = File.ReadAllBytes(p);
            if (bytetexture.Length > 0)
            {
                Texture2D txtur = new Texture2D(0, 0);
                txtur.name = quiz;
                txtur.LoadImage(bytetexture);
                libs.Add(txtur);
            }
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
}
