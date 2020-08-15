using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using TTM.Classes;

public class Scroll : MonoBehaviour
{
    Transform qtxt;

    private void Awake()
    {
        qtxt = transform.Find("qtxt");
    }

    public void Init(Q item)
    {
        qtxt.GetComponent<TextMesh>().text = item.Str;
    }

}
