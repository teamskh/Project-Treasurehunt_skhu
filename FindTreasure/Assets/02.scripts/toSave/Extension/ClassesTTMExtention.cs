using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

static class ClassesTTMExtension
{
    public static Q GetQuiz(this JsonData quiz)
    {
        Q item = new Q();
        item.Title = quiz["title"]["S"].ToString();
       
        item.Str = quiz["context"]["S"].ToString();
        
        item.Kind = int.Parse(quiz["kind"]["N"].ToString());
        
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
}
