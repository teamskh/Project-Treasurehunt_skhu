using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using TTM.Classes;
using System.IO;
using System.Net;
using System;

public class DownloadFiles : MonoBehaviour
{
    string defaultURL = "";
    //Dictionary<string, Competition> dic = new Dictionary<string, Competition>();
    CompetitionDictionary dic = new CompetitionDictionary();
    void Start()
    {
        Backend.Initialize(() => {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();

                bro = Backend.BMember.CustomLogin("Admin", "toomuch");
                if (bro.IsSuccess())
                {
                    dic.GetCompetitions();
                    foreach (var competition in dic.Keys)
                    {
                        CheckDirRoute(competition);
                    }
                }
            }
        });
        
    }

    void CheckDirRoute(string competitions)
    {
        var route = Application.dataPath + "/Resources/FindTreasure/IMG/" + competitions;
        Debug.Log($"Route : {route}");
        DirectoryInfo CompetDir = new DirectoryInfo(route);
        if (!CompetDir.Exists)
        {
            Directory.CreateDirectory(route);
            Debug.Log($"MakeFile : {new DirectoryInfo(route).Exists}");
        }
        else
            Debug.Log($"Directory is Exist : {CompetDir.Exists}");
        StartCoroutine(IMGDownLoad(route, competitions));
    }

    IEnumerator IMGDownLoad(string route, string competition)
    {
        yield return null;
        File.WriteAllText(route + "/" + competition + ".txt", competition);

        //아래 내용은 실제로 이미지를 다운로드할 수 있는 체계가 잡혔을때 활성화
        //+ DownLoadScene 제작

       // Dictionary<string, Q> Qdic = new Dictionary<string, Q>();
       // Qdic.GetQuizz(0);
       //
       // var dir = defaultURL + "/" + competition;
       // foreach (var quiz in Qdic.Keys)
       // {
       //     string uri = dir + "/" + quiz + ".png?raw=true";
       //     //이미지 다운로드
       //     using (WebClient client = new WebClient())
       //     {
       //         client.DownloadFile(new Uri(uri), route +"/"+quiz+".png");
       //     }
       // }
    }
}
