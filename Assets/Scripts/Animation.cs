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
    public string walkAnimationName;

    

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
    float chargedTime, skillTime;
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
        Debug.Log(skeleton.Data.FindAnimation(attackAnimationName).Duration);

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
        if (Input.GetMouseButton(1))
        {
            chargedTime += Time.deltaTime;
            Debug.Log(chargedTime);
            if (chargedTime > skeleton.Data.FindAnimation(chargeSkillAnimationName).Duration)
            {
               if(state!=State.MainSkill)
                {
                    state=State.MainSkill; 
                }
                chargedTime = 0;
            }
            if (state==State.MainSkill)
            {
                skillTime += Time.deltaTime;
                PlayerController.instance.p_currentManaFloat = (float)PlayerController.instance.p_currentManaInt - 0.01f;
                PlayerController.instance.p_currentManaInt = (int)PlayerController.instance.p_currentManaFloat;

                if (skillTime > skeleton.Data.FindAnimation(mainSkillAnimationName).Duration)
                {
                    state = State.Idle;
                }
            }
        }
        else chargedTime = 0;
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
        }
        if (a == "ChargeSkill")
        {
            chargedTime = 0;
            spineAnimationState.SetAnimation(0, chargeSkillAnimationName, false);
            //spineAnimationState.AddAnimation(0, mainSkillAnimationName, false,0);

            //Invoke("DelaySetStateMainSkill", skeleton.Data.FindAnimation(chargeSkillAnimationName).Duration);

        }
        if (a == "Die")
        {
            spineAnimationState.SetAnimation(0, dieAnimationName, false);
        }
        if (a == "Injured")
        {
            spineAnimationState.SetAnimation(0, injuredAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(injuredAnimationName).Duration);
            
        }
        if (a == "LevelUp")
        {
            spineAnimationState.SetAnimation(0, levelUpAnimationName, false);
            Invoke("DelaySetStateIdle", skeleton.Data.FindAnimation(levelUpAnimationName).Duration);
        }
        if (a == "MainSkill")
        {
            spineAnimationState.SetAnimation(0, mainSkillAnimationName, false);
        }
        if (a == "Walk")
        {
            spineAnimationState.SetAnimation(0, walkAnimationName, true);
        }
        if (a == "Run")
        {
            spineAnimationState.SetAnimation(0, runAnimationName, false);
        }
        if (a == "Jump")
        {
            spineAnimationState.SetAnimation(0, jumpAnimationName, false);
        }
        if (a == "Attack")
        {
            spineAnimationState.SetAnimation(0, attackAnimationName, false);
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




}
