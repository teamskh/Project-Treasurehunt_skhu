using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csSceneTrans : MonoBehaviour
{
    public void SceneTrans0_1()
    {
        Application.LoadLevel("02.memu");
    }
    public void SceneTrans1_1()
    {
        Application.LoadLevel("03.setting");
    }
    public void SceneTrans2_1()
    {
        Application.LoadLevel("04-0.select play");
    }
    public void SceneTrans2_2()
    {
        Application.LoadLevel("04-1.play");
    }
    public void SceneTrans2_3()
    {
        Application.LoadLevel("04-2.play");
    }
    public void SceneTrans3_1()
    {
        Application.LoadLevel("05.record");
    }
    public void SceneTrans4_1()
    {
        Application.LoadLevel("06.clear");
    }
}
