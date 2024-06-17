using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Shopkeeper : MonoBehaviour
{
    public GameObject talkPanel, shopPanel;
    bool canTrade;
    public InventoryItemBase[] ShopItem;
    public InventoryItemBase itemToBuy;
    public int itemIndex, itemCost;
    public Image[] border;
    public TextMeshProUGUI playerCoinsText;
    Vector2 localScaleShop;
    private void Start()
    {
        localScaleShop=transform.localScale;
    }

    private void FixedUpdate()
    {
        playerCoinsText.text = PlayerController.instance.coins.ToString();
    }

    private void OnMouseDown()
    {
       if(canTrade &&!shopPanel.activeInHierarchy&&!talkPanel.activeInHierarchy)
        {
            talkPanel.SetActive(true);
            AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        }
    }
    public void YesButtonShop()
    {
        shopPanel.SetActive(true);
        talkPanel.SetActive(false);
        SetDefaultAllBorder();
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void NoButtonShop()
    {
        talkPanel.SetActive(false);
        shopPanel.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {if(collision.CompareTag("Player"))
        {
            canTrade = true;
            transform.localScale = 1.2f*localScaleShop;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            canTrade = false;
            talkPanel.SetActive(false);
            shopPanel.SetActive(false);
            transform.localScale =localScaleShop;
        }
    }
    public void BuySlot_0 ()
    {
        itemIndex = 0; itemCost = 200;
        SetDefaultAllBorder();
        border[0].color = new Color(0, 1, 0, 1);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void BuySlot_1()
    {
        itemIndex = 1; itemCost = 200;
        SetDefaultAllBorder();
        border[1].color = new Color(0, 1, 0, 1);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void BuySlot_2()
    {
        itemIndex = 2; itemCost = 1500;
        SetDefaultAllBorder();
        border[2].color = new Color(0, 1, 0, 1);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    void SetDefaultAllBorder()
    {
        for (int i = 0; i < border.Length; i++)
        {
            border[i].color = Color.white;
        }
    }
    public void BuyButton()
    {if (PlayerController.instance.coins>= itemCost&&Inventory.instance.mItems.Count<9)
        {
            itemToBuy = Instantiate(ShopItem[itemIndex]);
            Inventory.instance.BuyItemInShop(itemToBuy, itemCost);
            Destroy(itemToBuy.gameObject);
            
        }
    else
        {
            AudioManager.instance.PlaySound(AudioManager.instance.error);
        }
            
    }

}
