using System.Collections;
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
    public int level, coins;

    public int[] slot;
    public void GetNotNUll()
    {
        slot=new int[9];
    }
}


public class SavingFile : MonoBehaviour
{
    public static SavingFile instance;
    public GameProgress gameProgress;
    public int numberIndexCharacter;
    public CharacterType characterType;
    public int level;
    public UITypes uiTypes;
    public int coins;
   // public CreatePlayerUI[] createPlayerUI;
    public PlayerController[] playerControllers;
    public UIManager[] uIManagers;
    public GameObject inventory;
    public int[] slot;

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
    public void Save(int numberIndexCharacter, CharacterType characterType, UITypes uiTypes, int level, int coins)
    {

        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {

            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                indexCharacter.characterType = characterType;
                indexCharacter.uiTypes = uiTypes;
                indexCharacter.level = level;
                indexCharacter.coins = coins;
                for (int i = 0; i < 9; i++)
                {
                    indexCharacter.slot[i] = slot[i];
                }
                SaveData();
                return;
            }
        }
        
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
    public void Get(CreatePlayerUI createPlayerUI, int numberIndexCharacter)
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                createPlayerUI.characterType = indexCharacter.characterType;
                createPlayerUI.level = indexCharacter.level;
                createPlayerUI.coins=indexCharacter.coins;
                createPlayerUI.uiTypes = indexCharacter.uiTypes;
                createPlayerUI.CreateNewPlayerUI(indexCharacter.characterType);
                
                
                for (int i = 0;i<9; i++)
                {
                    createPlayerUI.slot[i]= indexCharacter.slot[i];
                }

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
            File.WriteAllText(filePath, "{\"listIndexCharacter\":[{\"numberIndexCharacter\":2,\"characterType\":0,\"uiTypes\":0,\"level\":0,\"Coins\":0,\"slot\":[-1,-1,-1,-1,-1,-1,-1,-1,-1]},{\"numberIndexCharacter\":1,\"characterType\":0,\"uiTypes\":0,\"level\":0,\"Coins\":0,\"slot\":[-1,-1,-1,-1,-1,-1,-1,-1,-1]},{\"numberIndexCharacter\":0,\"characterType\":0,\"uiTypes\":0,\"level\":0,\"Coins\":0,\"slot\":[-1,-1,-1,-1,-1,-1,-1,-1,-1]}]}");
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
                        PlayerController.instance.coins = coins;
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
                        PlayerController.instance.coins = 0;
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
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter && level!=0)
            {
                Save(numberIndexCharacter, CharacterType.Melee, UITypes.Melee, 0,0);
                MainMenu.instance.loadGamePanel.SetActive(false);
                MainMenu.instance.loadGamePanel.SetActive(true);

            }
        }
        level = 0;
    }
}
