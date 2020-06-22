using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

    public void LoadQuiz()
    {
        List<string> list = GetComponent<QuizDic>().GetQuizList();
        foreach (string item in list)
        {
            QList.Add(MakeQuizButton(item));
        }
    }
    public GameObject MakeQuizButton(string name)
    {
        
        GameObject quizb = Instantiate(BPrefab, BPrefab.transform.position, Quaternion.identity);
        //위치 조정
        //quizb.GetComponent<RectTransform>().anchoredPosition.Set(0, QList.Count * 155);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, (QList.Count+1) * 155);
        //글씨 조정
       quizb.GetComponentInChildren<Text>().text = name;
        /*
        if (number == 0)
        {
            var dirPath = Application.dataPath + "/Resources/Texture/";

            if (System.IO.Directory.Exists(dirPath))
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(dirPath);
                foreach (var item in di.GetFiles())
                {
                    Console.WriteLine(item.Name);
                    if (item.Name == name + ".png")
                    {
                        string PATH = "Texture/" + item.Name;
                        texture = Resources.Load(PATH, typeof(Texture2D)) as Texture2D;
                        if (texture == null)
                        {
                            Debug.Log("망함");
                        }
                        quizb.GetComponentInChildren<RawImage>().texture = texture;
                    }
                }
            }
        }
        if (number == 1)
        {
            quizb.GetComponentInChildren<RawImage>().texture = Camera1.savess;
        }*/

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
        LoadQuiz();
    }
    public void Update()
    {
        if (QList.Count == 0)
            LoadQuiz();

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
