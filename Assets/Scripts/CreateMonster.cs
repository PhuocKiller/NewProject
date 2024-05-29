using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    public Monster[] monsters;
    public Monster[] monstersInstance;
    Monster mon;
    // Start is called before the first frame update

   
    void Start()
    {
        if (mon == null)
        {
            CreateNewMonster();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mon == null)
        {
            CreateNewMonster();
            mon.gameObject.SetActive(false); //Monster ko xuât hiện liền mà sau 2s
            Invoke("ActiveMonsterAgain", 2f);
        }
    }
    void CreateNewMonster()
    {
        int monIndex= Random.Range(0, monsters.Length);
        mon=Instantiate(monsters[monIndex], transform.position, Quaternion.identity,transform.parent);
    }
    void ActiveMonsterAgain()
    {
        mon.gameObject.SetActive(true); 

    }
}
