using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBoss : MonoBehaviour
{
    BoxCollider2D skillBoss;
    Monster mon;
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        skillBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mon.PlayerBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend,3,
                MechanicDamage.instance.DecreaseDamageMonster()),true);
            PlayerController.instance.beImmortal = false;
        }
    }
}
