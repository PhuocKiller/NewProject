using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Monster
{
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        newItem = GetComponent<CreateItems>();
    }
    void Start()
    {
        m_maxHealth = 5000; m_currentHealth = m_maxHealth;
        m_attack = 300; m_defend = 100;
        isLive = true;
        moveSpeed = 2f;
    }
    private void FixedUpdate()
    {
        if (isLive)
        {

            if (!isStun)
            {
                if (!isDetect)
                {
                    rigid.velocity = new Vector2(moveSpeed, 0);
                    animator.SetBool("run", false);
                }
                else { animator.SetBool("run", true); 
                    rigid.velocity = new Vector2(3 * moveSpeed, 0); } //khi phát hiện thì tăng tốc
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
        healthBarMonster.UpdateBar(m_currentHealth, m_maxHealth);
        UIMonster.GetComponent<RectTransform>().transform.localScale = new Vector2(transform.localScale.x, 1);
    }
}
