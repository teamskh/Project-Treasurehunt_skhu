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

    public Material seleted, nonseleted;

    Transform qtxt;
    Transform buttonPos;
    public string title;
    public string context;
    int kind;
    string answer;

    private void Awake()
    {
        qtxt = transform.Find("qtxt");
        buttonPos = transform.Find("Buttons");
    }

    public void Init(string name)
    {
        Q item = PlayerContents.Instance.FindQ(name);
        kind = item.Kind.Value;
        qtxt.GetComponent<TextMesh>().text = item.Str;
        title = name;
        context = item.Str;
        GameObject gameobj;
        switch (kind)
        {
            case 0:
                gameobj = Instantiate(OX, buttonPos);
                break;
            case 1:
                gameobj = Instantiate(List, buttonPos);
                Debug.Log($"List size: {item.List.Length}");
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
        switch (kind)
        {
            case 0:
            case 1:
                if (answer == ans)
                {
                    CheckAnswer();
                    return;
                }
                answer = ans;
                break;
            case 2:
                answer = ans;
                CheckAnswer();
                break;
        }
    }

    private void CheckAnswer()
    {
        Player.Instance.CheckAnswer(title, answer);
    }
}
