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
    public Player player;

    private void Awake()
    {
        curlist = Player.Instance.clearlist; //이게 맞아,,,?
        
        foreach (string title in curlist)
        {
            GameObject b = Instantiate(CompetR, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count + 1) * 60);
            foreach (Text t in b.GetComponentsInChildren<Text>())
            {
                if (t.gameObject.layer == 0) //대회이름
                {
                    t.text = "0";
                    //t.text = Player.Instance.Log.Values.ToString(); //이게 맞아,,?
                    ConName.Add(title);
                }
                else if(t.gameObject.layer == 9) //시간 
                {
                    t.text = title;
                    //t.text = Player.Instance.Log.Values.ToString(); //아닌 것 같은데,,,
                    ConName.Add(title);
                }
                else if (t.gameObject.layer == 5) //점수
                {
                    //t.text = Player.Instance.Log.Values.ToString(); //엉엉엉엉
                    ConName.Add(title);
                }
                
            }
        }
    }
}


