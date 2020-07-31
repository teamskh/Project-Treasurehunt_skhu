using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using BackEnd;
using System;

public static class PlayerExtension
{
    public static void GetPCompetitions(this Dictionary<string,Competition> dic)
    {
        var admindic = new Dictionary<string, Competition>();
        admindic.GetCompetitions();
        
        foreach(var comp in admindic)
        {
            if(comp.Value.StartTime < DateTime.Now && comp.Value.EndTime > DateTime.Now)
            {
                dic.Add(comp.Key,comp.Value);
            }
        }
    }
    public static void GetPQuizz(this Dictionary<string,Quiz> dic)
    {
        
    }
}
