using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfoPanel : MonoBehaviour
{
    public static PlayerInfoPanel Instance;

    public GameObject panel;
    public TMP_Text health;
    public TMP_Text mana;
    public TMP_Text attack;
    public TMP_Text xp;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInfo(PlayerController player)
    {
        health.text = "Halth: " + player.p_maxHealth;
        mana.text = "Mana: " + player.p_MaxMana;
        attack.text = "Attack: " + player.p_Attack;
        xp.text = "XP: " + player.p_MaxXP;
     

        panel.SetActive(true);
    }

    public void HideInfo()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }
}
