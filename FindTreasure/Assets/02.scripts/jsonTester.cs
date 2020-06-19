using System.Collections;
using System.Collections.Generic;
using TTM.Save;
using UnityEngine;

public class jsonTester : MonoBehaviour
{
    // Start is called before the first frame update
    CompetitionDictionary dic;
    void Start()
    {
        init();
    }
    public void init()
    {
        JsonLoadSave.LoadCompetitions(out dic);
        string str = JsonUtility.ToJson(dic);
        Debug.Log($"{str}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
