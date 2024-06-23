using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject[] coins;
    float value;
    public TextMeshProUGUI coinDropValue;
    bool isDropCoin;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        value= 50+20*PlayerController.instance.p_Level*UnityEngine.Random.Range(0.9f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropCoin)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            coinDropValue.GetComponent<RectTransform>().transform.position  //thay đổi vị trí tiền rơi
           = new Vector2(coinDropValue.GetComponent<RectTransform>().transform.position.x,
           coinDropValue.GetComponent<RectTransform>().transform.position.y + Time.deltaTime);
        }
    }
    void Setup()
    {
        Invoke("SetupCoins", 0.5f);
    }
    void SetupCoins()
    {
        coins[7].SetActive(true);
        for (int i = 0; i < 7; i++)
        {
            coins[i].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("FeetPlayer")&& coins[7].activeInHierarchy)
            {
                PlayerController.instance.coins +=(int) value;
                UIManager.instance.coinValues.text = PlayerController.instance.coins.ToString();
            coinDropValue.text="+" + (int)value;
            isDropCoin = true;
            coins[7].SetActive(false);
            Invoke("DestroyAllCoins", 1.2f);
            }
    }
    void DestroyAllCoins()
    {
        Destroy(gameObject);
    }

}
