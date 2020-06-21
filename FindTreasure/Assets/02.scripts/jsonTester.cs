using DataInfo;
using System.Collections;
using System.Collections.Generic;
using TTM.Save;
using UnityEngine;
using BackEnd;

public class jsonTester : MonoBehaviour
{


    // Start is called before the first frame update
    QuizInfoDictionary dic;
    void Start()
    {
        //init();
       
    }
    public void init()
    {
        JsonLoadSave.LoadQuizMade(out dic);
        string str = $"{dic.Count}";
        Debug.Log($"{str}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
