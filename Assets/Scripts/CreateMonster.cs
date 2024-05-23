using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    public Monster[] monsters;
    public Monster[] monstersInstance;
    // Start is called before the first frame update

   
    void Start()
    {
       
        CreateNewMonster();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CreateNewMonster()
    {
        int monIndex= Random.Range(0, monsters.Length);
        Instantiate(monsters[monIndex], transform.position, Quaternion.identity,transform.parent);
        
    }
}
