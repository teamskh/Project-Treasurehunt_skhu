using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizList : MonoBehaviour
{
    
    public GameObject BPrefab; //대회버튼
    public GameObject Content;
    public static List<GameObject> QList = new List<GameObject>();

    public void LoadQuiz()
    {
        List<string> list = GetComponent<QuizDic>().GetQuizList();
        Debug.Log($"List items: {list.Count}");
        foreach (string item in list)
        {
            QList.Add(MakeQuizButton(item));
        }
    }
    public GameObject MakeQuizButton(string name)
    {
        GameObject quizb = Instantiate(BPrefab, BPrefab.transform.localPosition, Quaternion.identity);
        //위치 조정
        //quizb.GetComponent<RectTransform>().anchoredPosition.Set(0, QList.Count * 155);
        Content.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, (QList.Count+1) * 155);
        //글씨 조정
        quizb.GetComponentInChildren<Text>().text = name;
        quizb.transform.SetParent(Content.transform, true);

        return quizb;
    }
    public void Start()
    {
        LoadQuiz();
    }
    public void OnEnable()
    {
        LoadQuiz();
    }
    public void OnDisable()
    {
        foreach (GameObject item in QList) Destroy(item);
    }
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
