using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Attack,
    Skill1,
    ChargeSkill,
    Die,
    Idle,
    Injured,
    Jump,
    Fall,
    LevelUp,
    MainSkill,
    Run,
    Walk
}
public enum Skins
{
    Lv1,Lv5,Lv10
}
    public class Animation : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string attackAnimationName;
    [SpineAnimation]
    public string skill_1_AnimationName;

    [SpineAnimation]
    public string chargeSkillAnimationName;

    [SpineAnimation]
    public string dieAnimationName;

    [SpineAnimation]
    public string injuredAnimationName;

    [SpineAnimation]
    public string levelUpAnimationName;

    [SpineAnimation]
    public string mainSkillAnimationName;

    [SpineAnimation]
    public string runAnimationName;

    [SpineAnimation]
    public string idleAnimationName;


    [SpineAnimation]
    public string fallAnimationName;

    [SpineAnimation]
    public string jumpAnimationName;


    #endregion

    public SkeletonAnimation skeletonAnimation;
    public static Animation instance;
    public State state; public Skins skin;
    public string stateString;
    

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    State previousState; Skins previousSkin;
    float chargedTime, skillTime, intervalTime, spawnEffectTime;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {
        // Make sure you get these AnimationState and Skeleton references in Start or Later.
        // Getting and using them in Awake is not guaranteed by default execution order.
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        State currentModelState = state;

        if (previousState != currentModelState)
        {
          
            SetState(GetStringState());

        }

        previousState = currentModelState;
        Skins currentSkin = skin;
        if (previousSkin!=currentSkin)
        {
            skeleton.SetSkin((GetStringSkin()));
        }
        previousSkin=currentSkin;

        MainSkill(); //cập nhật mainskill liên tục
        
    }
    public string GetStringState ()
    {
        if (state == State.Idle)
        {
            return "Idle";
        }
        else if (state == State.ChargeSkill) { return "ChargeSkill"; }
        else if (state == State.Die) { return "Die"; }
        else if (state == State.Injured) { return "Injured"; }
        else if (state == State.Jump) { return "Jump"; }
        else if (state == State.Fall) { return "Fall"; }
        else if (state == State.LevelUp) { return "LevelUp"; }
        else if (state == State.MainSkill) { return "MainSkill"; }
        else if (state == State.Walk) { return "Walk"; }
        else if (state == State.Run) { return "Run"; }
        else if (state == State.Skill1) { return "Skill1"; }
        else  { return "Attack"; }

    }
    public string GetStringSkin()
    {
        if (skin==Skins.Lv1)
        {
            return "Lv1";
        }
        else if (skin == Skins.Lv5)
        {
            return "Lv5";
        }
        else 
        {
            return "Lv10";
        }
    }
    public void SetState(string a)
    {
        if (a == "Idle")
        {
            spineAnimationState.SetAnimation(0, idleAnimationName, true);
            AudioManager.instance.StopSound();
        }
        if (a == "ChargeSkill")
        {
            chargedTime = 0;
            spineAnimationState.SetAnimation(0, chargeSkillAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.chargeSkill);
        }
        if (a == "Die")
        {
            spineAnimationState.SetAnimation(0, dieAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.die);
            ParticleManager.instance.SpawnDie(transform.position);
        }
        if (a == "Injured")
        {
            spineAnimationState.SetAnimation(0, injuredAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(injuredAnimationName).Duration);
            AudioManager.instance.PlaySound(AudioManager.instance.injured);

        }
        if (a == "LevelUp")
        {
            spineAnimationState.SetAnimation(0, levelUpAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(levelUpAnimationName).Duration);
            AudioManager.instance.PlaySound(AudioManager.instance.levelUp);
            ParticleManager.instance.SpawnLevel(transform.position);
        }
        if (a == "MainSkill")
        {
            spineAnimationState.SetAnimation(0, mainSkillAnimationName, false);
            if (PlayerController.instance.characterType==CharacterType.Melee)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.mainSkill_Melee, true);
            }
            else { AudioManager.instance.PlaySound(AudioManager.instance.mainSkill_Range, true); }
        }
       
        if (a == "Run")
        {
            spineAnimationState.SetAnimation(0, runAnimationName, true);
            AudioManager.instance.PlaySound(AudioManager.instance.run);
        }
        if (a == "Jump")
        {
            spineAnimationState.SetAnimation(0, jumpAnimationName, false);
            spineAnimationState.AddAnimation(0, fallAnimationName, true,0);
            AudioManager.instance.PlaySound(AudioManager.instance.jump);
        }
        if (a == "Fall")
        {
            spineAnimationState.SetAnimation(0, fallAnimationName, true);
        }
        if (a == "Attack")
        {
            spineAnimationState.SetAnimation(0, attackAnimationName, false);
           if (PlayerController.instance.characterType==CharacterType.Melee)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.attack_Melee);
                ParticleManager.instance.SpawnAttack();
            }
           else { AudioManager.instance.PlaySound(AudioManager.instance.attack_Range); }
            
        }
        if (a == "Skill1")
        {
            spineAnimationState.SetAnimation(0, skill_1_AnimationName, false);
            if (PlayerController.instance.characterType == CharacterType.Melee)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.skill1_Melee);
            }
            else { AudioManager.instance.PlaySound(AudioManager.instance.skill1_Range); }
            ParticleManager.instance.SpawnSkill_1();
        }

    }
    void DelaySetStateIdle()
    {
        state=State.Idle;
    }
    public float GetTimeOfAttackAnimation()
    {
        return skeleton.Data.FindAnimation(attackAnimationName).Duration;
    }
    public float GetTimeOfSkill_1_Animation()
    {
        return skeleton.Data.FindAnimation(skill_1_AnimationName).Duration;
    }
    public float GetTimeOfJumpAnimation()
    {
        return skeleton.Data.FindAnimation(jumpAnimationName).Duration;
    }

    void MainSkill()
    {
        if (Input.GetMouseButton(0) &&( state == State.ChargeSkill|| state == State.MainSkill))
        {
           
            chargedTime += Time.deltaTime; 
            if (chargedTime > skeleton.Data.FindAnimation(chargeSkillAnimationName).Duration)
            {
                //kích hoạt effect liên tục
                spawnEffectTime += Time.deltaTime;
                if (spawnEffectTime>0.5f)
                {
                    ParticleManager.instance.SpawnSkill(transform.position);
                    spawnEffectTime = 0;
                }
                if (state != State.MainSkill)
                {
                    state = State.MainSkill;
                    if (PlayerController.instance.characterType == CharacterType.Melee)
                    {
                        PlayerController.instance.skillMeleeGameObject.SetActive(true);
                    }
                    else { PlayerController.instance.skillRangeGameObject.SetActive(true); }
                }
                else
                {
                    if (PlayerController.instance.isIntervalSkill) { intervalTime = 0; } //Bật tắt interval
                    else
                    {
                        intervalTime += Time.deltaTime;
                        if (intervalTime > 0.1f) 
                        {
                            PlayerController.instance.isIntervalSkill = true;
                           
                            intervalTime = 0;
                        }
                    }
                    skillTime += Time.deltaTime;

                    if (skillTime < skeleton.Data.FindAnimation(mainSkillAnimationName).Duration)
                    {
                        
                        PlayerController.instance.p_currentManaFloat = PlayerController.instance.p_currentManaFloat -
                       PlayerController.instance.p_manaCostMainSkill * 0.02f / skeleton.Data.FindAnimation(mainSkillAnimationName).Duration;

                        PlayerController.instance.p_currentManaFade = PlayerController.instance.p_currentManaFade -
                       PlayerController.instance.p_manaCostMainSkill * 0.02f / skeleton.Data.FindAnimation(mainSkillAnimationName).Duration;

                    }
                    else
                    {
                        state = State.Idle; skillTime = 0; chargedTime = 0;
                    }
                }

            }

        }
        else
        {
            if(PlayerController.instance.characterType==CharacterType.Melee)
            {
                PlayerController.instance.skillMeleeGameObject.SetActive(false);
            }
            else { PlayerController.instance.skillRangeGameObject.SetActive(false); }
            chargedTime = 0; skillTime = 0;
        }
    }
    public void SetupSkins(int level)
    {
        if(level<5)
        {
            skin=Skins.Lv1;
        }
        else if(level<10)
        {
            skin = Skins.Lv5;
        }
        else { skin = Skins.Lv10; }
    }

}
