using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer mixer;
    void Start()
    {
        volumeSlider.value = 0.5f;
    }

    void Update()
    {
        
    }

    public void ValueChanged() {
        SetVolume("Master", volumeSlider.value);
    }

    void SetVolume(string name, float value) {
        float volume = Mathf.Log10(value) * 20;
        if (value == 0)
            volume = -80;

        mixer.SetFloat(name, volume);
    }
}
