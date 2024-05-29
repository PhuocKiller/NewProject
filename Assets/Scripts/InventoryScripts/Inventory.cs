using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 9;
    private List<IInventoryItem> mItems= new List<IInventoryItem>();
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemUsed;
    public event EventHandler<InventoryEventArgs> InventoryUpdate;
    public static Inventory instance;
    public InventoryItemBase[] inventoryItems;
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

        public void AddItem(IInventoryItem item)
    {
        if (mItems.Count<SLOTS)
        {
            Collider2D collider= (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickUp();
                if (ItemAdded != null)
                {
                    ItemAdded(this,new InventoryEventArgs(item));
                }
                if (InventoryUpdate != null)
                {
                    InventoryUpdate(this, new InventoryEventArgs(item));
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
}
