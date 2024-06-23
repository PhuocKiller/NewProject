using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBossView : MonoBehaviour
{
    BoxCollider2D attackBoss;
    Monster mon;
    public GameObject attackBossGameObject;
    Vector2 pos;
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        attackBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (mon.monsType==MonsterType.Boss)
        {
            if (collision.gameObject.CompareTag("Player") && BossAnimation.instance.bossState != BossState.Attack
           && ((float)mon.m_currentHealth / (float)mon.m_maxHealth) >= 0.5f && mon.isLive)
            {
                BossAnimation.instance.bossState = BossState.Attack;
                Invoke("DelayActiveAttackBossGameObject", 0.2f);
                Invoke("DelayDeactiveAttackBossGameObject", BossAnimation.instance.GetTimeOfAttackBoss());
            }
        }
       else if (mon.monsType == MonsterType.Wizard && collision.gameObject.CompareTag("Player"))
        {
            Invoke("DelayActiveAttackBossGameObject", 0.2f);
            Invoke("DelayDeactiveAttackBossGameObject", 3);
        }
    }
    void DelayActiveAttackBossGameObject ()
    {
        attackBossGameObject.SetActive(true);
        pos= attackBossGameObject.transform.position;

        attackBossGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -30);
    }
    void DelayDeactiveAttackBossGameObject()
    {
        attackBossGameObject.SetActive(false);
        attackBossGameObject.transform.position=pos;
        attackBossGameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
