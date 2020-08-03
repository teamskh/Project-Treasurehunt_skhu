using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class push : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Backend.Android.PutDeviceToken();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Psuh()
    {
        Backend.Android.PutDeviceToken();
    }

    public void noPush()
    {
        Backend.Android.DeleteDeviceToken();
    }
   
}
