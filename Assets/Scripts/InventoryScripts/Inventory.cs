using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 9;
    public List<IInventoryItem> mItems= new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> InventoryUpdate;
    public static Inventory instance;
    public InventoryItemBase[] inventoryItems, inventory_9_Items;
    InventoryItemBase item;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);  //xóa cái mới sinh ra
            }

        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadItemsFromSave();
    }

    public void AddItem(IInventoryItem item)
    {
        if (mItems.Count<SLOTS)
        {
            Collider2D collider= (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                if (item.itemTypes != ItemTypes.Key)
                {
                    collider.enabled = false;
                    mItems.Add(item);
                    item.OnPickUp();
                    if (ItemAdded != null)
                    {
                        ItemAdded(this, new InventoryEventArgs(item));
                    }
                    if (InventoryUpdate != null)
                    {
                        InventoryUpdate(this, new InventoryEventArgs(item));
                    }
                }
                else
                {
                    if (InventoryUpdate != null)
                    {
                        InventoryUpdate(this, new InventoryEventArgs(item));
                    }
                    if (!UIManager.instance.isHaveKey)
                    {
                        collider.enabled = false;
                        mItems.Add(item);
                        item.OnPickUp();
                        if (ItemAdded != null)
                        {
                            ItemAdded(this, new InventoryEventArgs(item));
                        }
                        if (InventoryUpdate != null)
                        {
                            InventoryUpdate(this, new InventoryEventArgs(item));
                        }
                    }
                    else
                    {

                    }
                }
                   
            }
        }
    }
    public void RemoveItem(IInventoryItem item) //Quăng item ra đất
    {
        
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();
         
            if (ItemRemoved != null)
            {
                ItemRemoved(this,new InventoryEventArgs(item));
            }
            if (InventoryUpdate != null)
            {
                InventoryUpdate(this, new InventoryEventArgs(item));
            }
        }
    }

    internal void UseItemClickInventory(IInventoryItem item) //Use item khi click trực tiếp trong inventory
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnUse();
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
            if (InventoryUpdate != null)
            {
                InventoryUpdate(this, new InventoryEventArgs(item));
            }
        }
    }
    public void UseKeyToFightBoss(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnUse();
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
    internal void UseItemClickButton(IInventoryItem item) //Use item khi click button
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnUse();
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
            if (InventoryUpdate != null)
            {
                InventoryUpdate(this, new InventoryEventArgs(item));
            }
        }
    }
    public void BuyItemInShop (IInventoryItem item, int itemCost)
    {
        if (mItems.Count < SLOTS)
        {
            if (item.itemTypes!=ItemTypes.Key)
            {
                mItems.Add(item);
                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                    BuyItemSuccess(itemCost);
                }
                if (InventoryUpdate != null)
                {
                    InventoryUpdate(this, new InventoryEventArgs(item));
                }
            }
            else
            {
                if (InventoryUpdate != null)
                {
                    InventoryUpdate(this, new InventoryEventArgs(item));
                }
                if (!UIManager.instance.isHaveKey)
                {
                    mItems.Add(item);
                    if (ItemAdded != null)
                    {
                        ItemAdded(this, new InventoryEventArgs(item));
                        BuyItemSuccess(itemCost);
                    }
                }
                else { AudioManager.instance.PlaySound(AudioManager.instance.error); }
            }
        }
    }
    void BuyItemSuccess(int itemCost)
    {
        PlayerController.instance.coins -= itemCost;
        UIManager.instance.coinValues.text = PlayerController.instance.coins.ToString();
        AudioManager.instance.PlaySound(AudioManager.instance.buyItem);
    }
    public void CreateNewItem(Vector3 pos, ItemTypes itemTypes) //tạo ra item khi quăng ra đất
    {
        int i;
        for (i = 0; i <= inventoryItems.Length;i++ )
        {
            if (inventoryItems[i].itemTypes == itemTypes)
            {
                break;
            }
        }
        Instantiate(inventoryItems[i],pos, Quaternion.identity);
    }
    public void LoadItemsFromSave()
    {
        for (int i = 0;i<9;i++)
        {
            for (int j = 0;j < inventoryItems.Length;j++)
            {
                if ((int)inventoryItems[j].itemTypes == SavingFile.instance.slot[i]) //dò items từ trong file saving
                {
                    if (mItems.Count < SLOTS)
                    {
                        inventory_9_Items[i] = inventoryItems[j];
                            mItems.Add(inventory_9_Items[i]);
                            if (ItemAdded != null)
                            {
                                ItemAdded(this, new InventoryEventArgs(inventory_9_Items[i]));
                            }
                            if (InventoryUpdate != null)
                            {
                                InventoryUpdate(this, new InventoryEventArgs(inventory_9_Items[i]));
                            }
                        
                    }
                }
            }
        }
    }
}
