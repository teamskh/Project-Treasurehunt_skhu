using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxmusic : MonoBehaviour
{
    public void Go()
    {
        gameman.Instance.sfaudio.Play();
    }
}
