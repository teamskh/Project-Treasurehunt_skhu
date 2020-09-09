using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class RandomProfile : MonoBehaviour
{
    public Sprite[] ListPro = new Sprite[4];
    public Image profile;
    public int num;
    public string userId;
    public char loc;

    // Start is called before the first frame update
    void Start()
    {
        userId = Social.localUser.id;
        loc = userId[userId.Length - 1];
        num = (int)char.GetNumericValue(loc);
        switch (num%4)
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
            case 3:
                profile.sprite = ListPro[3];
                break;
        }
    }
}
