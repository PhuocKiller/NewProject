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
    bool isLive, isStun;
    public UIMonster UIMonster;
    float timeStun;
    
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

            if (!isStun)
            {
                rigid.velocity = new Vector2(-moveSpeed, 0);
            }
            else
            {
                timeStun += Time.deltaTime;
                rigid.velocity = -rigid.velocity;
                if (timeStun >= 2)
                {
                    timeStun = 0; isStun = false;
                }
            }
        }
       healthBarMonster.UpdateBar(m_currentHealth,m_maxHealth);
       UIMonster.GetComponent<RectTransform>().transform.localScale = new Vector2(10*transform.localScale.x, 1);
    }
    private void OnTriggerExit2D(Collider2D collision) //xoay chiều di chuyển Monster
    {
       if (collision.gameObject.tag=="Ground")
        {
            moveSpeed = -moveSpeed;
            transform.localScale = new Vector2(0.1f * Mathf.Sign(rigid.velocity.x), 0.1f);
        }
    }
   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword") && PlayerController.instance.isAttackExactly) //Monster bị tấn công bởi kiếm
        {
            PlayerController.instance.isAttackExactly = false;
            damageofPlayer = PlayerController.instance.p_Attack - m_defend;
            MonsterBeingAttacked(damageofPlayer);
        }
        if (collision.CompareTag("Player"))          //Player bị tấn công
        {
            PlayerBeingAttacked(GetDamageOfTwoObject(m_attack,PlayerController.instance.p_Defend));
        }
        if (collision.CompareTag("Skill")&& PlayerController.instance.isIntervalSkill)
        {
            damageofPlayer = PlayerController.instance.p_Attack - m_defend;
            isStun = true;
            MonsterBeingAttacked((int)(0.1f* ((damageofPlayer ) + UnityEngine.Random.Range(-10,10))));
            PlayerController.instance.isIntervalSkill = false;
        }

    }
    int GetDamageOfTwoObject(int a,int b)
    {
        if ((a - b) < 0)
        {
            return 0;
        }
        else return a - b;
    }
    void MonsterBeingAttacked(int damage)
    {
        if (isLive)
        {
            animator.SetTrigger("hit");
            UIMonster.ShowDamage(damage);
            m_currentHealth = m_currentHealth - damage;
            if (m_currentHealth <= 0)
            {
                MonsterDie();
            }
        }
    }
    
    void MonsterDie()
    {
        m_currentHealth = 0;
        animator.SetBool("die", true);
        isLive = false;
        Invoke("DestroyMonster", 2f);
        
        
    }
    void DestroyMonster()
    {
        Destroy(gameObject);
        PlayerController.instance.p_CurrentXP += m_XP;
    }
  
    void PlayerBeingAttacked(int damage)
    {
        if(isLive)
        {
            PlayerController.instance.PlayerBeingAttacked(damage);

        }
    }
    private void OnMouseDown()
    {
        UIManager.instance.ShowInfoMonster(this);

    }




}
