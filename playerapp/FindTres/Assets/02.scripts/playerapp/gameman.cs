using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class gameman : MonoBehaviour
{
    public string exam; //여러개로
    public AudioSource baaudio;
    public AudioSource sfaudio;
    public Image[] img; 

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
    }

}
