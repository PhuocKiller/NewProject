using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public GameObject[] coins;
    float value;
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        value= Random.Range(30, 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Setup()
    {
        Invoke("DestroyCoins", 0.5f);
    }
    void DestroyCoins()
    {
        for (int i = 0; i < 8; i++)
        {
            Destroy(coins[i].gameObject);
        }
        coins[8].SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("vo enter");
        if (collision.collider.CompareTag("Player"))
            {
            Debug.Log("vo trong");
                PlayerController.instance.myCoins += value;
                UIManager.instance.coinValues.text = PlayerController.instance.myCoins.ToString();
                Destroy(gameObject);
            }
        
    }

}
