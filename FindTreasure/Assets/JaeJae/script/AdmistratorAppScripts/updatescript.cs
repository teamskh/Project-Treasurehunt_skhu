using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class updatescript : MonoBehaviour
{
    private bool timer=true;
    static float time=0f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))//Application.platform==RuntimePlatform.Android&&
        {
            time -= Time.deltaTime;
            Debug.Log("뒤로가기x");

            if (time > 0)
            {
                timer = false;
                Debug.Log(time);
            }
            else
            {
                timer = true;
                time = 0.1f;
                if (BackSpace.Instance.Count == 0)
                    {
                        gameObject.GetComponent<PanelScript>().setNumber(0);
                    }

                    string name = BackSpace.Instance.Pop().ToString();
                    Debug.Log(BackSpace.Instance.ToString());
                    SceneManager.LoadScene(name);
                Debug.Log("뒤로가기o");
            }
        }
       
    }
}
