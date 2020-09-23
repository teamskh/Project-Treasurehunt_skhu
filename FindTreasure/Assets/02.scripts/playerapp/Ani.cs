using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Ani : MonoBehaviour
{
    public Animator Obj;
    public GameObject bar;

    public Animator StartObj;
    public GameObject ListObj;
    public GameObject BtnObj;
    public GameObject welcome;
    

    public void Sidebtn()
    {
        Obj.SetBool("start", true);
    }

    public void ReBtn()
    {
        Obj.SetBool("start", false);
        StartCoroutine(Disabled(0.5f));
    }

    public void StartBtn()
    {
        StartObj.SetBool("start", true);
        StartCoroutine(abled(1.36f));
    }

    /*
    public void BtnZip()
    {
        Obj.SetBool("start", true);
        StartCoroutine(Btnabled(0.5f));

        StartCoroutine(BtnDisabled(2f));
    }*/

    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        bar.SetActive(false);
    }

    IEnumerator abled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        BtnObj.SetActive(false);
        welcome.SetActive(false);
        ListObj.SetActive(true);
    }

    /*IEnumerator Btnabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        BtnObj.SetActive(true);
        ListObj.SetActive(true);
        HideObj.SetActive(true);
    }

    IEnumerator BtnDisabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Obj.SetBool("start", false);
        HideObj.SetActive(false);
        BtnObj.SetActive(false);
        ListObj.SetActive(false);
    }*/
}
