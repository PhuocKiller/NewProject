using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get;
    }
    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual ItemTypes itemTypes { get; set; }

    public virtual void OnPickUp()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public virtual void OnDrop()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos = new Vector3(pos.x, pos.y, 0);
        Inventory.instance.CreateNewItem(pos, GetItemTypes());

    }
    public virtual ItemTypes GetItemTypes()
    {
        return itemTypes;
    }
    public virtual void OnUse()
    {

    }
}
