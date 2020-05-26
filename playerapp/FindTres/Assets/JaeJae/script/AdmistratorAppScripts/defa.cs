using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;

public class defa : MonoBehaviour
{
    public GameObject pan;
    public int cnt = 1;

    List<GameObject> LiLi = new List<GameObject>();

    private static defa s_Instance = null;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public static defa instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new GameObject("Manager").AddComponent<defa>();
                //오브젝트가 생성이 안되있을경우 생성.
            }
            return s_Instance;
        }
    }



}
