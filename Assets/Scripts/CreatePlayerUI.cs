using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreatePlayerUI : MonoBehaviour
{
    public PlayerUI[] playerUI;
    public int numberIndexCharacter;
    public CharacterType characterType;
    public int level;
    public UITypes uiTypes;
    public TextMeshProUGUI levelText;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        SavingFile.instance.Get(numberIndexCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void CreateNewPlayerUI (CharacterType characterType)
    {
        for (int i = 0; i < playerUI.Length; i++)
        {
            if (playerUI[i].characterType == characterType)
            {
                Instantiate(playerUI[i],transform.position,Quaternion.identity,transform.parent);
            }
        }
        levelText.text = "Level: " + level;
    }
    public void SetInfoByButton()
    {
        Debug.Log("vo set info");
        SavingFile.instance.numberIndexCharacter=numberIndexCharacter;
        SavingFile.instance.characterType = characterType;
        SavingFile.instance.level = level;
        SavingFile.instance.uiTypes = uiTypes;

    }
}
