using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBossView : MonoBehaviour
{
    BoxCollider2D SkillBoss;
    Monster mon;
    public GameObject skillBossGameObject;
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        SkillBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BossAnimation.instance.bossState != BossState.Skill
            &&(mon.m_currentHealth/mon.m_maxHealth)<0.5f)
        {
            BossAnimation.instance.bossState = BossState.Skill;
            Invoke("DelayActiveSkillBossGameObject", 1f);
            Invoke("DelayDeactiveSkillBossGameObject", BossAnimation.instance.GetTimeOfSkillBoss());
        }
    }
    void DelayActiveSkillBossGameObject()
    {
        skillBossGameObject.SetActive(true);
    }
    void DelayDeactiveSkillBossGameObject()
    {
        skillBossGameObject.SetActive(false);
    }
}
