using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rank : MonoBehaviour
{
    public Text Name;
    public InputField ConTestNa;

    private void Start()
    {
        Name.text = PlayerPrefs.GetString("nickna");
    }

    public void Test()
    {
        PlayerPrefs.SetString("ConName",ConTestNa.ToString());
        PlayerPrefs.SetInt("Score", gameman.Instance.score);
    }
}
