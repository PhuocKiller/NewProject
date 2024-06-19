using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoss : MonoBehaviour
{
    BoxCollider2D attackBoss;
    Monster mon;
   
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        attackBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mon.PlayerBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend,
                2, MechanicDamage.instance.DecreaseDamageMonster()) );
           
        }
    }
    

}
