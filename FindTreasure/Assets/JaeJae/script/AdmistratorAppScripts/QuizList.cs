using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using TTM.Classes;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if (list != null)
            foreach (string item in list)
            {
                QList.Add(MakeQuizButton(item));
                DownLoadIMG(item);
            }
        ControlIMGs();
    }
    
    public GameObject MakeQuizButton(string name)
    {
        GameObject quizb = Instantiate(BPrefab, BPrefab.transform.localPosition, Quaternion.identity);
        //위치 조정
        quizb.transform.SetParent(Content.transform, false);
        //quizb.GetComponent<RectTransform>().anchoredPosition.Set(0, QList.Count * 155);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, (QList.Count + 1) * 155);
        //글씨 조정
        quizb.GetComponentInChildren<Text>().text = name;
        quizb.GetComponent<Button>()?.onClick.AddListener(() => dic.CurrentCode(quizb.GetComponentInChildren<Text>().text));
        
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
        //AskD.SetActive(true);
        gameObject.GetComponent<PanelScript>().setNumber(0);
        Debug.Log("Active?");
    }

    /*
    public void LoadQuiz()
    {
        List<string> list = GetComponent<QuizDic>().GetQuizList();
        Debug.Log($"List items: {list.Count}");
        if (list != null)
            foreach (string item in list)
        {
            QList.Add(MakeQuizButton(item));
        }
    }
    void SetComp(string name)
    {
        adminManager.Instance.setComp(name);
    }

    public GameObject MakeQuizButton(string name)
    {
        GameObject quizb = Instantiate(BPrefab, BPrefab.transform.localPosition, Quaternion.identity);
        //위치 조정
        //quizb.GetComponent<RectTransform>().anchoredPosition.Set(0, QList.Count * 155);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, (QList.Count+1) * 155);
        //글씨 조정
       quizb.GetComponentInChildren<Text>().text = name;
        //quizb.GetComponent<Button>().onClick.AddListener(delegate () { SetComp(name); });
        /*
        if (number == 0)
        {
            var dirPath = Application.dataPath + "/Resources/Texture/";

            if (System.IO.Directory.Exists(dirPath))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dirPath);

                foreach (var item in di.GetFiles())
                {
                    Debug.Log(item.Name);
                    if (item.Name == name + ".png")
                    {
                        string PATH = "Texture/" + name;
                        //texture = Resources.Load(PATH, typeof(Texture2D)) as Texture2D;
                        texture = Resources.Load<Texture2D>(PATH);
                        if (texture == null)
                        {
                            Debug.Log("망함 " + PATH);
                        }
                        else
                        {
                            quizb.GetComponentInChildren<RawImage>().texture = texture;
                        }

                    }
                }
            }
        }
        if (number == 1)
        {
            quizb.GetComponentInChildren<RawImage>().texture = Camera1.savess;
        }*/
    /*
    var dirPath = Application.dataPath + "/Resources/Texture/";

    if (System.IO.Directory.Exists(dirPath))
    {
        System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dirPath);

        foreach (var item in di.GetFiles())
        {
            Debug.Log(item.Name);
            if (item.Name == name + ".png")
            {
                string PATH = "Texture/" + name;
                //texture = Resources.Load(PATH, typeof(Texture2D)) as Texture2D;
                texture = Resources.Load<Texture2D>(PATH);
                if (texture == null)
                {
                    Debug.Log("망함 "+PATH);
                }
                else
                {
                    quizb.GetComponentInChildren<RawImage>().texture = texture;
                }

            }
        }
    }
    */
    /*
    quizb.transform.SetParent(Content.transform, true);
    return quizb;
}

public void Start()
{
    if (number == 1)
    {
        MakeQuizButton(onClicks.Ttitle);//추가
        number = 0;
    }
    //LoadQuiz();
}
public void OnEnable()
{
    LoadQuiz();
}
public void OnDisable()
{
    foreach (GameObject item in QList) Destroy(item);
}*/
    void ControlIMGs()
    {
        DataPath path = new DataPath(Filename: "JPG/" + AdminCurState.Instance.Competition);

         var delpath = FTP.FtpDownloadUsingWebClient(new DataPath(path.Dir(), "Deleted", ".txt"));
        foreach(var name in unpackTXT(delpath))
        {
            DataPath curpath = new DataPath(path.Dir(), name);
            curpath.SetJPG();

            File.Delete(curpath.ToString());
        }

        var modpath = FTP.FtpDownloadUsingWebClient(new DataPath(path.Dir(), "Modified", ".txt"));
        foreach (var name in unpackTXT(modpath))
        {
            DataPath curpath = new DataPath(path.Dir(), name);
            curpath.SetJPG();

            if (File.Exists(curpath.ToString()))
            {
                File.Delete(curpath.ToString());
            }

            IMG(curpath);
        }
    }
    void DownLoadIMG(string name)
    {
        DataPath path = new DataPath(Filename: "JPG/" + AdminCurState.Instance.Competition, Usercode: name);
        path.SetJPG(); //확장자명 고정
        IMG(path); //전달
    }

    bool IMG(DataPath path)
    {
        if (!File.Exists(path.ToString()))
        {
            FTP.FtpDownloadUsingWebClient(path);
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
