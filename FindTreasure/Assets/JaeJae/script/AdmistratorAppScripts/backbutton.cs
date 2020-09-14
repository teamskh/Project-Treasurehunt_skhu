using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backbutton : MonoBehaviour
{
    // Start is called before the first frame update
void Update()

    {
            if (Input.GetKey(KeyCode.Escape))

            {
                SceneManager.LoadScene("administratorMenu");
            }
    }
}
