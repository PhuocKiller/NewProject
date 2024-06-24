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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*displayTime += Time.deltaTime;
        if (damageOfPlayerTMP.text != null)
        {
            damageOfPlayerTMP.GetComponent<RectTransform>().transform.position  //thay đổi vị trí bị trừ máu
            = new Vector2(damageOfPlayerTMP.GetComponent<RectTransform>().transform.position.x ,
            damageOfPlayerTMP.GetComponent<RectTransform>().transform.position.y + 0.03f);
        }
        if (displayTime>0.7) //0.7 là thời gian gài để tự biến mất
        {
            damageOfPlayerTMP.text = null;
        }*/
    }
    public void ShowDamage( int damage)
    {
        /*  displayTime = 0;
          damageOfPlayerTMP.text="-"+ damage.ToString();
          damageOfPlayerTMP.GetComponent<RectTransform>().transform.position 
              = new Vector2(GetComponent<RectTransform>().transform.position.x + Random.Range(-0.5f,0.5f),
              GetComponent<RectTransform>().transform.position.y+Random.Range(-0.5f, 0.5f));*/
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
        damageOfPlayerTMPInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4);
        Destroy(damageOfPlayerTMPInstance.gameObject, 0.5f);
    }
    Vector2 UIMonsterPos()
    {
        return GetComponent<RectTransform>().transform.position;
    }
}
