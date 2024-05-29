using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip theme,attack, chargeSkill, die, idle, injured, jump, fall, levelUp, mainSkill, run;
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
        themeSource.volume = 0.3f;
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
   /* public void PlaySound(AudioClip clip, float volume)
    {
        if (clip == this.attack)
        {
            Play(clip, volume);
            return;
        }
        if (clip==this.chargeSkill)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.die)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.idle)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.injured)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.jump)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.fall)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.levelUp)
        {
            Play(clip,volume);
            return;
        }
        if (clip == this.mainSkill)
        {
            Play(clip, volume);
            return;
        }
        if (clip == this.run)
        {
            Play(clip,volume);
            return;
        }
    }*/
    public void StopSound()
    {
        vfxAudioSource.Stop();
    }
    
}
