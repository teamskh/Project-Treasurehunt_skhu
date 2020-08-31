using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using TTM.Classes;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    GameObject OX;

    Transform qtxt;
    Transform buttonPos;

    private void Awake()
    {
        qtxt = transform.Find("qtxt");
        buttonPos = qtxt?.GetChild(0);
    }

    public void Init(Q item)
    {
        int kind = 0;// item.Kind.Value;
        qtxt.GetComponent<TextMesh>().text = item.Str;
         
        switch (kind)
        {
            case 0:
                Instantiate(OX, buttonPos);
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

}
