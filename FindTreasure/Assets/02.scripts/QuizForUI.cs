using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;
using UnityEngine.UI;

public class QuizForUI : MonoBehaviour
{

    public GameObject OX;
    public GameObject sel;
    public GameObject inpu;

    private List<GameObject> list = new List<GameObject>();

    private void Start()
    {
        list.Add(OX);
        list.Add(sel);
        list.Add(inpu);
    }
}
