using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Monster
{
    public AnimationState anim;
    public SnowBall snowBall;
    SnowBall snowBallInstance;
    public float shootSpeed;
    bool isShooting;
    float timeShoot;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        newItem = GetComponent<CreateItems>();
    }
    void Start()
    {
        m_maxHealth = 5000; m_currentHealth = m_maxHealth;
        m_attack = 300; m_defend = 50;
        isLive = true;
        moveSpeed = 2f;
        shootSpeed = 10f;
        isShooting = true;
    }
    private void FixedUpdate()
    {
        if (isLive)
        {

            if (!isStun)
            {
               if (!canShoot)
                {
                    animator.SetBool("attack", false);
                    CancelInvoke();
                    if (!isDetect)
                    {
                        rigid.velocity = new Vector2(moveSpeed, 0);
                        animator.SetBool("run", false);
                    }
                    else
                    {
                        animator.SetBool("run", true);
                        rigid.velocity = new Vector2(3 * moveSpeed, 0); //khi phát hiện thì tăng tốc
                    } 
                }
               else //đang bắn
                {
                    rigid.velocity = Vector2.zero;
                    animator.SetBool("run", false);
                    if (!isShooting)
                    {
                        timeShoot += Time.deltaTime;
                        if (timeShoot >0.667/5f)
                        {
                            isShooting = true;
                        }
                    }
                    else
                    {
                        CreateSnowBall();
                    }
                }
            }
            else
            {
                timeStun += Time.deltaTime;
                rigid.velocity = Vector2.zero;
                if (timeStun >= 0.2)
                {
                    timeStun = 0; isStun = false;
                }
            }
            bool playerHasHorizontalSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }
        healthBarMonster.UpdateBar(m_currentHealth, m_maxHealth);
        UIMonster.GetComponent<RectTransform>().transform.localScale = new Vector2(transform.localScale.x, 1);
       
    }
    void Shooting()
    {

    }
    void CreateSnowBall()
    {
        animator.SetTrigger("attack");
        snowBallInstance = Instantiate(snowBall, transform.position, Quaternion.identity);
        snowBallInstance.GetComponent<Rigidbody2D>().velocity= new Vector2(transform.localScale.x*shootSpeed, 0);
        snowBallInstance.GetComponent<SpriteRenderer>().color = new Color(1, Random.Range(0, 1f), Random.Range(0, 1f), 1);
        isShooting = false;timeShoot = 0;
        snowBallInstance.transform.parent = transform;
        snowBallInstance.mon = this;
        AudioManager.instance.PlayMonsterSound(AudioManager.instance.wizardAttack);
    }
}
