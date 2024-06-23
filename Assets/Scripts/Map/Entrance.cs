using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public int posIndex;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance.posIndex== posIndex)
        {
            PlayerController.instance.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
