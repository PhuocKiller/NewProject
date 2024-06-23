using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip attack_Melee,attack_Range,skill1_Melee,skill1_Range, chargeSkill, die, idle, injured, jump, fall,
        levelUp, mainSkill_Melee,mainSkill_Range, run,error,reFillPotion,goldDrop, buyItem, clickButton,checkSound, pauseGame,unpauseGame;
    public AudioClip bossAttack, bossTele, bossChase, bossSkill,bossDie;
    public AudioClip[] theme;
    public AudioSource  musicSource, soundSource;
    public float musicVolume, soundVolume;
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
                musicSource.clip = theme[i];
                musicSource.loop = true;
                musicSource.Play();
            }
        }
        musicSource.volume = SavingFile.instance.gameProgress.musicVolume;
        soundSource.volume = SavingFile.instance.gameProgress.soundVolume;
    }
    public void PlaySound(AudioClip clip, bool isLoop=false)
    {
        soundSource.clip = clip;
        soundSource.loop = isLoop; 
        soundSource.Play();
    }
 
    public void StopSound()
    {
        soundSource.Stop();
    }
    
}
