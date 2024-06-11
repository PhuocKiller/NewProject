using Spine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatePlayerUI : MonoBehaviour
{
    public PlayerUI[] playerUI;
    PlayerUI playerUiInstance;
    public int numberIndexCharacter;
    public CharacterType characterType;
    public int level, coins;
    public UITypes uiTypes;
    public TextMeshProUGUI levelText;
    public int[] slot;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        slot = new int[9];
        SavingFile.instance.Get(this, numberIndexCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        if( playerUiInstance!=null)
        {
            Destroy(playerUiInstance.gameObject);

        }
        
    }
    public void CreateNewPlayerUI (CharacterType characterType)
    {
        if (level != 0)
        {
            for (int i = 0; i < playerUI.Length; i++)
            {
                if (playerUI[i].characterType == characterType)
                {
                    playerUiInstance = Instantiate(playerUI[i], transform.position, Quaternion.identity, transform.parent);
                }
            }
            levelText.text = "Level: " + level;
        }
        else levelText.text = "";


    }
    public void SetInfoByButton()
    {
        SavingFile.instance.numberIndexCharacter=numberIndexCharacter;
        SavingFile.instance.characterType = characterType;
        SavingFile.instance.level = level;
        SavingFile.instance.uiTypes = uiTypes;
        SavingFile.instance.coins = coins;
        for (int i = 0;i<9;i++)
        {
            SavingFile.instance.slot[i] = slot[i];
        }
    }
}
