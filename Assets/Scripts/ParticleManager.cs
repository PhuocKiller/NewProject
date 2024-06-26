using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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
    public  ParticleSystem rage,b_Die,chase, boom1, skill_1, skillMelee,skillRange, die, attack, level, blood, tele, heal, mana;
    public ParticleSystem[] skinParticle;
    private ParticleSystem skill_1Instance, levelInstance, attackInstance, dieInstance, 
        skillMelee_Instance, skillRange_Instance,bloodInstance, teleInstance,healInstance, manaInstance;
    private ParticleSystem ChaseInstance, Boom1Instance;

   

    public void SpawnHeal(Vector3 callerPosition)
    {
        healInstance = Instantiate(heal, callerPosition, Quaternion.identity);
        Destroy(healInstance.gameObject, healInstance.main.duration);
        healInstance.transform.parent = PlayerController.instance.transform;
    }
    public void SpawnMana(Vector3 callerPosition)
    {
        manaInstance = Instantiate(mana, callerPosition, Quaternion.identity);
        Destroy(manaInstance.gameObject, manaInstance.main.duration);
        manaInstance.transform.parent = PlayerController.instance.transform;
    }
    public void SpawnBlood(Vector3 callerPosition)
    {
        bloodInstance = Instantiate(blood, callerPosition, Quaternion.identity);
        Destroy(bloodInstance.gameObject, bloodInstance.main.duration);
    }
    public void SpawnDie(Vector3 callerPosition)
    {
        dieInstance = Instantiate(die, callerPosition, Quaternion.identity);
        Destroy(dieInstance.gameObject, die.main.duration);
    }
    public void SpawnAttack()
    {
        attackInstance = Instantiate(attack, attackPoint.GetChild(0).position, Quaternion.identity);
        Destroy(attackInstance.gameObject, attackInstance.main.duration);

    }

    public void SpawnLevel(Vector3 callerPosition)
    {
        levelInstance = Instantiate(level, callerPosition, Quaternion.identity);
        Destroy(levelInstance.gameObject, levelInstance.main.duration);
    }
    public void SpawnSkill_1()
    {

        skill_1Instance = Instantiate(skill_1, skill1Point.GetChild(0).position, Quaternion.identity);
        Destroy(skill_1Instance.gameObject, skill_1Instance.main.duration);
    }
    public void SpawnSkill(Vector3 callerPosition)
    {
        if (PlayerController.instance.characterType==CharacterType.Melee)
        {
            skillMelee_Instance = Instantiate(skillMelee, callerPosition, Quaternion.identity);
            Destroy(skillMelee_Instance.gameObject, skillMelee_Instance.main.duration);
        }
        else
        {
            skillRange_Instance = Instantiate(skillRange, callerPosition, Quaternion.identity);
            Destroy(skillRange_Instance.gameObject, skillRange_Instance.main.duration);
        }
    }
    public void SpwanSkillRepeat()
    {
        InvokeRepeating("SpawnSkill",0, 0.3f);
    }


    //boss
    public void SpawnTele(Vector3 callerPosition)
    {
        teleInstance = Instantiate(tele, callerPosition, Quaternion.identity);
        Destroy(teleInstance.gameObject, teleInstance.main.duration);
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
