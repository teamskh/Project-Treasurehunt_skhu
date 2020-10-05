using BackEnd;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAdminMode : MonoBehaviour
{
    string univName = "성공회대학교";
    string inDate;
    [SerializeField]
    InputField inputField;
    [SerializeField]
    GameObject all_t, re;
    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(() =>
        {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();
                bro = Backend.BMember.CustomLogin("Admin", "toomuch");
            }
        });
    }

    private bool CheckPassword(string pw)
    {
        Param where = new Param();
        where.Add("univname", univName);

        BackendReturnObject bro = new BackendReturnObject();
        Debug.Log(bro.GetStatusCode());
        bro = Backend.GameSchemaInfo.Get("univ", where, 1);
        Debug.Log(where.GetJson().ToString());
        if (bro.IsSuccess())
        {
            var data = bro.Rows();
            if (pw == data[0]["univpw"]["S"].ToString()) return true;
            inDate = bro.GetReturnValuetoJSON()["rows"][0]["univpw"]["S"].ToString();
        }
        if (pw == inDate) return true;
        else return false;
    }

    public void check()
    {
        Debug.Log(inputField.text);
        if (CheckPassword(inputField.text) == true)
        {
            gameObject.GetComponent<scenechange>().Loading();
        }
        else if(inputField.text.Length<1)//비밀번호안침
        {
            StartCoroutine(setActiveObjinSecond(all_t, 1f));
            return;
        }
        else//비밀번호틀림
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
