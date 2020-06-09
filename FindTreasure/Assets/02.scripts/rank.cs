using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Text UI 사용
using UnityEngine.UI;
// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class rank : MonoBehaviour
{
    private const string leaderboardId = "CgkIx5W8ioMMEAIQAQ";
    public long sco =0;
    public Text scoreText;

    public void test()
    {
        sco += 1;
        scoreText.text = string.Format("score : {0}", sco);
    }

    public void send()
    {
        Social.ReportScore(sco, leaderboardId, (bool _send) =>
          {
              if (_send == true)
              {
                  sco = 0;
              }
          });
    }

    public void my()
    {
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboardId);
    }
}
