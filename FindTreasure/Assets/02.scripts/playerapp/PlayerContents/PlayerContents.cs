using System;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

public class PlayerContents
{

    PCompetitionDictionary Compets; //key 대회이름, value 대회 내용
    Dictionary<int, PQuizDicitionary> Quizzdic;
    static event Action DicUpdate;

    #region Singleton
    PlayerContents()
    {
        Quizzdic = new Dictionary<int, PQuizDicitionary>();
        Compets = new PCompetitionDictionary();
        DicUpdate = UpdateDics;
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

    //event로 걸어 놓은 함수(인스턴스에 접근할때마다 최신 버전으로 패치)
    void UpdateDics()
    {
        Compets.GetCompetitions();
        foreach (var key in Compets.transCode.Values)
        {
            PQuizDicitionary dic;
            if (!Quizzdic.ContainsKey(key))
                dic = new PQuizDicitionary();
            else
                Quizzdic.TryGetValue(key, out dic);

            dic.GetQuizz(key);
            Quizzdic.Remove(key);
            Quizzdic.Add(key, dic);
        }
    } 

    public Q FindQ(string key)
    {
        Q Quiz;

        int code = PlayerPrefs.GetInt("p_competition");

        FindLib(code).TryGetValue(key, out Quiz);
        
        return Quiz;
    }
    
    PQuizDicitionary FindLib(int code)
    {
        PQuizDicitionary currentlib;
        Quizzdic.TryGetValue(code, out currentlib);
        return currentlib;
    }

    public List<string>  CompetitionList() //prefab을 만듦 
    {
        List<string> arr = new List<string>();
        foreach (var key in Compets.Keys)
            arr.Add(key);
        return arr;
    }

    public void ClickListener(string com)
    {
        Compets.CurrentCode(com);
    }
    
}
