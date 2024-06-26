using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BodyMonster : MonoBehaviour
{
    CapsuleCollider2D capsule;
    Monster mon;
    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
    }
  
    private void OnTriggerStay2D(Collider2D collision)
    {
        bool isPlayerCrit;
        
        if (PlayerController.instance.isRage)
        {
            isPlayerCrit = MechanicDamage.instance.GetChance(PlayerController.instance.p_RageCritChance);
        }
        else
        {
            isPlayerCrit = MechanicDamage.instance.GetChance(PlayerController.instance.p_CritChance);
        }
        if (collision.CompareTag("Attack")  && PlayerController.instance.isAttackExactly) //Monster bị tấn công bởi kiếm hoặc cung
        {
            PlayerController.instance.isAttackExactly = false;
            PlayerController.instance.IncreaseRageFloat(1, isPlayerCrit);
            mon.MonsterBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(PlayerController.instance.GetAttack(), mon.m_defend,
                MechanicDamage.instance.IncreaseDamagePlayer(isPlayerCrit), 1), isPlayerCrit);
            ParticleManager.instance.SpawnBlood(transform.position);
        }
        if ((collision.CompareTag("Skill1") && PlayerController.instance.isAttackExactly)) //Monster bị tấn công bởi skill_1
        {
            PlayerController.instance.isAttackExactly = false;
            PlayerController.instance.IncreaseRageFloat(3, isPlayerCrit);
            mon.MonsterBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(PlayerController.instance.GetAttack(), mon.m_defend,
                MechanicDamage.instance.IncreaseDamagePlayer(isPlayerCrit), 1), isPlayerCrit);
            ParticleManager.instance.SpawnBlood(transform.position);
        }
        if (collision.CompareTag("Player")&& !PlayerController.instance.beImmortal)          //Player bị tấn công
        {
            float timeDelayImmortal;
            if (mon.monsType==MonsterType.Boss)
            {
                timeDelayImmortal = 0.5f;
            }
            else if (mon.monsType == MonsterType.Wizard)
            {
                timeDelayImmortal = 1f;
            }
            else { timeDelayImmortal = 3f; }
            mon.PlayerBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.GetDefend(),1,
                MechanicDamage.instance.DecreaseDamageMonster()), timeDelayImmortal);
            
        }
        if (collision.CompareTag("Skill") && PlayerController.instance.isIntervalSkill)
        {
            mon.isStun = true;
            PlayerController.instance.IncreaseRageFloat(1f, isPlayerCrit);
            mon.MonsterBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(PlayerController.instance.GetAttack(), mon.m_defend,
                MechanicDamage.instance.IncreaseDamagePlayer(isPlayerCrit), 1), isPlayerCrit);
            PlayerController.instance.isIntervalSkill = false;
            ParticleManager.instance.SpawnBlood(transform.position);
        }
    }
  
}
