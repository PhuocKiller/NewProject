using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.instance.EndOldScene("Round1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.instance.EndOldScene("Round2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.instance.EndOldScene("Round3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UIManager.instance.EndOldScene("Boss");
        }
        if (Input.GetKeyDown(KeyCode.L) && PlayerController.instance!=null)
        {
            PlayerController.instance.p_CurrentXP = PlayerController.instance.p_MaxXP;
        }
        if (Input.GetKeyDown(KeyCode.G) && PlayerController.instance != null)
        {
            PlayerController.instance.coins += 5000;
        }
        if (Input.GetKeyDown(KeyCode.F) && PlayerController.instance != null)
        {
            PlayerController.instance.FullEngergy();
        }
        if (Input.GetKeyDown(KeyCode.P) && PlayerController.instance != null)
        {
            PlayerController.instance.p_Attack+=500;
        }
        if (Input.GetKeyDown(KeyCode.R) && PlayerController.instance != null)
        {
            PlayerController.instance.p_currentRageFloat = PlayerController.instance.p_MaxRage;
        }
    }
}
