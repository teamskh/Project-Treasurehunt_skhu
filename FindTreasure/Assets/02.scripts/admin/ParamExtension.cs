using System.Collections;
using System.Collections.Generic;
using BackEnd;
using TTM.Classes;
using UnityEngine;

static class ParamExtension
{
    public static void SetQuiz(this Param myParam, Q quiz)
    {
        myParam.Add("idcompetition", 0);
        myParam.Add("idquiz", 0);
      
        myParam.Add("title", quiz.Title);
        myParam.Add("context", quiz.Str);
        myParam.Add("score", quiz.Score.Value);
      
       myParam.Add("kind", quiz.Kind.Value);
       myParam.Add("answer", quiz.Answer.ToString());
       
       if (quiz.List != null)
       {
           myParam.Add("choices", quiz.List);
       }
    }

    public static void Insert(this Param param)
    {
        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.GameSchemaInfo.Insert("Quizz", param);
        if (bro.IsSuccess())
        {
            Debug.Log("Success");
        }
        else
            Debug.Log(bro.ToString());
    }

    public static string ToStr(this Param param) {
        string txt = "";
        txt = param.GetJson();
        return txt;
    }

    public static void GetQuiz(this Param param,Param where, int count)
    {
        BackendReturnObject bro = new BackendReturnObject();

    }
}
