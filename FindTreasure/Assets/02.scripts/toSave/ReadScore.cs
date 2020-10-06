using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadScore : MonoBehaviour
{
    private static event Action UpdateScore;

    private void OnEnable()
    {
        UpdateScore = () =>GetComponent<Text>().text = Player.Instance.score.ToString();
    }

    public static void CallUpdate()
    {
        UpdateScore();
    }
}
