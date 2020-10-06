using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class QuizForUI : MonoBehaviour
{
    public GameObject inpu;

    private void OnEnable()
    {
        inpu.SetActive(true);
    }

}
