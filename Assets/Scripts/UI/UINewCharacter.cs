using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UINewCharacter : MonoBehaviour
{
    public Image meleeImage, rangeImage;
   

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Melee_Button()
    {
        meleeImage.color = new Color(meleeImage.color.r, 1, meleeImage.color.b);
        rangeImage.color = new Color(meleeImage.color.r, 0.5f, meleeImage.color.b);
        
    }
    public void Range_Button()
    {
        meleeImage.color = new Color(meleeImage.color.r, 0.5f, meleeImage.color.b);
        rangeImage.color = new Color(meleeImage.color.r, 1f, meleeImage.color.b);
    }
    public void PlayButtonNewGame()
    {
       
    }
    public void BackMenu()
    {
        
    }
}
