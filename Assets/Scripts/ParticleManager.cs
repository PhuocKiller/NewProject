using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public  ParticleSystem b_Die,chase, boom1, skill_1, skill, die, attack, level, blood, tele, heal, mana;
    public ParticleSystem[] skinParticle;
    private ParticleSystem Skill_1Instance, LevelInstance, AttackInstance, DieInstance, SkillInstance,BloodInstance,TeleInstance,HealInstance, ManaInstance;
    private ParticleSystem ChaseInstance, Boom1Instance;

    //player

    public void SpawnHeal(Vector3 callerPosition)
    {
        HealInstance = Instantiate(heal, callerPosition, Quaternion.identity);
        Destroy(HealInstance.gameObject, HealInstance.main.duration);
        HealInstance.transform.parent = PlayerController.instance.transform;
    }
    public void SpawnMana(Vector3 callerPosition)
    {
        ManaInstance = Instantiate(mana, callerPosition, Quaternion.identity);
        Destroy(ManaInstance.gameObject, ManaInstance.main.duration);
        ManaInstance.transform.parent = PlayerController.instance.transform;
    }
    public void SpawnBlood(Vector3 callerPosition)
    {
        BloodInstance = Instantiate(blood, callerPosition, Quaternion.identity);
        Destroy(BloodInstance.gameObject, BloodInstance.main.duration);
    }
    public void SpawnDie(Vector3 callerPosition)
    {
        DieInstance = Instantiate(die, callerPosition, Quaternion.identity);
        Destroy(DieInstance.gameObject, die.main.duration);
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
       // Skill_1Instance.transform.parent = skill1Point.transform;
        Destroy(Skill_1Instance.gameObject, Skill_1Instance.main.duration);
    }
    public void SpawnSkill(Vector3 callerPosition)
    {
        SkillInstance = Instantiate(skill, callerPosition, Quaternion.identity);
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
    void HideAllSkinParticle()
    {
        for (int i = 0; i < skinParticle.Length; i++)
        {
            skinParticle[i].gameObject.SetActive(false);
        }
    }
    public void SetSkinParticle(int level)
    {
        HideAllSkinParticle();
        if (level<5)
        {
            skinParticle[0].gameObject.SetActive(true);
        }
        else if (level<10)
        {
            skinParticle[1].gameObject.SetActive(true);
        }
        else
        {
            skinParticle[2].gameObject.SetActive(true);
        }
    }
}
