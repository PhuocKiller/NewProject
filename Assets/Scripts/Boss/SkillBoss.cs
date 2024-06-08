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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mon.PlayerBeingAttacked(mon.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend) * 3 * UnityEngine.Random.Range(0.8f, 1.2f));

        }
    }
}
