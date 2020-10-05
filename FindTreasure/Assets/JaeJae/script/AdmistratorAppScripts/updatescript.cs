using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class updatescript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Application.platform==RuntimePlatform.Android&&Input.GetKey(KeyCode.Escape))
        {
            if (BackSpace.Instance.Count==0)
            {
                gameObject.GetComponent<PanelScript>().setNumber(0);
            }

            string name = BackSpace.Instance.Pop().ToString();
            Debug.Log(BackSpace.Instance.ToString());
            SceneManager.LoadScene(name);

        }
    }
}
