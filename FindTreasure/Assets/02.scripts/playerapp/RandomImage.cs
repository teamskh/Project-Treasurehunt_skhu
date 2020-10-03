using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomImage : MonoBehaviour
{
    public Sprite[] ListPro = new Sprite[3];
    public Image profile;
    private int num;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        num = rand.Next(0,3);

        switch (num)
        {
            case 0:
                profile.sprite = ListPro[0];
                break;
            case 1:
                profile.sprite = ListPro[1];
                break;
            case 2:
                profile.sprite = ListPro[2];
                break;
        }
    }
}
