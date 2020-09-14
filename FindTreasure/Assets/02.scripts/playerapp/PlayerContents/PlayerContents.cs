using System;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

public class PlayerContents
{
    PCompetitionDictionary Compets;
    int CurCompet;
    string CurCompetName;
    public string CurCompetition { get => CurCompetName; }
    PQuizDicitionary CurLib;
    static event Action DicUpdate;
    List<ShortInfo> CurOpenCompets;

    #region Singleton
    PlayerContents()
    {
        Compets = new PCompetitionDictionary();
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
        Player.Instance.UpdateUserCompets(FindShorts(com));
    }

    ShortInfo FindShorts(string name)
    {
        foreach(var shorts in CurOpenCompets)
        {
            if (shorts.ConName == name) return shorts;
        }
        return null;
    }
    
}
