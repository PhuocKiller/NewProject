using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterInfoPanel : MonoBehaviour
{
    public static MonsterInfoPanel Instance;

    public GameObject panel;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text attackText;
    public TMP_Text defText;
    public TMP_Text xpText;

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

    public void ShowInfo(Monster monster)
    {
        nameText.text = "Name: " + monster.monsType;
        healthText.text = "Health: " + monster.m_maxHealth;
        attackText.text = "Attack: " + monster.m_attack;
        defText.text = "Def: " + monster.m_defend;
        xpText.text = "XP: " + monster.m_XP;

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

