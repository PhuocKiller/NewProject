using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBossView : MonoBehaviour
{
    BoxCollider2D SkillBoss;
    Monster mon;
    public GameObject skillBossGameObject;
    Vector2 pos;
    private void Awake()
    {
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        SkillBoss = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && BossAnimation.instance.bossState != BossState.Skill
            &&(((float)mon.m_currentHealth/(float)mon.m_maxHealth)<0.5f)&&mon.isLive)
        {
            BossAnimation.instance.bossState = BossState.Skill;
            Invoke("DelayActiveSkillBossGameObject", 0.2f);
            Invoke("DelayDeactiveSkillBossGameObject", BossAnimation.instance.GetTimeOfSkillBoss());
        }
    }
    void DelayActiveSkillBossGameObject()
    {
        skillBossGameObject.SetActive(true);
        skillBossGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 25);
        Invoke("DownSkillGameObject", 0.6f);
    }
    void DelayDeactiveSkillBossGameObject()
    {
        skillBossGameObject.SetActive(false);
        skillBossGameObject.transform.position = mon.transform.position;
        skillBossGameObject.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
        CancelInvoke();
    }
    void DownSkillGameObject()
    {
        skillBossGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10.8f);
        Invoke("UpSkillGameObject", 0.45f);
    }
    void UpSkillGameObject()
    {
        skillBossGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);
        Invoke("DownSkillGameObject", 0.23f);
    }
}
