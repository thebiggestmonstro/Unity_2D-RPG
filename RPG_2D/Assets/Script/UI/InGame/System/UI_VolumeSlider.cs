using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider _slider;
    public string _parameter;

    [SerializeField] 
    private AudioMixer _audioMixer;
    [SerializeField] 
    private float _multiplier;

    public void SetSliderValue(float value) => _audioMixer.SetFloat(_parameter, Mathf.Log10(value) * _multiplier);

    public void LoadSlider(float value)
    {
        if (value >= 0.001f)
            _slider.value = value;
    }
}
