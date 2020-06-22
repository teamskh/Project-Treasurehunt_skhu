using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TTM.Save;
using DataInfo;
using TTM.Classes;
using BackEnd;
using LitJson;



public class gameman : MonoBehaviour
{
    public AudioSource baaudio;
    public AudioSource sfaudio;
    public int score = 0;
    public string imageText; //문제,답 내용 결정
    public CompetitionDictionary competdic;
    public QuizDictionary quizdic;

    public Text userna;
    //페이지 이동시 저장될 유저이름

//public bool chek = false;

public string time;

    static gameman instance;
    public static gameman Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        baaudio.volume = PlayerPrefs.GetFloat(PrefsString.baaudio, 1f);
        sfaudio.volume = PlayerPrefs.GetFloat(PrefsString.sfaudio, 1f);
        Screen.fullScreen = !Screen.fullScreen;
        JsonLoadSave.LoadCompetitions(out competdic);
        JsonLoadSave.LoadQuizs(out quizdic);
    }

    public Answer CheckAnswer()
    {
        Answer ans = AnswerDictionary.GetAnswer(imageText);
        return ans;
    }

    public Quiz FindQuiz() {
        return quizdic.FindQuiz(imageText);
    }

    public void Load()
    {
        userna.text = PlayerPrefs.GetString(PrefsString.ID);
    }

    public List<string> GetList()
    {
        return competdic.getContestList();
    }

}

