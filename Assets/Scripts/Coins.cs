using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject[] coins;
    int value;
    public TextMeshProUGUI coinDropValue;
    bool isDropCoin;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        value= UnityEngine.Random.Range(30, 50);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDropCoin)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
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
        if (collision.collider.CompareTag("FeetPlayer"))
            {
                PlayerController.instance.coins += value;
                UIManager.instance.coinValues.text = PlayerController.instance.coins.ToString();
            coinDropValue.text="+" + value;
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
