using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject player;
    public GameObject UIPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(player).GetComponent<PlayerController>();
        }
        if (UIManager.instance == null)
        {
            UIManager.instance = Instantiate(UIPlayer).GetComponent<UIManager>();
        }
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
