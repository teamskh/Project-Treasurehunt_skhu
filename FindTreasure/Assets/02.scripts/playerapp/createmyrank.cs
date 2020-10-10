using GooglePlayGames.BasicApi.Multiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createmyrank : MonoBehaviour
{
    public GameObject CompetR;
    private List<string> curlist;
    private List<GameObject> buttons = new List<GameObject>();
    public List<string> ConName;

    private void Awake()
    {
        curlist = PlayerContents.Instance.CompetitionList();
        foreach (string title in curlist)
        {
            GameObject b = Instantiate(CompetR, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count + 1) * 60);
            foreach (Text t in b.GetComponentsInChildren<Text>())
            {
                if (t.gameObject.layer == 0) //대회이름
                {
                    t.text = "0";
                    ConName.Add(title);
                }
                else if(t.gameObject.layer == 9) //시간 
                {
                    t.text = title;
                    ConName.Add(title);
                }
                else if (t.gameObject.layer == 5) //점수
                {
                    t.text = title;
                    ConName.Add(title);
                }
                
            }
        }
    }
}


