using BackEnd;
using System;
using System.Collections;
using System.Collections.Generic;
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

    //GameObject Panel_T;

    public void OnEnable()
    {
        
    }
    private void Start()
    {
        OK_b = transform.Find("OK")?.GetComponent<Button>();
        input = transform.Find("password")?.GetComponent<InputField>();
        all_t.SetActive(false);
        re.SetActive(false);
        OK_b?.onClick.AddListener(() => OK_s());
    }

    void OK_s()
    {
        try
        {
            dic = new CompetitionDictionary();
            dic.GetCompetitions();

            key = AdminCurState.Instance.Competition;
            dic.TryGetValue(key, out comp);

            if (input.text == comp.Password)
            {
                gameObject.AddComponent<scenechange>().ChangeSceneToAdMenu();
                scenechange.Qname = key;
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
        catch(Exception e)
        {
            foreach (DictionaryEntry de in e.Data)
                Debug.Log(string.Format("    Key: {0,-20}      Value: {1}",
                                  "'" + de.Key.ToString() + "'", de.Value));
        }
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
