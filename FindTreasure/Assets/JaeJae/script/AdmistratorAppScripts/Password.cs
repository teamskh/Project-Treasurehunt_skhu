using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    Button OK_b;
    Button YES_b;
    InputField input;
    CompetitionDictionary dic;
    Competition comp;
    [SerializeField]
    GameObject all_t,re,closePass,inpop;
    private string key;

    //GameObject Panel_T;

    public void OnEnable()
    {
        YES_b = GameObject.Find("YES")?.GetComponent<Button>();
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
        YES_b?.onClick.AddListener(() => YES_s());
    }

    void OK_s()
    {

        key = LongPressButton.Bname;
        dic.TryGetValue(key, out comp);

        if (input.text==comp.Password)
        {
            gameObject.AddComponent<scenechange>().ChangeSceneToAdMenu();
            scenechange.Qname=key;
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

    void YES_s()
    {
        dic = new CompetitionDictionary();
        dic.GetCompetitions();
        key = gameman.Instance.conName;
        dic.TryGetValue(key, out comp);

        if (input.text == comp.Password)
        {
            closePass.SetActive(false);
            inpop.SetActive(true);
            CreateCompetbuttons.che = true;
        }
        else if (input.text.Length < 1)
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
