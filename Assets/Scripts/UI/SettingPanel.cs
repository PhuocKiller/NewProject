using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    public Slider musicSlider, soundSlider;
    public GameObject musicOnButton, musicOffButton, soundOnButton, soundOffButton;
   
    public void UpdateMusicVolume()
    {
        AudioManager.instance.musicSource.volume = musicSlider.value;
        musicOnButton.SetActive(true);
    }
    public void UpdateSoundVolume()
    {
        AudioManager.instance.soundSource.volume = soundSlider.value;
        soundOnButton.SetActive(true);
       if (!AudioManager.instance.soundSource.isPlaying)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        }
    }
    public void MusicOn()
    {
        musicOnButton.SetActive(false);
        AudioManager.instance.musicSource.volume = 0;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void MusicOff()
    {
        musicOnButton.SetActive(true);
        AudioManager.instance.musicSource.volume = musicSlider.value;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);

    }
    public void SoundOn()
    {
        soundOnButton.SetActive(false);
        AudioManager.instance.soundSource.volume = 0;
    }
    public void SoundOff()
    {
        soundOnButton.SetActive(true);
        AudioManager.instance.soundSource.volume = soundSlider.value;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void CheckSoundVolume() //check sound khi rê thanh trượt sound
    {
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
}
