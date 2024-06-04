using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EyeView : MonoBehaviour
{
    BoxCollider2D eyeViewBox;
    Monster mon;
    private void Awake()
    {
        eyeViewBox=GetComponent<BoxCollider2D>();
       mon= gameObject.transform.parent.gameObject.GetComponent<Monster>();
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            Debug.Log("see");
            mon.isDetect = true;
            mon.animator.SetBool("run", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("no see");
            mon.isDetect = false;
            mon.animator.SetBool("run", false);
        }
    }
}
