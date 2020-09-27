using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class Password : MonoBehaviour
{
    GameObject Panel;
    // Start is called before the first frame update
    Button OK_b;
    Button Cancel_b;
    InputField input;
    CompetitionDictionary dic;
    Competition comp;
    public GameObject all_t;
    public GameObject re;
    private string key;
    //GameObject Panel_T;

    public void OnEnable()
    {
        OK_b = GameObject.Find("OK")?.GetComponent<Button>();
        input= GameObject.Find("password")?.GetComponent<InputField>();
        //Cancel_b = transform.Find("Cancel").GetComponent<Button>();
    }
    private void Start()
    {
        key = LongPressButton.Bname;
        all_t.SetActive(false);
        re.SetActive(false);
        //Panel_T.SetActive(true);
        OK_b?.onClick.AddListener(() => OK_s());
        Debug.Log(1);
        //Cancel_b?.onClick.AddListener(() => Cancel_s());
    }

    void OK_s()
    {
        Debug.Log(key);
        Debug.Log(2);
        dic = new CompetitionDictionary();
        dic.TryGetValue(key, out comp);
        Debug.Log(4);
        if (input.text==comp.Password)
        {
            Debug.Log(5);
            gameObject.GetComponent<LongPressButton>().Change();
            Debug.Log(6);
        }
        else if(input.text.Length<1)
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            Debug.Log(7); 
            return;
        }
        else
        {
            StartCoroutine(setActiveObjinSecond(re, 1f));
            Debug.Log(8);
            return;
        }
    }

    void Cancel_s()
    {
        GameObject.Find("AskPw")?.SetActive(false);
    }

    IEnumerator setActiveObjinSecond(GameObject gameObject, float second)
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(second);
        gameObject.SetActive(false);
    }
}
