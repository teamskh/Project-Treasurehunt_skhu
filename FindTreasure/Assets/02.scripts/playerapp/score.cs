using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public Text txt; // gameman에서 저장된 점수 갖고 오기
    public Text endtxt; //클리어 창에 나올 총점수
    public GameObject end; //클리어 창

    public float LimiTime; //제한시간
    public bool star = false; //시작 확인
    public Text timer; //시간
    public GameObject bar; // 뜨는 시간
    public Text endtimerbar; // 상단에 남은시간
    public GameObject endtime; //남은시간있다면 켜지고 아니라면 안나옴

    public Text user;

    // Start is called before the first frame update
    public void Start()
    {
        txt.text = "0";
        endtxt.text = "0";
        user.text = gameman.Instance.userna;
        //메인 화면에 유저 이름 띄우기
    }

    public void click() //시간 세팅
    {
        star = true;
        LimiTime = 20;
        bar.SetActive(true);
        gameman.Instance.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (star == true)
        {
            if (LimiTime < 0)
            {
                bar.SetActive(false);
                end.SetActive(true);
                gameman.Instance.chek = true;
            }
            else if(gameman.Instance.score == 3)
            {
                endtimerbar.text = "" + Mathf.Round(LimiTime); //클리어창 남은시간
                bar.SetActive(false);
                endtime.SetActive(true);
                end.SetActive(true);
                gameman.Instance.chek = true;
            }
            else
            {
                txt.text = gameman.Instance.score.ToString();
                endtxt.text = txt.text;

                LimiTime -= Time.deltaTime;
                timer.text = "" + Mathf.Round(LimiTime);
            }
        }
    }

    public void close() //클리어창 닫기
    {
        if(gameman.Instance.chek == true)
            end.SetActive(false);
    }

    private void OnGUI()
    {
        string timestr;
        timestr = "" + LimiTime.ToString("00.00");
        timestr = timestr.Replace(".", ":");
        timer.text = timestr;
        endtimerbar.text = timestr;
    }
}
