using BackEnd;
using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

static class ClassesTTMExtension
{
    #region Quiz
    public static Q GetQuiz(this JsonData quiz)
    {
        Q item = new Q();
        item.Title = quiz["title"]["S"].ToString();
       
        item.Str = quiz["context"]["S"].ToString();
        
        item.Kind = int.Parse(quiz["kind"]["N"].ToString());

        item.Score = int.Parse(quiz["score"]["N"].ToString());//추가

        var ans = quiz["answer"]["S"].ToString();
        switch (item.Kind)
        {
            case 0:
                item.Answer = bool.Parse(ans);
                break;
            case 1:
                item.Answer = int.Parse(ans);
                JsonData data = quiz["choices"]["L"];
                var count = data.Count;
                item.List = new string[4];
                for(int i = 0; i < count; i++)
                {
                    item.List[i] = data[i]["S"].ToString();
                }
                break;
            case 2:
                item.Answer = ans;
                break;
        }

        return item;
    }
    public static void GetQuizz(this Dictionary<string, Q> dic,int competitionid)
    {
        //where 조건 설정
        Param where = new Param();
        where.Add("idcompetition", competitionid);

        //데이터 조정
        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.GameSchemaInfo.Get("Quizz", where, 100);
        if (bro.IsSuccess())
        {
            JsonData data = bro.GetReturnValuetoJSON()["rows"];
            foreach(JsonData item in data) {
                var it = item.GetQuiz();
                dic.Add(it.Title, it);
            }
            Debug.Log(data.ToJson());
        }
        else
            dic = null;
    }
    #endregion

    #region Competition

    public static Competition GetCompetition(this JsonData data)
    {
        Competition comp = new Competition();
        comp.Name = data["name"]["S"].ToString();
        comp.Mode = bool.Parse(data["mode"]["BOOL"].ToString());
        if (comp.Mode) comp.MaxMember = int.Parse(data["maxmember"]["N"].ToString());
        comp.Password = data["password"]["S"].ToString();

        DateTime date;
        if (data.Keys.Contains("starttime"))
            if (DateTime.TryParse(data["starttime"]["S"].ToString(), out date))
                comp.StartTime = date;
        if (data.Keys.Contains("endtime"))
            if (DateTime.TryParse(data["endtime"]["S"].ToString(), out date))
                comp.EndTime = date;

        if (data.Keys.Contains("info"))
        {
            if (data["info"].Keys.Contains("NULL")) comp.Info = null;
            else comp.Info = data["info"]["S"].ToString();
        }
        if (data.Keys.Contains("userpass"))
            comp.UserPass = int.Parse(data["userpass"]["N"].ToString());

        return comp;
    }

    public static void GetCompetitions(this Dictionary<string, Competition> dic)
    {
        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.GameSchemaInfo.Get("competitions",new Param(),100);
        if (bro.IsSuccess())
        {
            JsonData data = bro.GetReturnValuetoJSON()["rows"];
            foreach(JsonData item in data)
            {
                var it = item.GetCompetition();
                if (it != null)
                    dic.Add(it.Name, it);
            }
        }
    }

    #endregion

    public static void UnityLog<T>(this T obj)
    {
        Debug.Log(obj.ToString());
    }
}
