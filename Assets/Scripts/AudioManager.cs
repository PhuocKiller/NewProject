using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip theme,attack_Melee,attack_Range,skill1_Melee,skill1_Range, chargeSkill, die, idle, injured, jump, fall,
        levelUp, mainSkill_Melee,mainSkill_Range, run,error,reFillPotion,goldDrop, buyItem, clickButton, pauseGame,unpauseGame;
    public AudioSource  themeSource, vfxAudioSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        themeSource.clip = theme;
        themeSource.volume = 0.1f;
        themeSource.loop = true;
        themeSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
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
