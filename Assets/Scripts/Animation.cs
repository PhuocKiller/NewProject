using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum State
{
    Attack,
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
    public class Animation : MonoBehaviour
{
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string attackAnimationName;

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
    public State state;
    public string stateString;
    

    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;
    State previousState;
    float chargedTime, skillTime, intervalTime;
    
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
        else  { return "Attack"; }

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
            AudioManager.instance.PlaySound(AudioManager.instance.chargeSkill, 1);
        }
        if (a == "Die")
        {
            spineAnimationState.SetAnimation(0, dieAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.die, 1);
        }
        if (a == "Injured")
        {
            spineAnimationState.SetAnimation(0, injuredAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(injuredAnimationName).Duration);
            AudioManager.instance.PlaySound(AudioManager.instance.injured, 1);

        }
        if (a == "LevelUp")
        {
            spineAnimationState.SetAnimation(0, levelUpAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(levelUpAnimationName).Duration);
            AudioManager.instance.PlaySound(AudioManager.instance.levelUp, 1);
        }
        if (a == "MainSkill")
        {
            spineAnimationState.SetAnimation(0, mainSkillAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.mainSkill, 1,true);
        }
       
        if (a == "Run")
        {
            spineAnimationState.SetAnimation(0, runAnimationName, true);
            AudioManager.instance.PlaySound(AudioManager.instance.run, 1);
        }
        if (a == "Jump")
        {
            spineAnimationState.SetAnimation(0, jumpAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.jump, 1);
        }
        if (a == "Fall")
        {
            spineAnimationState.SetAnimation(0, fallAnimationName, true);
        }
        if (a == "Attack")
        {
            spineAnimationState.SetAnimation(0, attackAnimationName, false);
            AudioManager.instance.PlaySound(AudioManager.instance.attack, 1);
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
                       PlayerController.instance.p_manaOfSkill * 0.02f / skeleton.Data.FindAnimation(mainSkillAnimationName).Duration;

                        PlayerController.instance.p_currentManaFade = PlayerController.instance.p_currentManaFade -
                       PlayerController.instance.p_manaOfSkill * 0.02f / skeleton.Data.FindAnimation(mainSkillAnimationName).Duration;

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


}
