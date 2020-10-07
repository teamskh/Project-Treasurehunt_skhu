using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputUI : MonoBehaviour
{
    [SerializeField] Text Context;
    [SerializeField] Button OK;
    [SerializeField] InputField Answer;

    public void SetActive(string context,Action<string> Check)
    {
        Context.text = context;
        Debug.Log(context);
        gameObject.SetActive(true);
        OK.onClick.AddListener(() => Check(Answer.textComponent.text));
        OK.onClick.AddListener(() =>gameObject.SetActive(false));
    }

}
