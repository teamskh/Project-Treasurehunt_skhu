using System.Collections;
using System.Collections.Generic;
using BackEnd;
using LitJson;
using TTM.Classes;
using UnityEngine;

static class ParamExtension
{
    public static Param SetQuiz(this Param myParam, Q quiz)
    {
        /*
        myParam.Add("idcompetition", 0);
        myParam.Add("idquiz", 0);*/
        var idcompetition= PlayerPrefs.GetInt("a_competition");
        myParam.Add("idcompetition", idcompetition);

        QuiDictionary dic = new QuiDictionary();
        dic.GetQuizz(idcompetition);

        int idquiz = 0;
        MakeRandomCode.MakeCode(dic, out idquiz);
        myParam.Add("idquiz", idquiz);

        myParam.Add("title", quiz.Title);
        myParam.Add("context", quiz.Str);
        myParam.Add("score", quiz.Score.Value);
      
       myParam.Add("kind", quiz.Kind.Value);
       myParam.Add("answer", quiz.Answer.ToString());
       
       if (quiz.List != null){
           myParam.Add("choices", quiz.List);
       }

        return myParam;
    }

    public static void InsertQuiz(this Param param)
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

    public static Param SetCompetition(this Param param, Competition comp)
    {
        param.Add("name", comp.Name);
        param.Add("mode", comp.Mode);
        if (comp.Mode) param.Add("maxmember", comp.MaxMember);
        param.Add("password", comp.Password);
        if (comp.Info == null) comp.Info = null;
        else param.Add("info", comp.Info);
        if (comp.UserPass.ToString()==null) comp.Info = null;
        else param.Add("userpass", comp.UserPass);
        return param;
    }

    public static void InsertCompetition(this Param param)
    {
        CompetitionDictionary dic = new CompetitionDictionary();
        dic.GetCompetitions();

        int code = 0;
        MakeRandomCode.MakeCode(dic, out code);
        param.Add("code", code);

        Debug.Log(param.GetJson());
        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.GameSchemaInfo.Insert("competitions", param);
        if (bro.IsSuccess())
            Debug.Log("Success");
        else
            Debug.Log(bro.ToString());
    }

    public static void DeleteCompetition(this Param param, Competition comp)
    {
        
        TTM.Classes.CompetitionDictionary dic = new TTM.Classes.CompetitionDictionary();
        dic.GetCompetitions();

        Debug.Log(param.GetJson());
        BackendReturnObject bro = new BackendReturnObject();
        bro = Backend.GameSchemaInfo.Delete("competitions", param);
        if (bro.IsSuccess())
            Debug.Log("Success");
        else
            Debug.Log(bro.ToString());
    }
}
