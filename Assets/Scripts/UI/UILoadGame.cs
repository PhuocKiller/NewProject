using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoadGame : MonoBehaviour
{
    public Image Character1, Character2, Character3;
    public CreatePlayerUI[] createPlayerUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cha_0_Button()
    {
        Character1.color= new Color(Character1.color.r, Character1.color.g, 1);
        Character2.color = new Color(Character1.color.r, Character1.color.g, 0);
        Character3.color = new Color(Character1.color.r, Character1.color.g, 0);
        createPlayerUI[0].SetInfoByButton();
    }
    public void Cha_1_Button()
    {
        Character1.color = new Color(Character1.color.r, Character1.color.g, 0);
        Character2.color = new Color(Character1.color.r, Character1.color.g, 1);
        Character3.color = new Color(Character1.color.r, Character1.color.g, 0);
        createPlayerUI[1].SetInfoByButton();
    }
    public void Cha_2_Button()
    {
        Character1.color = new Color(Character1.color.r, Character1.color.g, 0);
        Character2.color = new Color(Character1.color.r, Character1.color.g, 0);
        Character3.color = new Color(Character1.color.r, Character1.color.g, 1);
        createPlayerUI[2].SetInfoByButton();
    }
    public void PlayButton()
    {
        SavingFile.instance.PlayLoadedGame();
    }
}
