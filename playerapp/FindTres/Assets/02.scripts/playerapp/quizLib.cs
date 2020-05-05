using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class quizLib : MonoBehaviour
{
    static quizLib instance;
    public static quizLib Instance
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

    public string exam; //여러개로

}
