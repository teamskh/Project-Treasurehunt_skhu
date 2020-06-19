using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public GameObject bar;
    public Text Time;
    public Text Score;

    // Start is called before the first frame update
    public void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //Score.text = gameman.Instance.score.ToString();
        Score.text = gameman.Instance.time;
    }
}
