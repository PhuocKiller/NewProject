using Spine;
using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;
public enum CharacterType
{
    Melee, Range
}
public class PlayerController : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rigid;
    public static PlayerController instance;
    public GameObject attackGameObject, mainSkillGameObject, skill_1_GameObject;
    GameObject feet;
    CapsuleCollider2D capAttack, capMainSkill, bodyPlayer, feetCapCollider; //collider các skill Range body Player
   // BoxCollider2D arrowBox, skillRangeBox; //collider các skill Range
    EdgeCollider2D edgePlayer;
    MeshRenderer meshRenderer;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public bool doJump, doAttack; //cho phép dc attack_Melee hoặc jump liên tiếp
    public bool isAttackExactly; //Player đánh trúng monster?
    public bool beImmortal, beFadeIncrease; //Player có bất tử ko?
    public bool isDie; //Player die chưa?
    public int p_maxHealth, p_MaxMana, p_CurrentXP, p_MaxXP, p_Level, p_Attack, p_Defend, p_manaCostMainSkill, p_manaCostSkill_1;
    public float p_currentManaFloat, p_currentManaFade, p_currentHealthFloat, p_currentHealthFade;
    public bool isIntervalSkill; //SKill đang dc thực hiện gây damage liên tục
    public int numberIndexCharacter, coins; public CharacterType characterType; //thông tin khi save
    public int posIndex; //vị trí Player khi chuyển scene
    bool isPressMove;
    float timeJump;
    public float deltaDamage;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);  //xóa cái mới sinh ra
            }

        }
        DontDestroyOnLoad(gameObject);
        rigid = GetComponent<Rigidbody2D>();
        capAttack = attackGameObject.GetComponent<CapsuleCollider2D>();
        capMainSkill = mainSkillGameObject.GetComponent<CapsuleCollider2D>();
        bodyPlayer = GetComponent<CapsuleCollider2D>();
        edgePlayer = GetComponent<EdgeCollider2D>();
        feet = GameObject.Find("Feet");
        feetCapCollider=feet.GetComponent<CapsuleCollider2D>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        doAttack = true;
        if(p_Level==0) { p_Level = 1; }
        UpdateLevelPlayer();
        deltaDamage = 0.06f;
        ParticleManager.instance.SetSkinParticle(p_Level);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDie)
        {
            if (Input.GetKey(KeyCode.Space)) { Jump(); }
            if (!doJump)
            {
                timeJump += Time.deltaTime;
            }
            else { timeJump=0; }
            if (Animation.instance.state!=State.Injured)
            {
                Run();
            }
            FadeImmortal();
        }
        
    }
   
    public void PlayerAttack()
    {
        
            if (!isDie&& Animation.instance.state==State.Idle &&doAttack)
        {
            if (PlayerController.instance.characterType == CharacterType.Melee)
            {
                attackGameObject.SetActive(true);
                isAttackExactly = true;
                Animation.instance.state = State.Attack;
                Invoke("DelaySword", 0.2f);
                doAttack = false;
                Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());
            }
            else
            {
                attackGameObject.SetActive(true);
                isAttackExactly = true;
                Invoke("DelayArrow", 0.3f); //Arrow ko bay liền ra khi ấn button
                Animation.instance.state = State.Attack;
                doAttack = false;
                Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());
            }
        }
    }
    public void PlayerSkill_1()
    {
        if (!isDie && Animation.instance.state == State.Idle && doAttack  )
        {
            if (p_currentManaFloat >= p_manaCostSkill_1)
            {
                p_currentManaFade -= p_manaCostSkill_1; p_currentManaFloat -= p_manaCostSkill_1;
                if (PlayerController.instance.characterType == CharacterType.Melee)
                {
                    skill_1_GameObject.SetActive(true);
                    isAttackExactly = true;
                    Animation.instance.state = State.Skill1;
                    Invoke("DelaySword", 0.2f);
                    doAttack = false;
                    Invoke("SetIdleState", Animation.instance.GetTimeOfSkill_1_Animation());
                }
                else
                {
                    skill_1_GameObject.SetActive(true);
                    isAttackExactly = true;
                    Invoke("DelayArrow", 0.75f); //Arrow ko bay liền ra khi ấn button
                    Animation.instance.state = State.Skill1;
                    doAttack = false;
                    Invoke("SetIdleState", Animation.instance.GetTimeOfSkill_1_Animation());
                }
            }
            else
            {
                AudioManager.instance.PlaySound(AudioManager.instance.error);
            }

        }
        
    }
    void DelaySword()
    {
        attackGameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
        skill_1_GameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -9);

    }
    void DelayArrow()
    {
        skill_1_GameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(-25f * skill_1_GameObject.transform.lossyScale.x, skill_1_GameObject.GetComponent<Rigidbody2D>().velocity.y);
        attackGameObject.GetComponent<Rigidbody2D>().velocity =
                new Vector2(-20f * attackGameObject.transform.lossyScale.x, attackGameObject.GetComponent<Rigidbody2D>().velocity.y);
        
    }
    void SetIdleState()
    {
        if (!isDie)
        {
            Animation.instance.state = State.Idle;
            if (characterType == CharacterType.Melee)
            {
                doAttack = true; doJump = true;
                attackGameObject.SetActive(false);
                attackGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                skill_1_GameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                skill_1_GameObject.SetActive(false);
            }
            else
            {
                doAttack = true; doJump = true;
                attackGameObject.SetActive(false);
                skill_1_GameObject.SetActive(false);
                attackGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                skill_1_GameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
        }
    }
    public void PlayerSkill()
    {
        if (!isDie  && Animation.instance.state == State.Idle)
        {
            if (p_currentManaFloat > p_manaCostMainSkill)
            {
                Animation.instance.state = State.ChargeSkill;

            }
            else
            {
                AudioManager.instance.PlaySound(AudioManager.instance.error);
            }
        }
      
    }

    void Jump()
    {
        if (!isDie&&doJump&& feetCapCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))&&
           ( Animation.instance.state == State.Idle|| Animation.instance.state == State.Run))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed); 
                Animation.instance.state = State.Jump;
                doJump = false;
        }
    }
    void Run()
    {
        if (!isDie && !(Animation.instance.state == State.Attack))
        {
            moveInput = new Vector2(Input.GetAxis("Horizontal"), rigid.velocity.y);
        }
            
        rigid.velocity = new Vector2(moveInput.x * runSpeed, rigid.velocity.y);


        bool playerHasHorizontalSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        
           if(feetCapCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                if (playerHasHorizontalSpeed && !(Animation.instance.state == State.Attack))
                {
                    if (timeJump>0.2f ||timeJump==0) //khi jump đủ time hoặc khi ko jump thì Run
                        {
                            Animation.instance.state = State.Run;
                    if (!Input.GetKey(KeyCode.Space))
                    {
                        doJump = true;
                    }
                }
                
                    rigid.velocity = new Vector2(moveInput.x * runSpeed, rigid.velocity.y);
                }
                else
                {
                    if (!(Animation.instance.state == State.Attack) 
               && !Input.GetMouseButton(0)
               && !(Animation.instance.state == State.LevelUp)
               && !(Animation.instance.state == State.Injured) && !(Animation.instance.state == State.Skill1))
                {
                    if (timeJump > 0.2f || timeJump == 0) //khi jump đủ time hoặc khi ko jump thì Idle
                    {
                        Animation.instance.state = State.Idle;
                        if (!Input.GetKey(KeyCode.Space))
                        {
                            doJump = true;
                        }
                    }
                }
                }
                    
            }
           else
            {
                rigid.velocity = new Vector2(moveInput.x * runSpeed * 0.4f, rigid.velocity.y); //0.3 là độ delay di chuyển horizontal
                if(!(Animation.instance.state == State.Jump) && !(Animation.instance.state == State.Injured)
                &&!(Animation.instance.state == State.Die))
                {
                Animation.instance.state = State.Fall;
                }
            }
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(-Mathf.Sign(rigid.velocity.x), 1f);  //mathf.Sign là trả về giá trị -1 hoặc 1 để chọn hướng
        }


    }
   
    public void GetLevel()  // Điều chỉnh XP và tăng LV
    {
        if (p_CurrentXP >= p_MaxXP)
        {
            p_CurrentXP = p_CurrentXP - p_MaxXP;
            p_Level++;
            ParticleManager.instance.SetSkinParticle(p_Level);
            if (p_Level==5)
            {
                UIManager.instance.panelSkinLv5.SetActive(true);
            }
            if (p_Level == 10)
            {
                UIManager.instance.panelSkinLv10.SetActive(true);
            }
            Animation.instance.state = State.LevelUp;
            UpdateLevelPlayer();
        }
        
        Animation.instance.SetupSkins(p_Level);
        UIManager.instance.levelPlayerTMP.text = p_Level.ToString();
    }
    void UpdateLevelPlayer()
    {
        p_maxHealth = 100 * p_Level;
        p_MaxMana = 100 + 50 * (p_Level - 1);
        FullEngergy();
        p_manaCostMainSkill = 40 + 2 * (p_Level - 1);
        p_manaCostSkill_1 = 10 + (p_Level - 1);
        p_MaxXP = 100 * p_Level*p_Level;
        p_Attack = 50 + 50 * (p_Level - 1); p_Defend = 15 + 5 * (p_Level - 1);
    }
    public void FullEngergy()
    {
        p_currentHealthFloat = p_maxHealth; p_currentHealthFade = p_maxHealth;
        p_currentManaFloat = p_MaxMana; p_currentManaFade = p_MaxMana;
    }
    public void PlayerBeingAttacked(float damage, bool isBossSkill = false) //Player bị tấn công
    {
        if (beImmortal ||isDie) { return; }
        if (!isBossSkill)
        {
            beImmortal = true;
            Invoke("DeactiveImmortal", 3);
        }
        ParticleManager.instance.SpawnBlood(new Vector2(transform.position.x, transform.position.y+2f));
        p_currentHealthFloat = p_currentHealthFloat - damage;
        p_currentHealthFade = p_currentHealthFade - damage;
        UIManager.instance.ShowDamageDealByMonster((int)damage);
        if (p_currentHealthFloat < 0)
        {
            p_currentHealthFloat = 0; p_currentHealthFade = 0;
            Animation.instance.state = State.Die;
            isDie = true;
            Invoke("PlayAgain",3);
            return;

        }
        
        Animation.instance.state = State.Injured;
        
    }
    void DeactiveImmortal()
    {
        beImmortal = false;
    }
    void PlayAgain()
    {
        UIManager.instance.panelPlayAgain.SetActive(true);
    }

    void FadeImmortal()
    {
        if (beImmortal)
        {
            if (!beFadeIncrease)
            {
                meshRenderer.material.color = new Color(1, 1, 1, meshRenderer.material.color.a - 5*Time.deltaTime);
                if (meshRenderer.material.color.a<0.3) { beFadeIncrease = true; }
            }
            else
            {
                meshRenderer.material.color = new Color(1, 1, 1, meshRenderer.material.color.a + 5*Time.deltaTime);
                if (meshRenderer.material.color.a>=1) {  beFadeIncrease = false; }
            }
            
        }
        else
        {
            meshRenderer.material.color = new Color(1, 1, 1, 1);
        }
    }//chớp nháy khi ở trạng thái bất tử
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
      
    }
    private void OnCollisionEnter2D(Collision2D hit)
    {
        IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            Inventory.instance.AddItem(item);
        }
    }
    
}
  
