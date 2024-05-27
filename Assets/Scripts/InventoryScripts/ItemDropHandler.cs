using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropHandler : MonoBehaviour, IDropHandler
{
    
    Inventory inventory;
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {
        RectTransform invPanel= transform as RectTransform;
        if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
        {
            IInventoryItem item = eventData.pointerDrag.gameObject.GetComponent<ItemDragHandler>().Item;
            if (item != null)
            {
                inventory.RemoveItem(item);
            }
        }
    }
}
