using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class scroll : MonoBehaviour
{
    
    TextMesh qtxt;

    // Start is called before the first frame update
    void Start()
    {
       
        qtxt = GameObject.Find("qtxt").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        qtxt.text = quizLib.Instance.exam;
    }

    public void inq()
    {
       
    }
}
