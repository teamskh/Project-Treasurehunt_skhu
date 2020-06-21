using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rank : MonoBehaviour
{
    //json으로 추가되는거
    public Text ConName;
    public Text txTime;
    public Text txScore;

   public void Load()
    {
        if (PlayerPrefs.HasKey("Name")) //대회이름
        {
            ConName.text = PlayerPrefs.GetString("Name");
            txTime.text = PlayerPrefs.GetString("Times");
            txScore.text = PlayerPrefs.GetInt("Score").ToString();
        }
    }
}
