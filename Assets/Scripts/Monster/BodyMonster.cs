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
        if ((collision.CompareTag("Sword") || collision.CompareTag("Arrow")) && PlayerController.instance.isAttackExactly) //Monster bị tấn công bởi kiếm hoặc cung
        {
            PlayerController.instance.isAttackExactly = false;
            mon.MonsterBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(PlayerController.instance.p_Attack, mon.m_defend,
                MechanicDamage.instance.IncreaseDamagePlayer(), 1));
        }
        if ((collision.CompareTag("Skill1") && PlayerController.instance.isAttackExactly)) //Monster bị tấn công bởi skill1
        {
            PlayerController.instance.isAttackExactly = false;
            mon.MonsterBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(PlayerController.instance.p_Attack, mon.m_defend,
                MechanicDamage.instance.IncreaseDamagePlayer(), 1));
        }
        if (collision.CompareTag("Player"))          //Player bị tấn công
        {
            mon.PlayerBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend,1,
                MechanicDamage.instance.DecreaseDamageMonster()) );
        }
        if (collision.CompareTag("Skill") && PlayerController.instance.isIntervalSkill)
        {
            mon.isStun = true;
            mon.MonsterBeingAttacked((int)(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend,
                MechanicDamage.instance.IncreaseDamagePlayer(), 1)));
            PlayerController.instance.isIntervalSkill = false;
        }
    }
  
}
