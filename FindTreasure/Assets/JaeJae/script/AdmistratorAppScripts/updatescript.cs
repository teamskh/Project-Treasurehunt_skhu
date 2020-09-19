using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class updatescript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {

            string name = BackSpace.Instance.Pop().ToString();
            Debug.Log(BackSpace.Instance.ToString());
            SceneManager.LoadScene(name);
        }
    }
}
