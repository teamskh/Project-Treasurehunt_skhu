using BackEnd;
using System.Collections;
using System.Collections.Generic;
using TTM.Classes;
using UnityEngine;

public class CompetitionFromServer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(() =>
        {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();

                bro = Backend.BMember.CustomLogin("Admin", "toomuch");
                if (bro.IsSuccess())
                {
                    Debug.Log("Custom Login Success");
                }
                else
                    Debug.Log(bro.ToString());

                PCompetitionDictionary dic = new PCompetitionDictionary();
                dic.GetCompetitions();
                foreach (var key in dic.Keys)
                {
                    Debug.Log(key);
                }
            }

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
