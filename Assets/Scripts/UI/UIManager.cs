using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;
//using UnityEngine.UIElements;
public enum UITypes
{
    Melee,Range
}
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Bars healthBar, manaBar, XPBar;
    public TextMeshProUGUI levelPlayerTMP, damageDealByMonster;
    public bool isRefillMana;
    public bool isRefillHealth;
    float timeRefillMana, timeRefillHealth;
    public GameObject panelMonsterInfo, panelPlayerInfo, panelInventory, panelSetting,unpauseButton;
    public TMP_Text nameMonsterTMP, healthMonsterTMP, attackMonsterTMP, defMonsterTMP, xpMonsterHaveTMP,
        healthPlayerTMP, manaPlayerTMP, xpPlayerTMP, attackPlayerTMP, defPlayerTMP, manaOfSkilPlayerTMP,
        numberHealPotionTMP,numberManaPotionTMP;
    int numberHealPotionInt,numberManaPotionInt;
    float displayTimePlayerBeAttacked;
    public Image imageHealPotion, imageManaPotion;
    public UITypes uiTypes;
 



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
        imageHealPotion=GameObject.Find("HealPotionButton").GetComponent<Image>();
        imageManaPotion = GameObject.Find("ManaPotionButton").GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Inventory.instance.ItemAdded += InventoryScript_ItemAdded;
        Inventory.instance.ItemRemoved += Inventory_ItemRemoved;
        Inventory.instance.InventoryUpdate += Inventory_Update;
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

            if (PlayerController.instance.p_currentManaFloat >= PlayerController.instance.p_currentManaFade)
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
        displayTimePlayerBeAttacked += Time.deltaTime;
        if (displayTimePlayerBeAttacked > 0.7) //0.7 là thời gian gài để tự biến mất
        {
            damageDealByMonster.text = null;
        }
        UpdateHealButton();
        UpdateManaButton();


    }
    
    public void ManaPotionClick()
    {
        if (numberManaPotionInt > 0)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.reFillPotion, 1);
            Transform inventoryPanel = transform.Find("InventoryPanel");
            foreach (Transform slot in inventoryPanel)
            {
                if (slot.childCount == 1)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    UnityEngine.UI.Image image = imageTransform.GetComponent<Image>();
                    ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                    if (itemDragHandler.Item != null&&itemDragHandler.Item.itemTypes==ItemTypes.ManaPotion)
                    {
                        Inventory.instance.UseItemClickInventory(itemDragHandler.Item);break;
                    }
                }
                
            }
        }
        else { AudioManager.instance.PlaySound(AudioManager.instance.error, 1); }
        
        
    }
    void UpdateManaButton()
    {
        if (numberManaPotionInt == 0)
        {
            imageManaPotion.color = new UnityEngine.Color(imageManaPotion.color.r, imageManaPotion.color.g, imageManaPotion.color.b, 0.2f);
        }
        else
        {
            imageManaPotion.color = new UnityEngine.Color(imageManaPotion.color.r, imageManaPotion.color.g, imageManaPotion.color.b, 1f);
        }
    }
    public void HealthPotionClick()
    {
        if (numberHealPotionInt>0)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.reFillPotion, 1);
            Transform inventoryPanel = transform.Find("InventoryPanel");
            foreach (Transform slot in inventoryPanel)
            {
                if (slot.childCount == 1)
                {
                    Transform imageTransform = slot.GetChild(0).GetChild(0);
                    UnityEngine.UI.Image image = imageTransform.GetComponent<Image>();
                    ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                    if (itemDragHandler.Item!=null&&itemDragHandler.Item.itemTypes == ItemTypes.HealPotion)
                    {
                        Inventory.instance.UseItemClickInventory(itemDragHandler.Item);break;
                    }
                }

            }
        }
        else { AudioManager.instance.PlaySound(AudioManager.instance.error, 1); }

    }
    void UpdateHealButton()
    {
        if (numberHealPotionInt == 0)
        {
            imageHealPotion.color = new UnityEngine.Color(imageHealPotion.color.r, imageHealPotion.color.g, imageHealPotion.color.b, 0.2f);
        }
        else
        {
            imageHealPotion.color = new UnityEngine.Color(imageHealPotion.color.r, imageHealPotion.color.g, imageHealPotion.color.b, 1f);
        }
    }
    public void ShowDamageDealByMonster(int damage)
    {
        displayTimePlayerBeAttacked = 0;
        damageDealByMonster.text = "-" + damage;
   
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
        healthPlayerTMP.text = "Halth: " + (int)PlayerController.instance.p_currentHealthFloat + "/" + PlayerController.instance.p_maxHealth;
        manaPlayerTMP.text = "Mana: " + (int)PlayerController.instance.p_currentManaFloat + "/" + PlayerController.instance.p_MaxMana;
        attackPlayerTMP.text = "Attack: " + PlayerController.instance.p_Attack;
        defPlayerTMP.text = "Defend: " + PlayerController.instance.p_Defend;
        xpPlayerTMP.text = "XP: " + PlayerController.instance.p_CurrentXP + "/" + PlayerController.instance.p_MaxXP;
        manaOfSkilPlayerTMP.text = "Mana cost: " + PlayerController.instance.p_manaOfSkill;
        panelPlayerInfo.SetActive(true);
      
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
    public void InventoryButton()
    {
        panelInventory.SetActive(true);
    }
   
    public void LoadButton()
    {
        SavingFile.instance.Load(PlayerController.instance.numberIndexCharacter);
    }
    public void ClosePanelInventory()
    {
        panelInventory.SetActive(false);
    }
    void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            UnityEngine.UI.Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();


            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                itemDragHandler.Item = e.Item;
                break;
            }
            //we found the empty slot
        }
    }
    void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            UnityEngine.UI.Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
           if (e.Item!=null && itemDragHandler.Item !=null)
            {
                if (itemDragHandler.Item.Equals(e.Item))
                {
                    image.enabled = false;
                    image.sprite = null;
                    itemDragHandler.Item = null;
                    break;
                }
            }
        }
    }
    public void Inventory_ItemSave()
    {
        SavingFile.instance.slot = new int[9];
        Transform inventoryPanel = transform.Find("InventoryPanel");
        for (int i = 0; i < 9; i++)
        {
            ItemDragHandler itemDragHandler= inventoryPanel.GetChild(i).GetChild(0).GetChild(0).GetComponent<ItemDragHandler>();
           
            if (itemDragHandler.Item!=null)
            {
                SavingFile.instance.slot[i] = (int)itemDragHandler.Item.itemTypes;
            }
            else { SavingFile.instance.slot[i] = -1; }
           
        }
    }
    
    void Inventory_Update(object sender, InventoryEventArgs e)
    {
      
        numberHealPotionInt = 0; numberManaPotionInt = 0;
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            if (slot.childCount==1)  //vì nút close panel ko có child
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                UnityEngine.UI.Image image = imageTransform.GetComponent<Image>();
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                if (itemDragHandler.Item != null)
                {
                    if (itemDragHandler.Item.itemTypes == ItemTypes.HealPotion)
                    {
                        numberHealPotionInt += 1;
                      
                    }
                    if (itemDragHandler.Item.itemTypes == ItemTypes.ManaPotion)
                    {
                        numberManaPotionInt += 1;
                        
                    }
                }
            }
            
        }
        numberHealPotionTMP.text = numberHealPotionInt.ToString();
        numberManaPotionTMP.text = numberManaPotionInt.ToString();
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
        unpauseButton.SetActive(true);
    }
    public void UnPauseButton()
    {
        Time.timeScale = 1f;
        unpauseButton.SetActive(false);
    }
    public void SettingButton()
    {
        panelSetting.SetActive(true);
       Time.timeScale = 0;
    }
    public void SaveButton()
    {
        Inventory_ItemSave();
        SavingFile.instance.Save(PlayerController.instance.numberIndexCharacter, PlayerController.instance.characterType,
            UIManager.instance.uiTypes, PlayerController.instance.p_Level);
        
    }
    public void  BackButtonInSetting()
    {
        panelSetting.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitButtonInSetting()
    {
        PlayerController.instance.transform.parent = GameObject.Find("Entrance").transform;
        UIManager.instance.transform.SetParent (GameObject.Find("Entrance").transform);
        Inventory.instance.transform.parent = GameObject.Find("Entrance").transform;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
