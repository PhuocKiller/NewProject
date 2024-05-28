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

    public virtual void OnPickUp()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        gameObject.SetActive(true);
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameObject.transform.position = new Vector3(pos.x, pos.y, 0);


    }
    public virtual void OnUse()
    {

    }
}
