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
    public Transform attackPoint, skill1Point,mainSkillPoint;
    [SerializeField] private ParticleSystem chase, boom1;
    [SerializeField] private ParticleSystem skill_1, skill;
    [SerializeField] private ParticleSystem die, attack, level, blood, tele;
    private ParticleSystem Skill_1Instance, LevelInstance, AttackInstance, DieInstance, SkillInstance,BloodInstance,TeleInstance;
    private ParticleSystem ChaseInstance, Boom1Instance;

    //player
    
    public void SpawnBlood(Vector3 callerPosition)
    {
        BloodInstance = Instantiate(blood, callerPosition, Quaternion.identity);
        Destroy(BloodInstance.gameObject, BloodInstance.main.duration);
    }
    public void SpawnDie(Vector3 callerPosition)
    {
        DieInstance = Instantiate(die, callerPosition, Quaternion.identity);
        Destroy(die.gameObject, die.main.duration);
    }
    public void SpawnAttack()
    {
        AttackInstance = Instantiate(attack, attackPoint.position, Quaternion.identity);
        Destroy(AttackInstance.gameObject, AttackInstance.main.duration);

    }

    public void SpawnLevel(Vector3 callerPosition)
    {
        LevelInstance = Instantiate(level, callerPosition, Quaternion.identity);
        Destroy(LevelInstance.gameObject, LevelInstance.main.duration);
    }
    public void SpawnSkill_1()
    {

        Skill_1Instance = Instantiate(skill_1, skill1Point.position, Quaternion.identity);
        Destroy(Skill_1Instance.gameObject, Skill_1Instance.main.duration);
    }
    public void SpawnSkill()
    {
        SkillInstance = Instantiate(skill, mainSkillPoint.position, Quaternion.identity);
        Destroy(SkillInstance.gameObject, SkillInstance.main.duration);
    }
    public void SpwanSkillRepeat()
    {
        InvokeRepeating("SpawnSkill",0, 0.3f);
    }


    //boss
    public void SpawnTele(Vector3 callerPosition)
    {
        TeleInstance = Instantiate(tele, callerPosition, Quaternion.identity);
        Destroy(TeleInstance.gameObject, TeleInstance.main.duration);
    }
    public void SpawnBoom1(Vector3 callerPosition)
    {
        Boom1Instance = Instantiate(boom1, callerPosition, Quaternion.identity);
        Destroy(Boom1Instance.gameObject, Boom1Instance.main.duration);
    }

    public void SpawnChase(Vector3 callerPosition)
    {
        ChaseInstance = Instantiate(chase, callerPosition, Quaternion.identity);
        Destroy(ChaseInstance.gameObject, ChaseInstance.main.duration);
    }
}