﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFlyDirection : MonoBehaviour
{
    BoxCollider2D box;
    Monster mon;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
    }
   
    private void OnTriggerExit2D(Collider2D collision) //xoay chiều di chuyển Monster
    {
        if (collision.gameObject.tag == "Ground" &&mon.isLive)
        {
            mon.moveSpeed = -mon.moveSpeed;
            if (mon.monsType==MonsterType.Boss)
            {
                mon.transform.localScale = new Vector2(Mathf.Sign(mon.rigid.velocity.x),1f);
            }
            else if (mon.monsType == MonsterType.Wizard)
            {
                mon.transform.localScale = new Vector2(-Mathf.Sign(mon.rigid.velocity.x), 1f);
            }
            else { mon.transform.localScale = new Vector2(0.1f * Mathf.Sign(mon.rigid.velocity.x), 0.1f); }
        }
    }
}
