using BackEnd;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAdminMode : MonoBehaviour
{
    string univName = "성공회대학교";
    // Start is called before the first frame update
    void Start()
    {
        Backend.Initialize(() =>
        {
            if (Backend.IsInitialized)
            {
                BackendReturnObject bro = new BackendReturnObject();
                bro = Backend.BMember.CustomLogin("Admin", "toomuch");
                
            }
        });
        
    }

    private bool CheckPassword(string pw)
    {
        BackendReturnObject bro = new BackendReturnObject();

        Param where = new Param();
        where.Add("univname", univName);

        bro = Backend.GameSchemaInfo.Get("univ", where, 1);
        if (bro.IsSuccess())
        {
            var data = bro.Rows();
            if (pw == data[0]["univpw"]["S"].ToString()) return true; 
        }
        return false;
    }
}
