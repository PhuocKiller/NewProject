using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class BossAI : Monster
    
{
    private void Awake()
    {
            rigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            newItem = GetComponent<CreateItems>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_maxHealth = 200; m_currentHealth = m_maxHealth;
        m_attack = UnityEngine.Random.Range(140,150); m_defend = UnityEngine.Random.Range(90,100);
        isLive = true;
        moveSpeed = 2f;
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
                else { rigid.velocity = new Vector2(-3 * moveSpeed, 0); } //khi phát hiện thì tăng tốc
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
    public override void MonsterBeingAttacked(int damage)
    {
        if (isLive)
        {
            UIMonster.ShowDamage(damage);
            m_currentHealth = m_currentHealth - damage;
            if (m_currentHealth <= 0)
            {
                MonsterDie();
            }
        }
    }
    public override void MonsterDie()
    {
        Debug.Log("you win");
    }
}
