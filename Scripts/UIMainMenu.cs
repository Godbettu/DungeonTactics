using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public GameObject PanelSetting;
    public AudioSource Music;
    public AudioSource Sound;

    public GameObject Musicbackground;
    public GameObject Musiccheckmark;
    public GameObject Soundbackground;
    public GameObject Soundcheckmark;

    public Slider SoundSlider;
    public Toggle SoundToggle;
    public Slider MusicSlider;
    public Toggle MusicToggle;

    private void Start()
    {
        SoundSlider.value = Sound.volume;
        SoundToggle.isOn = Sound.volume > 0.01f;

        MusicSlider.value = Music.volume;
        MusicToggle.isOn = Music.volume > 0.01f;

        UpdateSoundUI();
        UpdateMusicUI();
    }

    public void OnClickSetting(bool value)
    {
        PanelSetting.SetActive(value);
    }

    public void OnClickCloseSetting(bool value)
    {
        PanelSetting?.SetActive(false);
    }

    public void OnValueChangeSound(float value)
    {
        Sound.volume = value;
        Sound.mute = value <= 0.01f;

        SoundToggle.isOn = value > 0.01f;
        UpdateSoundUI();
    }

    public void OnClickMuteSound(bool value)
    {
        SoundToggle.isOn = value;
        Sound.mute = !value;

        Sound.volume = value ? SoundSlider.value : 0f;
        SoundSlider.value = Sound.volume;

        UpdateSoundUI();
    }

    private void UpdateSoundUI()
    {
        bool isOn = SoundToggle.isOn;

        Soundcheckmark?.SetActive(isOn);
        Soundbackground?.SetActive(!isOn);
    }

    public void OnValueChangeMusic(float value)
    {
        Music.volume = value;
        Music.mute = value <= 0.01f;

        MusicToggle.isOn = value > 0.01f;
        UpdateMusicUI();
    }

    public void OnClickMuteMusic(bool value)
    {
        MusicToggle.isOn = value;
        Music.mute = !value;

        Music.volume = value ? MusicSlider.value : 0f;
        MusicSlider.value = Music.volume;

        UpdateMusicUI();
    }

    private void UpdateMusicUI()
    {
        bool isOn = MusicToggle.isOn;

        Musiccheckmark?.SetActive(isOn);
        Musicbackground?.SetActive(!isOn);
    }
}
