using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion :InventoryItemBase
{
    public override string Name
    {
        get { return "ManaPotion"; }
    }
    public override ItemTypes itemTypes
    {
        get { return ItemTypes.ManaPotion; }
    }
    public override void OnUse()
    {
        base.OnUse();
        PlayerController.instance.p_currentManaFade += 50;
        if (PlayerController.instance.p_currentManaFade > PlayerController.instance.p_MaxMana)
        {
            PlayerController.instance.p_currentManaFade = PlayerController.instance.p_MaxMana;
        }
        UIManager.instance.isRefillMana = true;
    }

}
