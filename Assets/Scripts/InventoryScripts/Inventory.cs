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
    public static Inventory instance;
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
            }
        }
    }
    public void RemoveItem(IInventoryItem item)
    {
        
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();
            Collider2D collider=(item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider!=null)
            {
                collider.enabled = true;
            }
            if (ItemRemoved != null)
            {
                ItemRemoved(this,new InventoryEventArgs(item));
            }
        }
    }

    internal void UseItem(IInventoryItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnUse();
            
           /* Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }*/
            if (ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }
}
