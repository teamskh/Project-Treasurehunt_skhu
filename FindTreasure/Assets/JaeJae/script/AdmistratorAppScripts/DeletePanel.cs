using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class DeletePanel : MonoBehaviour
{
    //QuizList내용에서 같이 돌리기 보다 자체 프리펩 내에서 돌릴 수 있게 스크립트 분리
    Button Y;
    Button N;
    QuiDictionary dic;
    Q quiz;
    CompetitionDictionary compdic;
    Competition comp;

    public void OnEnable()
    {
        Y = transform.Find("Y").GetComponent<Button>();
        N = transform.Find("N").GetComponent<Button>();
    }
    private void Start()
    {
        Y?.onClick.AddListener(() => Delete());
        N?.onClick.AddListener(() => Cancel());
    }

    void Delete()
    {
        string key = LongPressButton.Oname;
        if (GameObject.Find("PlayAr") == true)
        {
            Application.Quit();
        }
        else if (GameObject.Find("GameSetting") == true)
        {
            dic = new QuiDictionary();
            dic.TryGetValue(key, out quiz);
            Param param = new Param();
            param.DeleteQuiz(quiz);
            GameObject.Find("AskDel")?.SetActive(false);
            GameObject.Find("GameSetting")?.GetComponent<QuizList>().LoadQuiz();
        }
        else if(GameObject.Find("GameManager") == true)
        {
            compdic = new CompetitionDictionary();
            compdic.TryGetValue(key, out comp);
            Param param = new Param();
            param.DeleteCompetition(comp);
            GameObject.Find("AskDel")?.SetActive(false);
            GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().SetList();
        }
    }
    
    void Cancel()
    {
        GameObject.Find("AskDel")?.SetActive(false);
    }
}
