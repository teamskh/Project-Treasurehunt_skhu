using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class QuizList : MonoBehaviour
{
    public static int number;
    public GameObject BPrefab; //대회버튼
    public GameObject Content;
    public static List<GameObject> QList = new List<GameObject>();
    public static Texture2D texture;
    QuiDictionary dic;
    Competition comp = new Competition();

    public GameObject AskD;
    public void LoadQuiz()
    {
        dic = new QuiDictionary();
        var idcompetition = PlayerPrefs.GetInt("a_competition");
        dic.GetQuizz(idcompetition);
        foreach (GameObject item in QList)
        {
            Destroy(item);
        }
        QList.Clear();

        var list = dic.Keys;
        ControlIMGs(); 
        if (list != null)
            foreach (string item in list)
            {
                DownLoadIMG(item);
                QList.Add(MakeQuizButton(item));
            }
        
    }
    
    public GameObject MakeQuizButton(string name)
    {
        GameObject quizb = Instantiate(BPrefab, BPrefab.transform.localPosition, Quaternion.identity);
        //위치 조정
        quizb.transform.SetParent(Content.transform, false);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, (QList.Count + 1) * 155);
        //글씨 조정
        quizb.GetComponentInChildren<Text>().text = name;

        SaveImage.Instance.SetNewTexture(new Texture2D(0, 0).Load(AdminCurState.Instance.Competition, name), false);
        quizb.GetComponentInChildren<RawImage>().texture = SaveImage.Instance.getTexture();
        quizb.GetComponent<Button>()?.onClick.AddListener(() => dic.CurrentCode(name));
        quizb.GetComponent<Button>()?.onClick.AddListener(() => AdminCurState.Instance.Quiz = name);
       
        
        return quizb;
    }

    public void Start()
    {
        AskD.SetActive(false);
    }
    public void OnEnable()
    {
        LoadQuiz();
    }

    public void SetActive()
    {
        gameObject.GetComponent<PanelScript>().setNumber(0);
    }

    void ControlIMGs()
    {
        DataPath path = new DataPath(Filename: "JPG/" + AdminCurState.Instance.Competition);

        try {
            var delpath = FTP.ImageServerDownload(new DataPath(path.Dir(), "Deleted", ".txt"));
            foreach (var name in unpackTXT(delpath))
            {
                DataPath curpath = new DataPath(path.Dir(), name);
                curpath.SetJPG();

                File.Delete(curpath.ToString());
            }
        }
        catch (WebException e)
        {
            Debug.Log(e.Message);
        }

        try
        {
            var modpath = FTP.ImageServerDownload(new DataPath(path.Dir(), "Modified", ".txt"));
            foreach (var name in unpackTXT(modpath))
            {
                DataPath curpath = new DataPath(path.Dir(), name);
                curpath.SetJPG();
                if (File.Exists(curpath.ToString()))
                {
                    File.Delete(curpath.ToString());
                }
            }
        }catch(WebException e)
        {
            Debug.Log(e.Message);
        }
    }
    void DownLoadIMG(string name)
    {
        DataPath path = new DataPath(Filename: "JPG/" + AdminCurState.Instance.Competition, Usercode: name);
        path.SetJPG();
        IMG(path);
    }

    bool IMG(DataPath path)
    {
        if (!File.Exists(path.ToString()))
        {
            FTP.ImageServerDownload(path);
            return true;
        }

        return false;
    }

    string[] unpackTXT(string path)
    {
        var names = File.ReadAllText(path);
        return names.Split(char.Parse("\n"));
    }
}
