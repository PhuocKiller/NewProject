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
    public int GetDamageOfTwoObject(int attack, int defend, float damageIncrease, float damageReduce)
    {
        return (int)(attack * UnityEngine.Random.Range(0.95f, 1.05f) *
            (1 - (PlayerController.instance.deltaDamage * defend / (1 + PlayerController.instance.deltaDamage * defend)))
           * damageIncrease * damageReduce);
    }
    public float IncreaseDamagePlayer(bool isCrit)
    {
        float damageFromSkin, damageFromSkill, damageFromRage;
        //Player's skin affect power
        if (PlayerAnimation.instance.skin == Skins.Lv1)
        {
            damageFromSkin = 1;
        }
        else if (PlayerAnimation.instance.skin == Skins.Lv5)
        {
            damageFromSkin = 2;
        }
        else { damageFromSkin = 5; }

        //Player's Skill affect power
        if (PlayerAnimation.instance.state == State.Attack)
        {
            damageFromSkill = 1;
        }
        else if (PlayerAnimation.instance.state == State.Skill1)
        {
            damageFromSkill = 3;
        }
        else if (PlayerAnimation.instance.state == State.MainSkill)
        {
            damageFromSkill = 0.5f;
        }
        else damageFromSkill = 1;
        if (isCrit)
        {
            if (PlayerController.instance.isRage)
            {
                damageFromRage = PlayerController.instance.p_RageCritDamage;
            }
            else
            {
                damageFromRage = PlayerController.instance.p_CritDamage;
            }
        }
        else { damageFromRage = 1f; }
        return (damageFromSkin * damageFromSkill* damageFromRage);
    }
    public float DecreaseDamageMonster()
    {
        float defendFromSkin;
        //Player's skin affect armor
        if (PlayerAnimation.instance.skin == Skins.Lv1)
        {
            defendFromSkin = 1;
        }
        else if (PlayerAnimation.instance.skin == Skins.Lv5)
        {
            defendFromSkin = 0.8f;
        }
        else { defendFromSkin = 0.3f; }
        return defendFromSkin;
    }
    public bool GetChance(float chance)
    {
        float r = Random.Range(1f, 100f);
        if (r / 100 < chance)
        {
            return true;
        }
        else return false;
    }
}
