using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

public static class CompetitionParam
{
    private static Param WhereCompet(this Param mine)
    {
        mine.Add("code", PlayerPrefs.GetInt("a_competition"));
        return mine;
    }

    public static Param CompetName(this Param update, string name)
    {
        update.Add("name", name);
        return update;
    }

    public static Param CompetInfo(this Param update, string info)
    {
        update.Add("info", info);
        return update;
    }

    public static Param CompetStart(this Param update, DateTime start)
    {
        update.Add("starttime", start.ToString());
        return update;
    }

    public static Param CompetEnd(this Param update, DateTime end)
    {
        update.Add("endtime", end.ToString());
        return update;
    }

    public static Param CompetToTeam(this Param update, int max)
    {
        update.Add("mode", true);
        update.Add("maxmember", max);
        return update;
    }

    public static Param CompetToSingle(this Param update)
    {
        update.Add("mode", false);
        update.Add("maxmember", 0);
        return update;
    }
    public static Param CompetPassword(this Param update,string pass)
    {
        update.Add("password", pass);
        return update;
    }

    public static void CompetUpdate(this Param update)
    {
        BackendReturnObject bro = Backend.GameSchemaInfo.Update("competitions", new Param().WhereCompet(), update);
        if (bro.IsSuccess())
            Debug.Log("Update Success");
    }

    public static void CompetInsert(this Param insert, Competition compet)
    {
        insert.CompetName(compet.Name)
            .CompetPassword(compet.Password)
            .CompetStart(compet.StartTime)
            .CompetEnd(compet.EndTime);

        if (compet.Mode) insert.CompetToTeam(compet.MaxMember);
        else insert.CompetToSingle();

        CompetitionDictionary dic = new CompetitionDictionary();
        dic.GetCompetitions();
        int code = 0;
        MakeRandomCode.MakeCode(dic, out code);
        insert.Add("code", code);

        BackendReturnObject bro = Backend.GameSchemaInfo.Insert("competitions", insert);
        if (bro.IsSuccess()) Debug.Log("InsertSucess");
    }

    public static void DeleteCompetition(this Param delete,string name)
    {
        CompetitionDictionary dic = new CompetitionDictionary();
        dic.GetCompetitions();

        int code;
        dic.transCode.TryGetValue(name, out code);

        delete.Add("code", code);
        Param quizz = new Param();
        quizz.Add("idcompetition", code);
        BackendReturnObject bro = Backend.GameSchemaInfo.Delete("Quizz", quizz);
        if (bro.IsSuccess())
        {
            bro = Backend.GameSchemaInfo.Delete("competitions", delete);
        }
    }
}
