using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    Button CPB,RCS,CMB;
    GameObject Panel_T;
    private List<GameObject> panel = new List<GameObject>();
    int Pkind;
    
    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "ContestList")
        {
            panel?.Add(GameObject.Find("ContestCanvas").transform.Find("ContestSetupPanel").gameObject ?? null);
            panel?.Add(GameObject.Find("ContestCanvas").transform.Find("AskDel").gameObject ?? null);
            panel?.Add(GameObject.Find("ContestCanvas").transform.Find("AskPw").gameObject ?? null);
        }
        else if (SceneManager.GetActiveScene().name == "ContestMenu")
        {
            CPB = GameObject.Find("ChangePw_b")?.GetComponent<Button>();
            RCS = GameObject.Find("ReCSetting_b")?.GetComponent<Button>();
            CMB = GameObject.Find("ChangeMode_b")?.GetComponent<Button>();
            //Button?.Add(GameObject.Find("ChangePw_b").gameObject ?? null);
            //Button?.Add(GameObject.Find("ReCSetting_b").gameObject ?? null);
            //Button?.Add(GameObject.Find("ChangeMode_b").gameObject ?? null);
            panel?.Add(GameObject.Find("Panel_A").transform.Find("Panel_pw").gameObject ?? null);
            panel?.Add(GameObject.Find("Panel_A").transform.Find("Panel_comp").gameObject ?? null);
            panel?.Add(GameObject.Find("Panel_A").transform.Find("Panel_mode").gameObject ?? null);
        }
        else if(SceneManager.GetActiveScene().name == "02.Main")
        {
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskDel").gameObject ?? null);
        }
        else if (SceneManager.GetActiveScene().name == "QuizMenu")
        {
            panel?.Add(GameObject.Find("Panel").transform.Find("AskDel").gameObject ?? null);
        }
        Panel_T = GameObject.Find("Panel_T").gameObject;
    }
    private void Start()
    {
        Panel_T.SetActive(false);
    }

    public void setNumber(int kind)
    {
        panel[kind].SetActive(true);
        Panel_T.SetActive(true);
        Pkind = kind;
    }
    public void set()
    {
        panel[Pkind].SetActive(false);
        Panel_T.SetActive(false);
    }
}
