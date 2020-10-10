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
        time -= Time.deltaTime;
        if (Application.platform == RuntimePlatform.Android && Input.GetKey(KeyCode.Escape))
        {
            
            Debug.Log("뒤로가기x");

            if (time > 0)
            {
                timer = false;
            }
            else
            {
                timer = true;
                time = 0.05f;
                if (BackSpace.Instance.Count == 0)
                    {
                    //gameObject.GetComponent<PanelScript>().setNumber(0);
                    gameObject.GetComponent<PanelScript>().set(0);
                }

                    string name = BackSpace.Instance.Pop().ToString();
                    Debug.Log(BackSpace.Instance.ToString());
                    SceneManager.LoadScene(name);
                Debug.Log("뒤로가기o");
            }
        }
       
    }
}
