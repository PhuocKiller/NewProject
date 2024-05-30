using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlayerUI : MonoBehaviour
{
    public PlayerUI[] playerUI;
    public int numberIndexCharacter;
    public CharacterType characterType;
    public int level;
    public UITypes uiTypes;
    public static CreatePlayerUI instance;
    private void Awake()
    {
        instance = this;
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
        Debug.Log("len" + playerUI.Length);
        for (int i = 0; i < playerUI.Length; i++)
        {
            if (playerUI[i].characterType == characterType)
            {
                Instantiate(playerUI[i],transform.position,Quaternion.identity,transform.parent);
                
            }
        }
    }
}
