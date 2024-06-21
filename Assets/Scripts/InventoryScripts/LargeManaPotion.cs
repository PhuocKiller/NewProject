using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeManaPotion : InventoryItemBase
{
    public override string Name
    {
        get { return "LargeManaPotion"; }
    }
    public override ItemTypes itemTypes
    {
        get { return ItemTypes.LargeManaPotion; }
    }
    public override void OnUse()
    {
        base.OnUse();
        PlayerController.instance.p_currentManaFade += PlayerController.instance.p_MaxMana;
        if (PlayerController.instance.p_currentManaFade > PlayerController.instance.p_MaxMana)
        {
            PlayerController.instance.p_currentManaFade = PlayerController.instance.p_MaxMana;
        }
        UIManager.instance.isRefillMana = true;
        ParticleManager.instance.SpawnMana(PlayerController.instance.transform.position);
    }
}
