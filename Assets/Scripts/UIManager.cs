using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Bars healthBar, manaBar, XPBar;
    public TextMeshProUGUI levelPlayerTMP, damageDealByMonster;
    public bool isRefillMana;
    public bool isRefillHealth;
    float timeRefillMana, timeRefillHealth;
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

}
