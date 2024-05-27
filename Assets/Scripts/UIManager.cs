using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Bars healthBar, manaBar, XPBar;
    public TextMeshProUGUI levelPlayerTMP, damageDealByMonster;
    public bool isRefillMana;
    public bool isRefillHealth;
    float timeRefillMana, timeRefillHealth;
    public GameObject panelMonsterInfo, panelPlayerInfo;
    public TMP_Text nameMonsterTMP, healthMonsterTMP, attackMonsterTMP, defMonsterTMP, xpMonsterHaveTMP,
        healthPlayerTMP, manaPlayerTMP, xpPlayerTMP, attackPlayerTMP, defPlayerTMP, manaOfSkilPlayerTMP;
    public Inventory Inventory;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);  //xóa cái mới sinh ra
            }

        }
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.UpdateBar(PlayerController.instance.p_currentHealthFloat, PlayerController.instance.p_maxHealth);
        healthBar.UpdateFadeBar(PlayerController.instance.p_currentHealthFade, PlayerController.instance.p_maxHealth);
        if (!PlayerController.instance.isDie)
        {
            manaBar.UpdateBar(PlayerController.instance.p_currentManaFloat, PlayerController.instance.p_MaxMana);
            manaBar.UpdateFadeBar(PlayerController.instance.p_currentManaFade, PlayerController.instance.p_MaxMana);
            PlayerController.instance.GetLevel();
            XPBar.UpdateBar(PlayerController.instance.p_CurrentXP, PlayerController.instance.p_MaxXP);
        }
        if (isRefillMana)
        {
            
            if (PlayerController.instance.p_currentManaFloat>= PlayerController.instance.p_currentManaFade)
            {
                PlayerController.instance.p_currentManaFloat = PlayerController.instance.p_currentManaFade;
                isRefillMana = false;
            }
            PlayerController.instance.p_currentManaFloat += 0.5f;
        }
        if (isRefillHealth)
        {

            if (PlayerController.instance.p_currentHealthFloat >= PlayerController.instance.p_currentHealthFade)
            {
                PlayerController.instance.p_currentHealthFloat = PlayerController.instance.p_currentHealthFade;
                isRefillHealth = false;
            }
            PlayerController.instance.p_currentHealthFloat += 0.5f;
        }


    }
    public void ManaPotionClick()
    {

        PlayerController.instance.p_currentManaFade += 50;
        if (PlayerController.instance.p_currentManaFade > PlayerController.instance.p_MaxMana)
        {
            PlayerController.instance.p_currentManaFade = PlayerController.instance.p_MaxMana;
        }
        isRefillMana = true;
    }
    public void HealthPotionClick()
    {

        PlayerController.instance.p_currentHealthFade += 50;
        if (PlayerController.instance.p_currentHealthFade > PlayerController.instance.p_maxHealth)
        {
            PlayerController.instance.p_currentHealthFade = PlayerController.instance.p_maxHealth;
        }
        isRefillHealth = true;
    }
    public void ShowDamageDealByMonster(int damage)
    {
        damageDealByMonster.text = "-"+ damage.ToString();
        damageDealByMonster.GetComponent<RectTransform>().transform.position  //thay đổi vị trí bị trừ máu
          = PlayerController.instance.transform.position;
        Debug.Log("mat mau");Debug.Log(damage);
    }
    public void ShowInfoMonster(Monster monster)
    {
        nameMonsterTMP.text = "Name: " + monster.monsType;
        healthMonsterTMP.text = "Health: " + monster.m_maxHealth;
        attackMonsterTMP.text = "Attack: " + monster.m_attack;
        defMonsterTMP.text = "Def: " + monster.m_defend;
        xpMonsterHaveTMP.text = "XP: " + monster.m_XP;

        panelMonsterInfo.SetActive(true);
    }
    public void ClosePanelMonster()
    {
        panelMonsterInfo.SetActive(false);
    }
    public void ShowInfoPlayer()
    {
        healthPlayerTMP.text = "Halth: " + (int)PlayerController.instance.p_currentHealthFloat +"/"+PlayerController.instance.p_maxHealth;
        manaPlayerTMP.text = "Mana: " + (int)PlayerController.instance.p_currentManaFloat+ "/" +PlayerController.instance.p_MaxMana;
        attackPlayerTMP.text = "Attack: " + PlayerController.instance.p_Attack;
        defPlayerTMP.text = "Defend: " + PlayerController.instance.p_Defend;
        xpPlayerTMP.text = "XP: " +PlayerController.instance.p_CurrentXP +"/" + PlayerController.instance.p_MaxXP;
        manaOfSkilPlayerTMP.text ="Mana cost: " + PlayerController.instance.p_manaOfSkill;
        panelPlayerInfo.SetActive(true);
        Debug.Log("a");
    }
    public void ClosePanelPlayer()
    {
        panelPlayerInfo.SetActive(false);
    }
    public void AttackButton()
    {
        PlayerController.instance.PlayerAttack();
    }
    public void SkillButton()
    {
        PlayerController.instance.PlayerSkill();
    }
    void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {

            UnityEngine.UI.Image image =slot.GetChild(0).GetChild(0).GetComponent<Image>();
            
                if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                Debug.Log("b");
                break;
            }
            
            
            //we found the empty slot
           
        }
    }
}
