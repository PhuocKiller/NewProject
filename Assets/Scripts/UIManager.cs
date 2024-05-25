using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Bars healthBar, manaBar, XPBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateBar(PlayerController.instance.p_currentHealth, PlayerController.instance.p_maxHealth);
        if (!PlayerController.instance.isDie)
        {
            manaBar.UpdateBar(PlayerController.instance.p_currentManaFloat, PlayerController.instance.p_MaxMana);
            PlayerController.instance.GetLevel();
            XPBar.UpdateBar(PlayerController.instance.p_CurrentXP, PlayerController.instance.p_MaxXP);
        }
    }
}
