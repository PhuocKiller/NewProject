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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mon.PlayerBeingAttacked(mon.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend) * 2 * UnityEngine.Random.Range(0.8f, 1.2f));
           
        }
    }
    

}
