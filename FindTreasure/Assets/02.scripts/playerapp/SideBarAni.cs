using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SideBarAni : MonoBehaviour
{
    public Animator Obj;
    public GameObject bar;
    public void Sidebtn()
    {
        Obj.SetBool("start", true);
    }

    public void ReBtn()
    {
        Obj.SetBool("start", false);
        StartCoroutine(Disabled(0.4f));
    }

    IEnumerator Disabled(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        bar.SetActive(false);
    }
}
