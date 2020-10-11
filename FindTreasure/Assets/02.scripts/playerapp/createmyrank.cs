using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class createmyrank : MonoBehaviour
{
    public GameObject CompetR;
    private List<GameObject> buttons;

    private void OnEnable()
    {
        buttons = new List<GameObject>();
        var curlist = Player.Instance.Record;

        foreach (var item in curlist)
        {
            GameObject b = Instantiate(CompetR, transform);
            b.GetComponent<RectTransform>().anchoredPosition.Set(0, (buttons.Count + 1) * 60);
            foreach (Text t in b.GetComponentsInChildren<Text>())
            {
                if (t.gameObject.layer == 0) //대회이름
                {
                    t.text = item.CompetitionName;
                }
                else if (t.gameObject.layer == 9) //시간 
                {
                    var txt = item.ClearTime.ToString("yy-MM-dd tt hh:mm:ss.f");
                    t.text = txt;
                }
                else if (t.gameObject.layer == 5) //점수
                {
                    t.text = item.Score.ToString();
                }

            }
        }
    }
}


