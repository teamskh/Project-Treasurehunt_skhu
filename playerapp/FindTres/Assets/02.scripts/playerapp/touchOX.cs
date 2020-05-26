using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class touchOX : MonoBehaviour
{
    public bool answerO;
    public bool answerX;

    public GameObject qu;
    public GameObject ans;
    public GameObject[] an;

    public Material Mat1;
    public Material Mat2;

    public void SelO()
    {
        if ((answerO == true)&& (answerX ==  false))
        {
            gameman.Instance.score += 1;
            qu.SetActive(false);
            ans.SetActive(true);
            an[0].GetComponent<Renderer>().material = Mat1;
            an[1].GetComponent<Renderer>().material = Mat2;
            an[2].GetComponent<Renderer>().material = Mat2;
            an[3].GetComponent<Renderer>().material = Mat2;

        }
        else
        {
            qu.SetActive(false);
            ans.SetActive(true);
            an[1].GetComponent<Renderer>().material = Mat1;
            an[0].GetComponent<Renderer>().material = Mat2;

            an[2].GetComponent<Renderer>().material = Mat2;
            an[3].GetComponent<Renderer>().material = Mat2;
        }
    }

    public void SelX()
    {
        if ((answerO == false) && (answerX == true))
        {
            qu.SetActive(false);
            ans.SetActive(true);
            an[1].GetComponent<Renderer>().material = Mat1;
            an[0].GetComponent<Renderer>().material = Mat2;
            an[2].GetComponent<Renderer>().material = Mat2;
            an[3].GetComponent<Renderer>().material = Mat2;
            gameman.Instance.score += 1;
        }
        else
        {
            qu.SetActive(false);
            ans.SetActive(true);
            an[0].GetComponent<Renderer>().material = Mat1;
            an[1].GetComponent<Renderer>().material = Mat2;
            an[2].GetComponent<Renderer>().material = Mat2;
            an[3].GetComponent<Renderer>().material = Mat2;
        }
    }

    public void Inp()
    {
        //gameman.Instance.inss.SetActive(true);

    }
}
