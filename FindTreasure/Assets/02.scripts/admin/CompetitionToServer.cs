using BackEnd;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetitionToServer : MonoBehaviour
{
    string id = "Admin";
    string pw = "toomuch";
    //서버에서 로드한 competition 저장하는 dic;
    TTM.Classes.CompetitionDictionary dic;
    //만든 버튼들 모아두는 리스트
    List<GameObject> Buttons = new List<GameObject>();

    [SerializeField] GameObject buttonprefab;
    [SerializeField] GameObject Adapter;

    public GameObject AskD;
    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(() =>
        {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();

                Debug.Log($"{id} : {pw}");

                bro = Backend.BMember.CustomLogin(id, pw);
                if (bro.IsSuccess())
                {
                    Debug.Log("Custom Login Success");
                }
                else
                    Debug.Log(bro.ToString());


                
            }
            SetList();
        });
        AskD.SetActive(false);
    }

    public void SetList()
    {
        dic = new TTM.Classes.CompetitionDictionary();
        dic.GetCompetitions();

        //버튼 관리 리스트 비우기
        foreach(var item in Buttons)
        {
            Destroy(item);
        }
        Buttons.Clear();

        //버튼 관리 리스트 채우기
        var list = dic.Keys;
        foreach(var item in list) {
            Buttons.Add(MakeCompetButton(item));
        }
    }

    GameObject MakeCompetButton(string name)
    {
        GameObject competb = Instantiate(buttonprefab, buttonprefab.transform.position, Quaternion.identity);
        
        //위치 조정
        competb.transform.SetParent(Adapter.transform, false);
        //competb.GetComponent<RectTransform>().anchoredPosition.Set(0,Buttons.Count*120);
        competb.transform.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, (Buttons.Count + 1) * 145);
        
        //글씨 조정
        competb.GetComponentInChildren<Text>().text = name;
        competb.GetComponent<Button>()?.onClick.AddListener(() =>dic.CurrentCode(competb.GetComponentInChildren<Text>().text));
        competb.GetComponent<Button>()?.onClick.AddListener(() =>FTP.AvaliablePath(new TTM.Classes.DataPath("JPG/" + name)));
       
        return competb;
    }

    public void Active()
    {
        gameObject.GetComponent<PanelScript>().setNumber(2);
    }

    public void SetActive()
    {
        gameObject.GetComponent<PanelScript>().setNumber(1);
    }
}
