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
    
    private bool CheckPassword(string pw)
    {
        Param where = new Param();
        where.Add("univname", univName);

        BackendReturnObject bro = Backend.GameSchemaInfo.Get("univ", where, 1);
        if (bro.IsSuccess())
        {
            var data = bro.Rows();
            Debug.Log(data[0]["univpw"]["S"].ToString());
            if (pw.Trim() == data[0]["univpw"]["S"].ToString()) return true;
            inDate = bro.GetReturnValuetoJSON()["rows"][0]["univpw"]["S"].ToString();
            Debug.Log($"Access Token : {PlayerPrefs.GetString("access_token")}");
        }
        if (pw == inDate) return true;
        else return false;
    }

    public void check()
    {
        Debug.Log(inputField.text);
        if (CheckPassword(inputField.text) == true)
        {
            BackendReturnObject bro = Backend.BMember.CustomLogin("Admin", "toomuch");
            Debug.Log($"Access Token : {PlayerPrefs.GetString("access_token")}");
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
