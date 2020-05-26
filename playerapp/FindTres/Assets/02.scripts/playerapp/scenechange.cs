using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange :MonoBehaviour
{
    public void changeFirstScene()
    {
        SceneManager.LoadScene("main");
    }

    public void changeSecondScene()
    {
        SceneManager.LoadScene("sample2");
    }

    public void changeMenuScene()
    {
        SceneManager.LoadScene("02.menu");
    }

    public void changeSettingScene()
    {
        SceneManager.LoadScene("03.Setting");
    }
}
