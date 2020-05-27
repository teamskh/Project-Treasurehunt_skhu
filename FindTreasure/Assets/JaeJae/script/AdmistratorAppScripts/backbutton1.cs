using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backbutton1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()

    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))

            {
                Application.LoadLevel("ContestList");
            }
        }
        if (Input.GetKey(KeyCode.Escape))

        {
            Application.LoadLevel("ContestList");
        }
    }
}
