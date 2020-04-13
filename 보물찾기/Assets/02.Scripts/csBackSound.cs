using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class csBackSound : MonoBehaviour
{
    public Slider backVolume;
    public AudioSource audio;

    private float backvol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        backvol = PlayerPrefs.GetFloat("backvol",1f);
        backVolume.value = backvol;
        audio.volume = backVolume.value;
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }

    public void SoundSlider()
    {
        audio.volume = backVolume.value;

        backvol = backVolume.value;
        PlayerPrefs.SetFloat("backvol", backvol);
    }
}
