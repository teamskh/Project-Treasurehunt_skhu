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
            panel?.Add(GameObject.Find("Canvas").transform.Find("ContestSetupPanel").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskDel").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskPw").gameObject ?? null);
            panel[2].transform.SetAsLastSibling();
        }
        else if (SceneManager.GetActiveScene().name == "ContestMenu")
        {
            CPB = GameObject.Find("ChangePw_b")?.GetComponent<Button>();
            RCS = GameObject.Find("ReCSetting_b")?.GetComponent<Button>();
            CMB = GameObject.Find("ChangeMode_b")?.GetComponent<Button>();
            panel?.Add(GameObject.Find("Canvas").transform.Find("Panel_pw").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("Panel_comp").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("Panel_mode").gameObject ?? null);
            Panel_T = GameObject.Find("Canvas").transform.Find("Panel_T").gameObject;
        }
        else if(SceneManager.GetActiveScene().name == "02.Main")
        {
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskDel").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("PanelAdPw").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskPw").gameObject ?? null);
            panel?.Add(GameObject.Find("Canvas").transform.Find("Panel_h").gameObject ?? null);
            Panel_T = GameObject.Find("Canvas").transform.Find("Panel_T").gameObject;
            panel[0].transform.SetAsLastSibling();
        }
        else if (SceneManager.GetActiveScene().name == "QuizMenu")
        {
            panel?.Add(GameObject.Find("Canvas").transform.Find("AskDel").gameObject ?? null);
        }
        else if (SceneManager.GetActiveScene().name == "QuizAdd")
        {
            panel?.Add(GameObject.Find("Canvas").transform.Find("CameraP").gameObject ?? null);
            Panel_T = GameObject.Find("Canvas").transform.Find("Panel_T").gameObject;
        }
    }

    public void setNumber(int kind)
    {
        Debug.Log(panel[kind].name);
        panel[kind].SetActive(true);
        Panel_T.SetActive(true);
        Pkind = kind;
    }
    public void set()
    {
        panel[Pkind].SetActive(false);
        Panel_T.SetActive(false);
    }

    public void set(int a)
    {
        panel[a].SetActive(true);
    }
    public void _set(int a)
    {
        panel[a].SetActive(false);
    }

    public void resetInput(InputField inputfield)
    {
        inputfield.SetTextWithoutNotify("");
    }

    public void setPanel(int a)
    {
        panel[a].SetActive(true);
        panel[a+1].SetActive(true);
    }

    public void setP(int a)
    {
        panel[a].SetActive(false);
        panel[a+1].SetActive(false);
    }
}
