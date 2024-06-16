using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    public Image Character0, Character1, Character2, meleeImage, rangeImage;
    public CreatePlayerUI[] createPlayerUI;
    public GameObject newGamePanel, loadGamePanel,loginPanel, settingPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }
    public void NewGame()
    {
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        loginPanel.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void LoadGame()
    {
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(true);
        loginPanel.SetActive(false);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void Setting()
    { 
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        settingPanel.SetActive(true);
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void EnterLogin()
    {
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        loginPanel.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Cha_0_Button()
    {
        Character0.color = new Color(Character0.color.r, Character0.color.g, 1);
        Character1.color = new Color(Character0.color.r, Character0.color.g, 0);
        Character2.color = new Color(Character0.color.r, Character0.color.g, 0);
        createPlayerUI[0].SetInfoByButton();
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void Cha_1_Button()
    {
        Character0.color = new Color(Character0.color.r, Character0.color.g, 0);
        Character1.color = new Color(Character0.color.r, Character0.color.g, 1);
        Character2.color = new Color(Character0.color.r, Character0.color.g, 0);
        createPlayerUI[1].SetInfoByButton();
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void Cha_2_Button()
    {
        Character0.color = new Color(Character0.color.r, Character0.color.g, 0);
        Character1.color = new Color(Character0.color.r, Character0.color.g, 0);
        Character2.color = new Color(Character0.color.r, Character0.color.g, 1);
        createPlayerUI[2].SetInfoByButton();
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
    public void PlayLoadedGameButton()
    {
        SavingFile.instance.PlayLoadedGame();
        
    }
    public void DeleteGameButton()
    {
        SavingFile.instance.DeleteGame();
    }
    public void PlayNewGameButton()
    {
        SavingFile.instance.PlayNewGame();
    }
    public void Melee_Button()
    {
        meleeImage.color = new Color(meleeImage.color.r, 1, meleeImage.color.b);
        rangeImage.color = new Color(meleeImage.color.r, 0.5f, meleeImage.color.b);
        SavingFile.instance.characterType=CharacterType.Melee;
        SavingFile.instance.uiTypes=UITypes.Melee;
        SavingFile.instance.level = 1;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);

    }
    public void Range_Button()
    {
        meleeImage.color = new Color(meleeImage.color.r, 0.5f, meleeImage.color.b);
        rangeImage.color = new Color(meleeImage.color.r, 1f, meleeImage.color.b);
        SavingFile.instance.characterType = CharacterType.Range;
        SavingFile.instance.uiTypes = UITypes.Range;
        SavingFile.instance.level = 1;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
   
    public void BackMenu()
    {
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        settingPanel.SetActive(false);
        SavingFile.instance.level = 0;
        AudioManager.instance.PlaySound(AudioManager.instance.clickButton);
    }
}
