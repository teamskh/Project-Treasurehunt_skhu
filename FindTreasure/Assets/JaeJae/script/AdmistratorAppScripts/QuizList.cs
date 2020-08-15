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
    public void LoadQuiz()
    {
        dic = new QuiDictionary();
        dic.GetQuizz(comp.wordNumber);
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
            }
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
       
    }
    public void OnEnable()
    {
        LoadQuiz();
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
        public void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("administratorMenu");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            SceneManager.LoadScene("administratorMenu");
        }
    }
}
