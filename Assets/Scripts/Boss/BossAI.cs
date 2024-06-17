using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class BossAI : Monster
{
    public GameObject[] posBoss;
    public float timeToChangePosition = 20f;
    int posBossIndex, newPosBossIndex;
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
        m_attack = UnityEngine.Random.Range(40,50); m_defend = UnityEngine.Random.Range(50,60);
        isLive = true;
        moveSpeed = 2f;
        InvokeRepeating("ChangePosition", 0, timeToChangePosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthBarMonster.UpdateBar(m_currentHealth, m_maxHealth);
        UIMonster.GetComponent<RectTransform>().transform.localScale = new Vector2(transform.localScale.x, 1);
        if (isLive)
        {

            if (!isStun)
            {
                if ((BossAnimation.instance.skeletonBossAnimation.state.GetCurrent(0).ToString() == "Attack"
                    || BossAnimation.instance.skeletonBossAnimation.state.GetCurrent(0).ToString() == "Skill")
                    && BossAnimation.instance.skeletonBossAnimation.state.GetCurrent(0).IsComplete==false)
                {
                    rigid.velocity = Vector3.zero; 
                    return;
                }
                else
                {
                    
                    if (!isDetect)
                    {
                        rigid.velocity = new Vector2(-moveSpeed, 0);
                        BossAnimation.instance.bossState = BossState.Walk;
                    }
                    else  //khi phát hiện thì tăng tốc
                    {
                        rigid.velocity = new Vector2(-3 * moveSpeed, 0);
                        BossAnimation.instance.bossState = BossState.Chase;
                    } 
                }



            }
            else
            {
                timeStun += Time.deltaTime;
                rigid.velocity = -rigid.velocity;
                BossAnimation.instance.bossState = BossState.Idle;
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
        
    }
    public override void MonsterBeingAttacked(int damage)
    {
        if (isLive)
        {
            UIMonster.ShowDamage(damage);
            m_currentHealth = m_currentHealth - damage;
            if (m_currentHealth <= 0)
            {
                m_currentHealth = 0;
                isLive = false;
                MonsterDie();
            }
        }
    }
    public override void MonsterDie()
    {
        BossAnimation.instance.bossState = BossState.Die;
    }
    void ChangePosition()
    {
        if(isLive)
        {
            newPosBossIndex = Random.Range(0, 4);
            if (newPosBossIndex == posBossIndex)
            {
                ChangePosition();
            }
            else
            {
                posBossIndex = newPosBossIndex;
                transform.position = posBoss[posBossIndex].transform.position;
            }
        }
    }
}
