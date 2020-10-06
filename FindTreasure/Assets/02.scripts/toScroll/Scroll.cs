using UnityEngine;
using System.Collections;
using UnityEngine.UI;
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
    public string title;
    string answer;

    private void Awake()
    {
        qtxt = transform.Find("qtxt");
        buttonPos = transform.Find("Buttons");
    }

    public void Init(string name)
    {
        Q item = PlayerContents.Instance.FindQ(name);
        int kind = item.Kind.Value;
        qtxt.GetComponent<TextMesh>().text = item.Str;
        title = name;
        GameObject gameobj;
        switch (kind)
        {
            case 0:
                gameobj = Instantiate(OX, buttonPos);
                break;
            case 1:
                gameobj = Instantiate(List, buttonPos);
                gameobj.GetComponent<ListContext>().setList(item.List);
                break;
            case 2:
                gameobj = Instantiate(Input, buttonPos);
               
                break;
            default:
                gameobj = null;
                break;
        }
    }
    
    public void setAnswer(string ans)
    {
        if (answer == ans)
        {
            CheckAnswer();
            return;
        }
        answer = ans;
    }

    private void CheckAnswer()
    {
        Player.Instance.CheckAnswer(title, answer);
    }
}
