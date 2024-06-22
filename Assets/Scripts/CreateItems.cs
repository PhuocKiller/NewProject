using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItems : MonoBehaviour
{
    public InventoryItemBase[] items;
    public Coins coins;
    public float[] chance;
    public bool isPick;
   
    public void CreateItemsFromDeath()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (GetChance(chance[i]) )
            {
                Vector2 pos = new Vector2(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(0f, 4f));
                Instantiate(items[i], pos, Quaternion.identity);
            }
        }
        Instantiate(coins,transform.position, Quaternion.identity);
    }
    
    bool GetChance(float chance)
    {
        float r = Random.Range(1f, 100f);
        if (r / 100 < chance)
        {
            return true;
        }
        else return false;
    }

}
