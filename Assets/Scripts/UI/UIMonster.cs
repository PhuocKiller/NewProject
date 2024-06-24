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
        displayTime += Time.deltaTime;
        if (damageOfPlayerTMP.text != null)
        {
            damageOfPlayerTMP.GetComponent<RectTransform>().transform.position  //thay đổi vị trí bị trừ máu
            = new Vector2(damageOfPlayerTMP.GetComponent<RectTransform>().transform.position.x ,
            damageOfPlayerTMP.GetComponent<RectTransform>().transform.position.y + 0.03f);
        }
        if (displayTime>0.7) //0.7 là thời gian gài để tự biến mất
        {
            damageOfPlayerTMP.text = null;
        }
    }
    public void ShowDamage( int damage)
    {
        displayTime = 0;
        damageOfPlayerTMP.text="-"+ damage.ToString();
        damageOfPlayerTMP.GetComponent<RectTransform>().transform.position 
            = new Vector2(GetComponent<RectTransform>().transform.position.x + Random.Range(-0.5f,0.5f),
            GetComponent<RectTransform>().transform.position.y+Random.Range(-0.5f, 0.5f));

    }
}
