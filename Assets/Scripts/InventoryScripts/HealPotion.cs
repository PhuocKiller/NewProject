using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion :InventoryItemBase
{
    public override string Name
    {
        get { return "HealPotion"; }
    }
    public override ItemTypes itemTypes
    {
        get { return ItemTypes.HealPotion; }
    }
    public override void OnUse()
    {
        base.OnUse();
        PlayerController.instance.p_currentHealthFade += 0.5f* PlayerController.instance.p_maxHealth;
        if (PlayerController.instance.p_currentHealthFade > PlayerController.instance.p_maxHealth)
        {
            PlayerController.instance.p_currentHealthFade = PlayerController.instance.p_maxHealth;
        }
        UIManager.instance.isRefillHealth = true;
        ParticleManager.instance.SpawnHeal(new Vector2(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y+3));
    }

}
