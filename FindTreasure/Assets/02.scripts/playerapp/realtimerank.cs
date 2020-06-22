using UnityEngine;
using LitJson;
using BackEnd;
using System.Collections.Generic;
using System;
using static BackEnd.BackendAsyncClass;
using UnityEngine.UI;

public class realtimerank : MonoBehaviour
{
    public Text[] Texrank;

    public void Start()
    {
        for (int i = 0; i < 15; i++)
            Texrank[i].text = " ";
    }

    //특정 실시간 랭킹 불러오기
    public void RTAllRank()
    {
        string ra;
        int j=0;
        BackendReturnObject BRO = Backend.RTRank.GetRTRankByUuid("db9848d0-b3be-11ea-8d65-076db9a598b3", 2); //상위 2명

        if (BRO.IsSuccess())
        {
            Debug.Log(BRO.GetReturnValue());
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            for(int i = 0; i < rows.Count; i++)
            {
                Texrank[j].text = rows[i]["nickname"].ToString();
                Texrank[j + 1].text = rows[i]["rank"][0].ToString();
                Texrank[j + 2].text = rows[i]["score"][0].ToString();
                ra = "닉네임 : " + rows[i]["nickname"].ToString() + " 순위 : " + rows[i]["rank"][0].ToString() + " 점수 : " + rows[i]["score"][0].ToString();
                Debug.Log(ra);
                j += 3;
            }
        }
        else
        {
            if (BRO.IsSuccess())
            {
                Debug.Log("구글 토큰으로 뒤끝서버 로그인 성공 - 동기 방식-");
            }
            else
            {
                switch (BRO.GetStatusCode())
                {
                    case "404":
                        Debug.Log("존재하지 않는 테이블이름");
                        break;
                    case "412":
                        Debug.Log("비활성화 된 테이블");
                        break;
                    default:
                        Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
                        break;
                }
            }
        }

    }

    //특정 실시간 랭킹에서 플레이어의 랭킹 불러오기
    //얘 사용
    public void RTMyRank()
    {
        string ra;
        int j = 0;
        BackendReturnObject BRO = Backend.RTRank.GetMyRTRank("db9848d0-b3be-11ea-8d65-076db9a598b3", 2); //나와 위 아래 2명 //uuid?????????
        BackendReturnObject BRO1 = Backend.BMember.GetUserInfo();

        if (BRO.IsSuccess())
        {
            JsonData rows = BRO.GetReturnValuetoJSON()["rows"];
            JsonData rows1 = BRO1.GetReturnValuetoJSON()["row"];

            for (int i = 0; i < rows.Count; i++)
            {
                if (rows[i]["nickname"].ToString()== rows1["nickname"].ToString())
                {
                    Debug.Log("meme");
                    Texrank[j].color = Color.blue;
                    Texrank[j+1].color = Color.blue;
                    Texrank[j+2].color = Color.blue;

                    Texrank[j].text = rows[i]["nickname"].ToString();
                    Texrank[j + 1].text = rows[i]["rank"][0].ToString();
                    Texrank[j + 2].text = rows[i]["score"][0].ToString();
                }
                else
                {
                    Texrank[j].text = rows[i]["nickname"].ToString();
                    Texrank[j + 1].text = rows[i]["rank"][0].ToString();
                    Texrank[j + 2].text = rows[i]["score"][0].ToString();
                }

                ra = "닉네임 : " + rows[i]["nickname"].ToString() + " 순위 : " + rows[i]["rank"][0].ToString() + " 점수 : " + rows[i]["score"][0].ToString();
                Debug.Log(ra);
                j += 3;
            }
        }
        else
        {
            if (BRO.IsSuccess())
            {
                Debug.Log("성공쓰");
            }
            else
            {
                switch (BRO.GetStatusCode())
                {
                    case "404":
                        Debug.Log("존재하지 않는 테이블이름");
                        break;
                    case "412":
                        Debug.Log("비활성화 된 테이블");
                        break;
                    default:
                        Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
                        break;
                }
            }
        }
    }

    //실시간 랭킹 갱신
    public void UpdateRTRank()
    {
        string rowindate;
        rowindate = Backend.BMember.GetUserInfo().GetInDate().ToString();
        
        BackendReturnObject BRO = Backend.GameInfo.UpdateRTRankTable("rank", "score", gameman.Instance.score, rowindate);
        if (BRO.IsSuccess())
        {
            Debug.Log("실시간랭킹갱신");
        }
        else
        {
            if (BRO.IsSuccess())
            {
                Debug.Log("구글 토큰으로 뒤끝서버 로그인 성공 - 동기 방식-");
            }
            else
            {
                switch (BRO.GetStatusCode())
                {
                    case "404":
                        Debug.Log("존재하지 않는 테이블이름");
                        break;
                    case "412":
                        Debug.Log("비활성화 된 테이블");
                        break;
                    default:
                        Debug.Log("서버 공통 에러 발생" + BRO.GetMessage());
                        break;
                }
            }
        }
    }
}