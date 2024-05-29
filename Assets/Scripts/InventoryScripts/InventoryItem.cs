using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemTypes
{
    HealPotion,
    ManaPotion,
    Key
}
public class InventoryItem : MonoBehaviour
{

}
public interface IInventoryItem
{
    ItemTypes itemTypes { get; }
    string Name { get; }
    Sprite Image { get; }
    void OnPickUp();
    void OnDrop();
    void OnUse();
}
public class InventoryEventArgs : EventArgs

{
    public IInventoryItem Item;
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }
}
