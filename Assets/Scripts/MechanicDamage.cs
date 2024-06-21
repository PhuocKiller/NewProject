using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicDamage : MonoBehaviour
{
    public static MechanicDamage instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public int GetDamageOfTwoObject(int a, int b, float damageIncrease, float damageReduce)
    {
        return (int)(a * UnityEngine.Random.Range(0.9f, 1.1f) *
            (1 - (PlayerController.instance.deltaDamage * b / (1 + PlayerController.instance.deltaDamage * b)))
           * damageIncrease * damageReduce);
    }
    public float IncreaseDamagePlayer()
    {
        float a; float b;
        //Player's skin affect power
        if (Animation.instance.skin == Skins.Lv1)
        {
            a = 1;
        }
        else if (Animation.instance.skin == Skins.Lv5)
        {
            a = 2;
        }
        else { a = 5; }

        //Player's Skill affect power
        if (Animation.instance.state == State.Attack)
        {
            b = 1;
        }
        else if (Animation.instance.state == State.Skill1)
        {
            b = 3;
        }
        else if (Animation.instance.state == State.MainSkill)
        {
            b = 0.5f;
        }
        else b = 1;
        return (a * b);
    }
    public float DecreaseDamageMonster()
    {
        float a;
        //Player's skin affect armor
        if (Animation.instance.skin == Skins.Lv1)
        {
            a = 1;
        }
        else if (Animation.instance.skin == Skins.Lv5)
        {
            a = 0.8f;
        }
        else { a = 0.3f; }
        return a;
    }
}
