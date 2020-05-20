using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Slider backVolume;

    public Slider sfxVolume;

    private float backVol = 1f;
    private float sfxVol = 1f;

    private void Start()
    {
        backVol = PlayerPrefs.GetFloat("backvol", 1f);
        backVolume.value = backVol;
        gameman.Instance.baaudio.volume = backVolume.value;

        sfxVol = PlayerPrefs.GetFloat("sfxvol", 1f);
        sfxVolume.value = sfxVol;
        gameman.Instance.sfaudio.volume = sfxVolume.value;
    }

    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        gameman.Instance.baaudio.volume = backVolume.value;
        backVol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backVol);

        gameman.Instance.sfaudio.volume = sfxVolume.value;
        sfxVol = sfxVolume.value;
        PlayerPrefs.SetFloat("sfxvol", sfxVol);
    }
    
}
