using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get { return "HealPotion"; }
    }

    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickUp()
    {
       gameObject.SetActive(false);
    }
}
