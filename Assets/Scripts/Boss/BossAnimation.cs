﻿using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum BossState
    {
        Attack,
        Skill,
        Idle,
        Walk,
        Chase,
        Die
    }
    public class BossAnimation : MonoBehaviour
    {
        #region Inspector
        // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
        [SpineAnimation]
        public string attackBossAnimationName;
     
        [SpineAnimation]
        public string skillBossAnimationName;


        [SpineAnimation]
        public string idleBossAnimationName;

        [SpineAnimation]
        public string walkBossAnimationName;

        [SpineAnimation]
        public string chaseBossAnimationName;

        [SpineAnimation]
        public string dieBossAnimationName;



    #endregion

    public SkeletonAnimation skeletonBossAnimation;
        public static BossAnimation instance;
        public BossState bossState;
        public string stateBossString;

    


    // Spine.AnimationState and Spine.Skeleton are not Unity-serialized objects. You will not see them as fields in the inspector.
    public Spine.AnimationState spineBossAnimationState;
        public Spine.Skeleton skeletonBoss;
        BossState previousBossState;
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
            skeletonBossAnimation = GetComponent<SkeletonAnimation>();
            spineBossAnimationState = skeletonBossAnimation.AnimationState;
        skeletonBoss = skeletonBossAnimation.Skeleton;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            BossState currentModelState = bossState;

            if (previousBossState != currentModelState)
            {

                SetState(GetStringState());

            }

        previousBossState = currentModelState;
        
        }
        public string GetStringState()
        {
            if (bossState == BossState.Idle)
            {
                return "Idle";
            }
            else if (bossState == BossState.Walk) { return "Walk"; }
            else if (bossState == BossState.Skill) { return "Skill"; }
            else if (bossState == BossState.Chase) { return "Chase"; }
            else if (bossState == BossState.Die) { return "Die"; }
            else { return "Attack"; }

        }
    public void SetState(string a)
    {
        if (a == "Idle")
        {
            spineBossAnimationState.SetAnimation(0, idleBossAnimationName, true);
        }
        if (a == "Attack")
        {
            spineBossAnimationState.SetAnimation(0, attackBossAnimationName, false);
            ParticleManager.instance.SpawnBoom1(transform.position);
            AudioManager.instance.PlayMonsterSound(AudioManager.instance.bossAttack);
        }
        if (a == "Skill")
        {
            spineBossAnimationState.SetAnimation(0, skillBossAnimationName, false);
            AudioManager.instance.PlayMonsterSound(AudioManager.instance.bossSkill);
            
        }
        if (a == "Walk")
        {
            spineBossAnimationState.SetAnimation(0, walkBossAnimationName, true);
            
        }
        if (a == "Chase")
        {
            spineBossAnimationState.SetAnimation(0, chaseBossAnimationName, true);
            ParticleManager.instance.SpawnChase(transform.position);
            AudioManager.instance.PlayMonsterSound(AudioManager.instance.bossChase);

        }
        if (a == "Die")
        {
            spineBossAnimationState.SetAnimation(0, dieBossAnimationName, false);
            AudioManager.instance.PlayMonsterSound(AudioManager.instance.bossDie);
        }
    }
    public float GetTimeOfAttackBoss()
    {
        return skeletonBoss.Data.FindAnimation(attackBossAnimationName).Duration;
    }
    public float GetTimeOfSkillBoss()
    {
        return skeletonBoss.Data.FindAnimation(skillBossAnimationName).Duration;
    }
 }
