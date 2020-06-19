using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using TTM.Classes;

public class Scroll : MonoBehaviour
{
    TextMesh qtxt;
    protected Quiz quiz;

    void Start()
    {
        foreach (var item in GetComponentsInChildren<TextMesh>())
        {
            if (item.gameObject.tag=="STR")
            {
                qtxt = item;
                break;
            }
        }
    }

    public void Init(Quiz quiz)
    {
        this.quiz = quiz;
    }


    // Update is called once per frame
    void Update()
    {
        qtxt.text = gameman.Instance.exam;
    }
}
