﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
      //  mon.damageofPlayer = mon.GetDamageOfTwoObject(PlayerController.instance.p_Attack, mon.m_defend);
        if ((collision.CompareTag("Sword") || collision.CompareTag("Arrow")) && PlayerController.instance.isAttackExactly) //Monster bị tấn công bởi kiếm hoặc cung
        {
            PlayerController.instance.isAttackExactly = false;
            mon.MonsterBeingAttacked((int)(mon.damageofPlayer * UnityEngine.Random.Range(0.8f, 1.2f)));
        }
        if ((collision.CompareTag("Skill1") && PlayerController.instance.isAttackExactly)) //Monster bị tấn công bởi skill1
        {
            PlayerController.instance.isAttackExactly = false;
            mon.MonsterBeingAttacked((int)(mon.damageofPlayer *3* UnityEngine.Random.Range(0.8f, 1.2f)));
        }
        if (collision.CompareTag("Player"))          //Player bị tấn công
        {
            mon.PlayerBeingAttacked(mon.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend) * UnityEngine.Random.Range(0.8f, 1.2f));
        }
        if (collision.CompareTag("Skill") && PlayerController.instance.isIntervalSkill)
        {
            mon.isStun = true;
            mon.MonsterBeingAttacked((int)(0.2f * (mon.damageofPlayer) * UnityEngine.Random.Range(0.8f, 1.2f)));
            PlayerController.instance.isIntervalSkill = false;
        }
    }
    public void OnMouseDown()
    {
        UIManager.instance.ShowInfoMonster(mon);
    }
    

    
   
}
