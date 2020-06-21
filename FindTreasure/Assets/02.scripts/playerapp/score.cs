using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class score : MonoBehaviour
{
    public GameObject bar;
    public Text Time;
    public Text Score;

    public GameObject Ending; //끝 팝업
    public GameObject EndingTime; //시간 남으면 시간만
    public GameObject EndingScore; //시간 지나면 점수만

    // Update is called once per frame
    void Update()
    {
        //Score.text = gameman.Instance.score.ToString();
        
        Time.text = gameman.Instance.time;
        Score.text = gameman.Instance.score.ToString();

        if ((int.Parse(Score.ToString()) == 50)||(int.Parse(Time.ToString())<1)) //최대 점수 대회마다 다름;; -> ??
        {
            PlayerPrefs.SetString("Name", Time.ToString());//대회 이름,,?
            bar.SetActive(false);
            Ending.SetActive(true);
            if (int.Parse(Time.ToString()) < 1) //이게 될까?
            {
                PlayerPrefs.SetInt("Score", int.Parse(Score.text));
                EndingTime.SetActive(false);
            }
                
            else if (int.Parse(Score.text) == 50)
            {
                PlayerPrefs.SetString("Times", Time.ToString());
                EndingScore.SetActive(false);
            }
        }
    }

}
