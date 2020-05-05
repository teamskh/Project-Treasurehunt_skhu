using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange : MonoBehaviour
{
    public void changeFirstScene()
    {
        SceneManager.LoadScene("sample1");
    }

    public void changeSecondScene()
    {
        SceneManager.LoadScene("sample2");
    }
}
