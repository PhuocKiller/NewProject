using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMonster : MonoBehaviour
{
    public TextMeshProUGUI damageOfPlayerTMP;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform=GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void ShowDamage( int damage)
    {
        damageOfPlayerTMP.text="-"+ damage.ToString();
        damageOfPlayerTMP.GetComponent<RectTransform>().transform.position  //thay đổi vị trí bị trừ máu
            = new Vector2(GetComponent<RectTransform>().transform.position.x + Random.Range(-0.5f,0.5f),
            GetComponent<RectTransform>().transform.position.y+ Random.Range(-0.5f, 0.5f));
    }
}
