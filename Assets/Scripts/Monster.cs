using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum MonsterType 
{fly0, fly1,fly2,fly3,fly4,fly5,fly6,fly7,fly8
}

    

public class Monster : MonoBehaviour
{
    public static Action <Monster> beHit;
    public MonsterType monsType;
    
    public float moveSpeed = 1f;
    CapsuleCollider2D capsule;
    BoxCollider2D box;
    Rigidbody2D rigid;
    Animator animator;
    public int m_currentHealth, m_maxHealth, m_attack, m_defend, m_XP, damageofPlayer,damageofMonster;
    public Bars healthBarMonster;
    bool isLive ;
    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = 100; m_currentHealth = m_maxHealth;
        m_attack = UnityEngine.Random.Range(30, 40);m_defend = UnityEngine.Random.Range(10, 15);
        m_XP = 110;
        isLive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLive)
        {
            rigid.velocity = new Vector2(-moveSpeed, 0);
        }
       healthBarMonster.UpdateBar(m_currentHealth,m_maxHealth);
        //healthBarMonster.transform.position = new Vector2(transform.position.x,transform.position.y+2f);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       if (collision.gameObject.tag=="Ground")
        {
            moveSpeed = -moveSpeed;
            FlipEnemy();
        }
    }
    void FlipEnemy()
    {
        transform.localScale = new Vector2(0.1f*Mathf.Sign(rigid.velocity.x), 0.1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword" && PlayerController.instance.isAttackExactly)
        { 
            MonsterBeingAttacked();
        }
        if (collision.gameObject.tag =="Player")
        {
            PlayerBeingAttacked();
        }

    }
    void MonsterBeingAttacked()
    {
        animator.SetTrigger("hit");
        PlayerController.instance.isAttackExactly = false;
        damageofPlayer = PlayerController.instance.p_Attack - m_defend;
        m_currentHealth=m_currentHealth-damageofPlayer;
        if (m_currentHealth <= 0) 
        {
            MonsterDie();
            
        }
    }
    void MonsterDie()
    {
        m_currentHealth = 0;
        animator.SetBool("die", true);
        isLive = false;
        Invoke("DeactiveMonster", 2f);
        PlayerController.instance.p_CurrentXP += m_XP;
    }
    void DeactiveMonster()
    {
        gameObject.SetActive(false);
        Invoke("ActiveMonster", 3f);
    }
    void ActiveMonster()
    {
        gameObject.SetActive(true);
        isLive = true;
        animator.SetBool("die", false);
        m_currentHealth = m_maxHealth;
    }

    void PlayerBeingAttacked()
    {
        if(isLive)
        {
            if (PlayerController.instance.beImmortal) { return; }
            PlayerController.instance.beImmortal = true;
            damageofMonster = m_attack - PlayerController.instance.p_Defend;
            PlayerController.instance.p_currentHealth = PlayerController.instance.p_currentHealth - damageofMonster;
            if (PlayerController.instance.p_currentHealth < 0)
            {
                PlayerController.instance.p_currentHealth = 0;
                Animation.instance.state = State.Die;
                PlayerController.instance.isDie = true;
                return;

            }
            PlayerController.instance.DelayDeactiveImmortal();
            Animation.instance.state = State.Injured;
        }
    }




}
