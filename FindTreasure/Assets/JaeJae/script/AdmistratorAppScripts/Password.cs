using BackEnd;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    Button OK_b;
    InputField input;
    CompetitionDictionary dic;
    Competition comp;
    [SerializeField]
    GameObject all_t,re;
    private string key;
    CompetitionDictionary compdic;
    GameObject Panel1, Panel2;
    public void OnEnable()
    {
        OK_b = GameObject.Find("OK")?.GetComponent<Button>();
        input= GameObject.Find("password")?.GetComponent<InputField>();
    }
    private void Start()
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        all_t.SetActive(false);
        re.SetActive(false);
        OK_b?.onClick.AddListener(() => OK_s());
    }

    void OK_s()
    {
        key=AdminCurState.Instance.Competition;
        dic.TryGetValue(key, out comp);

        if (input.text==comp.Password)
        {
            if (GameObject.Find("AskDel"))
            {
                string key = AdminCurState.Instance.Competition;
                compdic = new CompetitionDictionary();
                compdic.TryGetValue(key, out comp);
                Param param = new Param();
                param.DeleteCompetition();
                GameObject.Find("GameManager")?.GetComponent<CompetitionToServer>().SetList();
                try
                {
                    FTP.ImageServerAllIMG(key);
                }
                catch (WebException e)
                {
                    Debug.Log(e.Message);
                }
                gameObject.GetComponent<PanelScript>().setP(1);
                input.SetTextWithoutNotify("");
            }
            else
            {
                gameObject.AddComponent<scenechange>().ChangeSceneToAdMenu();
            }
        }
        else if(input.text.Length<1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }
        else
        {
            StartCoroutine(setActiveObjinSecond(re, 1f));
            return;
        }
    }


    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
