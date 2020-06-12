using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenechange :MonoBehaviour
{
    public void changeMainScene()
    {
        SceneManager.LoadScene("02.Main");
    }

    public void changeSettingScene()
    {
        SceneManager.LoadScene("03.Setting");
    }

    public void changeadminScene()
    {
        SceneManager.LoadScene("ContestList");
    }
}
