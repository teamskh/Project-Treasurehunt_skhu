using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttt : MonoBehaviour
{
    private static ttt s_Instance = null;
    
    // Use this for initialization

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame

    void Update()
    {
    }
    
    public static ttt instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new GameObject("Manager").AddComponent<ttt>();
                //오브젝트가 생성이 안되있을경우 생성.
            }
            return s_Instance;
        }
    }
    
    void OnApplicationQuit()
    {
        s_Instance = null;
        //게임종료시 삭제.
    }
}
