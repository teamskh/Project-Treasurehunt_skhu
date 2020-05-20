using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class quiz : MonoBehaviour
{


    InputField txt;
    Text qtxt;

    
    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("InQu").GetComponent<InputField>(); //여러개는 find 값 수정하면 될듯??
    
       
    }

    public void inq()
    {
        gameman.Instance.exam = txt.text;
    }
}
