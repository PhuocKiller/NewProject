using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    public Rigidbody2D rigidBall;
    CircleCollider2D circleBall;
    public Monster mon;
    private void Awake()
    {
        rigidBall=GetComponent<Rigidbody2D>();
        circleBall=GetComponent<CircleCollider2D>();
       // mon = transform.parent.gameObject.GetComponent<Wizard>();
    }
    private void Start()
    {
        
        Destroy(gameObject,2);
     }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mon.PlayerBeingAttacked(MechanicDamage.instance.GetDamageOfTwoObject(mon.m_attack, PlayerController.instance.p_Defend,
                0.5f, MechanicDamage.instance.DecreaseDamageMonster()),true);
        }
    }
}
