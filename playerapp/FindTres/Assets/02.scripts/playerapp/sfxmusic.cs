using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxmusic : MonoBehaviour
{
    public void start()
    {
        gameman.Instance.sfaudio.Play();
    }
}
