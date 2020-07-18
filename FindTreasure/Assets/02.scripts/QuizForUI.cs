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

    private Animator anim;
    private List<GameObject> list = new List<GameObject>();

    public void FindQuizAndSet() { 
        Quiz quiz = gameman.Instance.FindQuiz();
        if (quiz != null)
        {
            int kind = quiz.Kind;
            for (int i = 0; i < list.Count; i++)
            {
                if (i == kind)
                    list[i].SetActive(true);
                else
                    list[i].SetActive(false);
            }
            foreach (Text txt in GetComponentsInChildren<Text>())
            {
                if (txt.gameObject.tag == "STR")
                {
                    txt.text = quiz.Str;
                    return;
                }
            }
        }
    }
    private void AnimStart(bool open)
    {
        if (anim != null)
            anim.SetBool("Open", open);
        else
            Debug.Log("Anim is Null");
    }


    private void OnEnable()
    {
        AnimStart(true);
        FindQuizAndSet();
    }

    private void OnDisable()
    {
        AnimStart(false);
    }

    private void Start()
    {
        list.Add(OX);
        list.Add(sel);
        list.Add(inpu);
        anim = GetComponentInChildren<Animator>();
    }
}
