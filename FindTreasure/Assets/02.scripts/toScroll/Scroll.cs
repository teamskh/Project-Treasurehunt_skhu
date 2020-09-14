using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using TTM.Classes;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    GameObject OX;
    [SerializeField]
    GameObject List;
    [SerializeField]
    GameObject Input;

    Transform qtxt;
    Transform buttonPos;

    private void Awake()
    {
        qtxt = transform.Find("qtxt");
        buttonPos = transform.Find("Buttons");
    }

    public void Init(Q item)
    {
        int kind = item.Kind.Value;
        qtxt.GetComponent<TextMesh>().text = item.Str;
         
        switch (kind)
        {
            case 0:
                Instantiate(OX, buttonPos);
                break;
            case 1:
                Instantiate(List, buttonPos);
                break;
            case 2:
                Instantiate(Input, buttonPos);
                break;
            default:
                break;
        }
    }

}
