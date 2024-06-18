using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum MonsterType 
{fly0, fly1,fly2,fly3,fly4,fly5,fly6,fly7,fly8,Boss
}

    

public class Monster : MonoBehaviour
{
    public MonsterType monsType;
    public CreateItems newItem;
    public float moveSpeed = 1f;
    
    
    public Rigidbody2D rigid;
    public Animator animator;
    public int m_level,m_currentHealth, m_maxHealth, m_attack, m_defend, m_XP, damageofPlayer,damageofMonster;
    public Bars healthBarMonster;
    public bool isLive, isStun,isDetect;
    public UIMonster UIMonster;
    public float timeStun;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        newItem = GetComponent<CreateItems>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_level = PlayerController.instance.p_Level;
        m_maxHealth = 100*m_level; m_currentHealth = m_maxHealth;
        m_attack =20+ 10*m_level;m_defend = 20+2* m_level;
        m_XP = 80+20*m_level;
        isLive = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isLive)
        {

            if (!isStun)
            {
                if (!isDetect)
                {
                    rigid.velocity = new Vector2(-moveSpeed, 0);
                }
                else { rigid.velocity = new Vector2(-2*moveSpeed, 0); } //khi phát hiện thì tăng tốc
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
        else
        {
            rigid.velocity = Vector2.zero;
        }
       healthBarMonster.UpdateBar(m_currentHealth,m_maxHealth);
       UIMonster.GetComponent<RectTransform>().transform.localScale = new Vector2(10*transform.localScale.x, 1);
    }
   
    public void PlayerBeingAttacked(float damage)
    {
        if (isLive)
        {
            PlayerController.instance.PlayerBeingAttacked(damage);

        }
    }
    public virtual void MonsterBeingAttacked(int damage)
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
    
    public virtual void MonsterDie()
    {
        m_currentHealth = 0;
        animator.SetBool("die", true);
        isLive = false;
        Invoke("DestroyMonster", 2f);
    }
    
    public void DestroyMonster()
    {
        Destroy(gameObject);
        PlayerController.instance.p_CurrentXP += m_XP;
        newItem.CreateItemsFromDeath(); //rớt vật phẩm khi chết
    }
  






}
