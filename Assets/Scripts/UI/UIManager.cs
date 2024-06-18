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
using static UnityEditor.Progress;
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
    public bool isRefillMana, isRefillHealth;
    public bool isHaveKey; //có chìa khóa trong inventory hay ko
    float timeRefillMana, timeRefillHealth;
    public GameObject panelMonsterInfo, panelPlayerInfo, panelInventory, panelSetting,unpauseButton, 
        panelPlayAgain, panelHelp, panelLoadNewScene,panelPlayerBeAttacked;
    public TMP_Text nameMonsterTMP, healthMonsterTMP, attackMonsterTMP, defMonsterTMP, xpMonsterHaveTMP,
        healthPlayerTMP, manaPlayerTMP, xpPlayerTMP, attackPlayerTMP, defPlayerTMP,
        numberHealPotionTMP,numberManaPotionTMP,
        manaCostSkill_1TMP, manaCostMainSkillTMP;
    int numberHealPotionInt,numberManaPotionInt;
    float displayTimePlayerBeAttacked;
    Image imageHealPotion, imageManaPotion;
    public Image imageLoadNewScene;
    public UITypes uiTypes;
    public TMP_Text coinValues;
    public Slider musicSlider, soundSlider;
    public bool isClosingScene, isOpeningScene;




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
        coinValues.text= PlayerController.instance.coins.ToString();
        isOpeningScene = true;
    }



    // Update is called once per frame
    void FixedUpdate()
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
        if (isRefillMana) // hồi mana
        {

            if (PlayerController.instance.p_currentManaFloat >= PlayerController.instance.p_currentManaFade)
            {
                PlayerController.instance.p_currentManaFloat = PlayerController.instance.p_currentManaFade;
                isRefillMana = false;
            }
            PlayerController.instance.p_currentManaFloat += 0.5f;
        }
        if (isRefillHealth) //hồi máu
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
            panelPlayerBeAttacked.SetActive(false);
        }
        UpdateHealButton();
        UpdateManaButton();
        if (isClosingScene)
        {
            imageLoadNewScene.fillAmount+=0.5f*Time.deltaTime;
            if(imageLoadNewScene.fillAmount==1)
            {
                isClosingScene=false;
                isOpeningScene = true;
            }
        }
        if (isOpeningScene)
        {
            imageLoadNewScene.fillAmount-= Time.deltaTime;
            if (imageLoadNewScene.fillAmount == 0)
            {
                isOpeningScene = false;
                panelLoadNewScene.SetActive(false);
            }
        }
    }
    
    public void ManaPotionClick()
    {
        if (numberManaPotionInt > 0)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.reFillPotion);
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
        else { AudioManager.instance.PlaySound(AudioManager.instance.error); }
        
        
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
            AudioManager.instance.PlaySound(AudioManager.instance.reFillPotion);
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
        else { AudioManager.instance.PlaySound(AudioManager.instance.error); }

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
        panelPlayerBeAttacked.SetActive(true);
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
    
    public void ShowInfoPlayer()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        if (panelPlayerInfo.activeInHierarchy==false)
        {
            panelPlayerInfo.SetActive(true);
        }
    else { panelPlayerInfo.SetActive(false); }
        healthPlayerTMP.text = "Health: " + (int)PlayerController.instance.p_currentHealthFloat + "/" + PlayerController.instance.p_maxHealth;
        manaPlayerTMP.text = "Mana: " + (int)PlayerController.instance.p_currentManaFloat + "/" + PlayerController.instance.p_MaxMana;
        attackPlayerTMP.text = "Attack: " + PlayerController.instance.p_Attack;
        defPlayerTMP.text = "Defend: " + PlayerController.instance.p_Defend;
        xpPlayerTMP.text = "XP: " + PlayerController.instance.p_CurrentXP + "/" + PlayerController.instance.p_MaxXP;
     }
   
    public void AttackButton()
    {
        PlayerController.instance.PlayerAttack();
    }
    public void Skill_1_Button()
    {
        PlayerController.instance.PlayerSkill_1();
    }
    public void SkillButton()
    {
        PlayerController.instance.PlayerSkill();
    }
    public void InventoryButton()
    {
        if (panelInventory.activeInHierarchy==false)
        {
            panelInventory.SetActive(true);
        }
        else { panelInventory.SetActive(false); }
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
   
    public void LoadButton()
    {
        SavingFile.instance.Load(PlayerController.instance.numberIndexCharacter);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
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
        isHaveKey = false;
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
                    if (itemDragHandler.Item.itemTypes ==ItemTypes.Key)
                    {
                        isHaveKey = true;
                    }
                }
            }
            
        }
        numberHealPotionTMP.text = numberHealPotionInt.ToString();
        numberManaPotionTMP.text = numberManaPotionInt.ToString();
    }
    public void CheckKeyInventory(Guest guest)
    {
        isHaveKey = false;
        Transform inventoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in inventoryPanel)
        {
            if (slot.childCount == 1)  //vì nút close panel ko có child
            {
                Transform imageTransform = slot.GetChild(0).GetChild(0);
                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();
                if (itemDragHandler.Item != null)
                {
                    if (itemDragHandler.Item.itemTypes == ItemTypes.Key)
                    {
                        isHaveKey = true;
                        Inventory.instance.UseKeyToFightBoss(itemDragHandler.Item);
                    }
                }
            }
        }
        if (isHaveKey)
        {
            
            AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
            EndOldScene("Boss");
        } else 
        {
            guest.ActiveNoKeyPanel();
        }
       
    }
   
    public void PauseButton()
    {
        Time.timeScale = 0;
        unpauseButton.SetActive(true);
        AudioManager.instance.PlaySound(AudioManager.instance.pauseGame);
    }
    public void UnPauseButton()
    {
        Time.timeScale = 1f;
        unpauseButton.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.unpauseGame);
    }
    public void HelpButton()
    {
        panelHelp.SetActive(true);
        manaCostSkill_1TMP.text = PlayerController.instance.p_manaCostSkill_1.ToString();
        manaCostMainSkillTMP.text= PlayerController.instance.p_manaCostMainSkill.ToString();
    }
    public void CloseButton()
    {
        if (panelHelp.activeInHierarchy) { panelHelp.SetActive(false); }
    }
    public void SettingButton()
    {
        panelSetting.SetActive(true);
       Time.timeScale = 0;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void SaveButton()
    {
        Inventory_ItemSave();
        SavingFile.instance.Save(PlayerController.instance.numberIndexCharacter, PlayerController.instance.characterType,
         UIManager.instance.uiTypes, PlayerController.instance.p_Level, PlayerController.instance.coins,
         musicSlider.value, soundSlider.value);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);

    }
    public void  BackButtonInSetting()
    {
        panelSetting.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void QuitButtonInSetting()
    {
        PlayerController.instance.transform.parent = GameObject.Find("Entrances").transform;
        UIManager.instance.transform.SetParent (GameObject.Find("Entrances").transform);
        Inventory.instance.transform.parent = GameObject.Find("Entrances").transform;
        Time.timeScale = 1f;
        panelPlayAgain.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayAgainButton()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
        PlayerController.instance.FullEngergy();
        PlayerController.instance.isDie = false;
        PlayerController.instance.beImmortal=false;
        PlayerController.instance.posIndex = 0;
        Animation.instance.state = State.Idle;
        panelPlayAgain.SetActive(false);
        SceneManager.LoadScene("Round1");
    }
    public void EndOldScene(string sceneName =null)
    {
        panelLoadNewScene.SetActive(true);
        isClosingScene = true;
        if (sceneName != null)
        {
            StartCoroutine(LoadSceneDelay(sceneName));
        }
    }
    IEnumerator LoadSceneDelay(string sceneName =null)
    {
        yield return new WaitForSeconds(2);
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

}
