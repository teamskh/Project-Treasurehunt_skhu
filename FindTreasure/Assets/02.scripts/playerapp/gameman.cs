using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class gameman : MonoBehaviour
{
    public AudioSource baaudio;
    public AudioSource sfaudio;
    //public Image[] img;
    public int score = 0;
    //public GameObject inss;
    public string imageText="a"; //문제,답 내용 결정

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
        baaudio.volume = PlayerPrefs.GetFloat("backvol", 1f);
        sfaudio.volume = PlayerPrefs.GetFloat("sfxvol", 1f);
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void Load()
    {
        userna.text = PlayerPrefs.GetString("id");
    }

}
