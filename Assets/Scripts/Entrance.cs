using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
