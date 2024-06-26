using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMonster : MonoBehaviour
{
    public TextMeshProUGUI damageOfPlayerTMP;
    TextMeshProUGUI damageOfPlayerTMPInstance;
    float displayTime;
    RectTransform rectTransform;
    Monster mon;
    
    private void Awake()
    {
        rectTransform=GetComponent<RectTransform>();
        mon = gameObject.transform.parent.gameObject.GetComponent<Monster>();
        damageOfPlayerTMP.text = null;
        
    }
   
    public void ShowDamage( int damage,bool isCrit)
    {
        
        damageOfPlayerTMPInstance = Instantiate(damageOfPlayerTMP, transform.position, Quaternion.identity);

        damageOfPlayerTMPInstance.GetComponent<RectTransform>().transform.SetParent(transform);
        damageOfPlayerTMPInstance.transform.localScale = new Vector3(0.13f, 0.13f, 0);
        if (mon.monsType==MonsterType.Boss)
        {
            damageOfPlayerTMPInstance.GetComponent<RectTransform>().transform.position =
                                  new Vector2(UIMonsterPos().x + Random.Range(-1.5f, 1.5f), UIMonsterPos().y + Random.Range(1f, 3f));
        }
        else if (mon.monsType == MonsterType.Wizard)
        {
            damageOfPlayerTMPInstance.GetComponent<RectTransform>().transform.position =
                                  new Vector2(UIMonsterPos().x + Random.Range(-1.2f, 1.2f), UIMonsterPos().y + Random.Range(-1.2f, 1.2f));
        }
        else
        {
            damageOfPlayerTMPInstance.GetComponent<RectTransform>().transform.position =
                      new Vector2(UIMonsterPos().x + Random.Range(-0.25f, 0.25f), UIMonsterPos().y + Random.Range(-0.25f, 0.25f));
           
        }
        damageOfPlayerTMPInstance.text = "-" + damage;
        damageOfPlayerTMPInstance.fontSize= ChangeFontSizeCritDamage(mon.monsType, isCrit);
        damageOfPlayerTMPInstance.color = ChangeColorCritDamage(mon.monsType, isCrit, damageOfPlayerTMPInstance.color);
        damageOfPlayerTMPInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
        Destroy(damageOfPlayerTMPInstance.gameObject, 0.5f);
    }
    Vector2 UIMonsterPos()
    {
        return GetComponent<RectTransform>().transform.position;
    }
    float ChangeFontSizeCritDamage(MonsterType monsType, bool isCrit)
    {
        if (monsType == MonsterType.Boss)
        {
            if (isCrit)
            {
                return 14;
            }
            else
            {
                return 6;
            }
        }
        else if (monsType == MonsterType.Wizard)
        {
            if (isCrit)
            {
                return 14;
            }
            else
            {
                return 6;
            }
        }
        else
        {
            if (isCrit)
            {
                return 75;
            }
            else
            {
                return 50;
            }
        }

    }
    Color ChangeColorCritDamage(MonsterType monsType, bool isCrit, Color color)
    {
        if (monsType == MonsterType.Boss)
        {
            if (isCrit)
            {
                return color;
            }
            else
            {
                return new Color(color.r*0.9f, color.g, color.b, color.a*0.9f);
            }
        }
        else if (monsType == MonsterType.Wizard)
        {
            if (isCrit)
            {
                return color;
            }
            else
            {
                return new Color(color.r * 0.9f, color.g, color.b, color.a * 0.9f);
            }
        }
        else
        {
            if (isCrit)
            {
                return color;
            }
            else
            {
                return new Color(color.r * 0.9f, color.g, color.b, color.a * 0.9f);
            }
        }

    }
}
