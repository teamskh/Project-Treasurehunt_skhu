using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class DownLoadFiles : MonoBehaviour
{
    string gitFileRoot = "https://github.com/teamskh/REPORT/tree/master/contents";
    string downLoadPath;
    string FileName = "/Contest.dat";
    WebClient wc;
    // Start is called before the first frame update
    void Start()
    {
        downLoadPath = Application.persistentDataPath;

        wc = new WebClient();
        DownLoad();
    }

    private void DownLoad()
    {
        try
        {
            wc.DownloadFile(new Uri(gitFileRoot), downLoadPath + FileName);
        }
        catch(Exception e)
        {
            Debug.LogFormat("Error: {0}", e.ToString());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
