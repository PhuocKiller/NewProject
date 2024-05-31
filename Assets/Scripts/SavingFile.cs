﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;


[System.Serializable]
public class GameProgress
{
    public List<GameIndexCharacter> listIndexCharacter;
}

[System.Serializable]
public class GameIndexCharacter
{
    public int numberIndexCharacter;
    public CharacterType characterType;
    public UITypes uiTypes;
    public int level;
}


public class SavingFile : MonoBehaviour
{
    public static SavingFile instance;
    public GameProgress gameProgress;
    public int numberIndexCharacter;
    public CharacterType characterType;
    public int level;
    public UITypes uiTypes;
    public CreatePlayerUI[] createPlayerUI;
    public PlayerController[] playerControllers;
    public UIManager[] uIManagers;
    public GameObject inventory;

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
    private void Start()
    {
        LoadData();


    }
    private void Update()
    {
    }
    public void Save(int numberIndexCharacter, CharacterType characterType, UITypes uiTypes, int level)
    {

        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                indexCharacter.characterType = characterType;
                indexCharacter.uiTypes = uiTypes;
                indexCharacter.level = level;
                SaveData();
                return;
            }
        }
        GameIndexCharacter gameIndexCharacter = new GameIndexCharacter();
        gameIndexCharacter.numberIndexCharacter = numberIndexCharacter;
        gameIndexCharacter.characterType = characterType;
        gameIndexCharacter.uiTypes = uiTypes;
        gameIndexCharacter.level = level;
        gameProgress.listIndexCharacter.Add(gameIndexCharacter);
        SaveData();
    }
    public void Load(int numberIndexCharacter)
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                PlayerController.instance.numberIndexCharacter = indexCharacter.numberIndexCharacter;
                PlayerController.instance.characterType = indexCharacter.characterType;
                PlayerController.instance.p_Level = indexCharacter.level;
                UIManager.instance.uiTypes = indexCharacter.uiTypes;

                return;
            }
        }
    }
    public void Get(int numberIndexCharacter)
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter&& indexCharacter.level!=0)
            {
                createPlayerUI[numberIndexCharacter ].characterType = indexCharacter.characterType;
                createPlayerUI[numberIndexCharacter ].level = indexCharacter.level;
                createPlayerUI[numberIndexCharacter ].uiTypes = indexCharacter.uiTypes;
                createPlayerUI[numberIndexCharacter ].CreateNewPlayerUI(indexCharacter.characterType);

                return;
            }
        }
    }

    public void LoadData()
    {
        string file = "Save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "{\"listIndexCharacter\":[{\"numberIndexCharacter\":2,\"characterType\":0,\"uiTypes\":0,\"level\":0},{\"numberIndexCharacter\":1,\"characterType\":0,\"uiTypes\":0,\"level\":0},{\"numberIndexCharacter\":0,\"characterType\":0,\"uiTypes\":0,\"level\":0}]}");
        }
        gameProgress = JsonUtility.FromJson<GameProgress>(File.ReadAllText(filePath));
    }
    public void SaveData()
    {
        string file = "Save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        string json = JsonUtility.ToJson(gameProgress);
        File.WriteAllText(filePath, json);
    }
    public void PlayLoadedGame()
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter &&level!=0)
            {
                for (int i = 0; i < playerControllers.Length; i++)
                {
                    if (playerControllers[i].characterType == characterType)
                    {
                        if (PlayerController.instance == null)
                        {
                            PlayerController.instance = Instantiate(playerControllers[i]).GetComponent<PlayerController>();
                        }
                        if (UIManager.instance == null)
                        {
                            UIManager.instance = Instantiate(uIManagers[i]).GetComponent<UIManager>();
                        }
                        if (Inventory.instance == null)
                        {
                            Inventory.instance = Instantiate(inventory).GetComponent<Inventory>();
                        }
                        PlayerController.instance.numberIndexCharacter = numberIndexCharacter;
                        PlayerController.instance.characterType= characterType;
                        if(level==0) { level=1; }
                        PlayerController.instance.p_Level = level;
                        UIManager.instance.uiTypes = uiTypes;
                        SceneManager.LoadScene("Round1");
                    }

                }
            }
        }
    }
    public void PlayNewGame()
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.level== 0)
            {
                for (int i = 0; i < playerControllers.Length; i++)
                {
                    Debug.Log(playerControllers[i].characterType);
                    Debug.Log(characterType);
                    Debug.Log(indexCharacter.numberIndexCharacter);
                    if (playerControllers[i].characterType == characterType)
                    {
                        if (PlayerController.instance == null)
                        {
                            PlayerController.instance = Instantiate(playerControllers[i]).GetComponent<PlayerController>();
                        }
                        if (UIManager.instance == null)
                        {
                            UIManager.instance = Instantiate(uIManagers[i]).GetComponent<UIManager>();
                        }
                        if (Inventory.instance == null)
                        {
                            Inventory.instance = Instantiate(inventory).GetComponent<Inventory>();
                        }
                        PlayerController.instance.numberIndexCharacter = indexCharacter.numberIndexCharacter;
                        PlayerController.instance.characterType = characterType;
                        PlayerController.instance.p_Level = 1;
                        UIManager.instance.uiTypes = uiTypes;
                        SceneManager.LoadScene("Round1");
                    }
                }
            }
        }
    }
    public void DeleteGame()
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                Save(numberIndexCharacter, CharacterType.Melee, UITypes.Melee, 0);
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}