using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           PlayerController.instance.PlayerBeingAttacked(0.2f * PlayerController.instance.p_maxHealth + Random.Range(20, 50));
        }
    }
}
