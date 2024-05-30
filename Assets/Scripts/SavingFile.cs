using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
           Save(numberIndexCharacter, characterType, UIManager.instance.uiTypes, level);
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Load(numberIndexCharacter);
        }
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
        foreach (var indexCharacter in gameProgress.listIndexCharacter )
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                PlayerController.instance.numberIndexCharacter = indexCharacter.numberIndexCharacter;
                PlayerController.instance.characterType = indexCharacter.characterType;
                PlayerController.instance.p_Level= indexCharacter.level;
                UIManager.instance.uiTypes = indexCharacter.uiTypes;

                return;
            }
        }
    }
    public void Get(int numberIndexCharacter)
    {
        foreach (var indexCharacter in gameProgress.listIndexCharacter)
        {
            if (indexCharacter.numberIndexCharacter == numberIndexCharacter)
            {
                Debug.Log(indexCharacter.characterType);
                Debug.Log("num index" + numberIndexCharacter);
                
                

                CreatePlayerUI.instance.characterType = indexCharacter.characterType;
                CreatePlayerUI.instance.CreateNewPlayerUI(indexCharacter.characterType);
                
                return;
            }
        }
    }

    public void LoadData()
    {
        string file = "Save.json";
        string filePath=Path.Combine(Application.persistentDataPath,file);
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "{\"listIndexCharacter\":[{\"numberIndexCharacter\":1}]}");
        }
        gameProgress = JsonUtility.FromJson<GameProgress>(File.ReadAllText(filePath));
    }
    public void SaveData()
    {
        string file = "Save.json";
        string filePath = Path.Combine(Application.persistentDataPath, file);
        string json=JsonUtility.ToJson(gameProgress);
        File.WriteAllText (filePath, json);
    }
   
}
