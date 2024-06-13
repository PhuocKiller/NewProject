using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip attack_Melee,attack_Range,skill1_Melee,skill1_Range, chargeSkill, die, idle, injured, jump, fall,
        levelUp, mainSkill_Melee,mainSkill_Range, run,error,reFillPotion,goldDrop, buyItem, clickButton, pauseGame,unpauseGame;
    public AudioClip[] theme;
    public AudioSource  themeSource, vfxAudioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (i == SceneManager.GetActiveScene().buildIndex)
            {
                themeSource.clip = theme[i];
                themeSource.volume = 0.3f;
                themeSource.loop = true;
                themeSource.Play();
            }
        }
    }


    public void PlaySound(AudioClip clip, float volume, bool isLoop=false)
    {
        vfxAudioSource.clip = clip;
        vfxAudioSource.volume = volume;
        vfxAudioSource.loop = isLoop; 
        vfxAudioSource.Play();
    }
 
    public void StopSound()
    {
        vfxAudioSource.Stop();
    }
    
}
