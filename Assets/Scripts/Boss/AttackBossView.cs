using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossView : MonoBehaviour
{
    BoxCollider2D attackBoss;
    Monster mon;
    public GameObject attackBossGameObject;
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        attackBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BossAnimation.instance.bossState != BossState.Attack
            && (mon.m_currentHealth / mon.m_maxHealth) >= 0.5f)
        {
            BossAnimation.instance.bossState = BossState.Attack;
            Invoke("DelayActiveAttackBossGameObject", 0.6f);
            Invoke("DelayDeactiveAttackBossGameObject", BossAnimation.instance.GetTimeOfAttackBoss());
        }
    }
    void DelayActiveAttackBossGameObject ()
    {
        attackBossGameObject.SetActive(true);
    }
    void DelayDeactiveAttackBossGameObject()
    {
        attackBossGameObject.SetActive(false);
    }
}
