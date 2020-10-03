using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coru : MonoBehaviour
{
    public void Run()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        Debug.Log("1");
        yield return new WaitForSeconds(1);
    }
}
