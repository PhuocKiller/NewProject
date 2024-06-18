using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Transform attackPoint;
    public Transform skill1Point;
    [SerializeField] private ParticleSystem chase, boom1;
    [SerializeField] private ParticleSystem skill1, skill;
    [SerializeField] private ParticleSystem die, attack, level, blood, tele;
    private ParticleSystem Skill1Instance, LevelInstance, AttackInstance, DieInstance, SkillInstance,BloodInstance,TeleInstance;
    private ParticleSystem ChaseInstance, Boom1Instance;

    //player
    
    public void SpawnBlood(Vector3 callerPosition)
    {
        BloodInstance = Instantiate(blood, callerPosition, Quaternion.identity);
    }
    public void SpawnDie(Vector3 callerPosition)
    {
        DieInstance = Instantiate(die, callerPosition, Quaternion.identity);
        Debug.Log("gg");
    }
    public void SpawnAttack()
    {
        AttackInstance = Instantiate(attack, attackPoint.position, Quaternion.identity);
    }

    public void SpawnLevel(Vector3 callerPosition)
    {
        LevelInstance = Instantiate(level, callerPosition, Quaternion.identity);
    }
    public void SpawnSkill1()
    {

        Skill1Instance = Instantiate(skill1, skill1Point.position, Quaternion.identity);
    }
    public void SpawnSkill()
    {
        SkillInstance = Instantiate(skill, skill1Point.position, Quaternion.identity);
    }


    //boss
    public void SpawnTele(Vector3 callerPosition)
    {
        TeleInstance = Instantiate(tele, callerPosition, Quaternion.identity);
    }
    public void SpawnBoom1(Vector3 callerPosition)
    {
        Boom1Instance = Instantiate(boom1, callerPosition, Quaternion.identity);
    }
    
    public void SpawnChase(Vector3 callerPosition)
    {
        ChaseInstance = Instantiate(chase, callerPosition, Quaternion.identity);
        Debug.Log("gg");
    }
}
