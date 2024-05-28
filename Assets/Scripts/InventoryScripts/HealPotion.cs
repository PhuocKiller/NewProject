using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion :InventoryItemBase
{
    public override string Name
    {
        get { return "HealPotion"; }
    }
    public override void OnUse()
    {
        base.OnUse();
        UIManager.instance.HealthPotionClick();
    }

}
