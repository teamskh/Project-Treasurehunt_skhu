using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class QuizForUI : MonoBehaviour
{

    public GameObject OX;
    public GameObject sel;
    public GameObject inpu;

    private List<GameObject> list = new List<GameObject>();

    // Start is called before the first frame update
    private void OnEnable()
    {
        Quiz quiz = gameman.Instance.FindQuiz();
        int kind = quiz.Kind;
        for(int i = 0; i < list.Count; i++)
        {
            if (i == kind)
                list[i].SetActive(true);
            else
                list[i].SetActive(false);
        }
        foreach(Text txt in GetComponentsInChildren<Text>())
        {
            if(txt.gameObject.tag == "STR")
            {
                txt.text = quiz.Str;
                return;
            }
        }
    }
    private void Start()
    {
        list.Add(OX);
        list.Add(sel);
        list.Add(inpu);
    }
}
