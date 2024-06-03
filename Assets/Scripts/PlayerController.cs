using Spine;
using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
    public GameObject swordGameObject, skillMeleeGameObject,arrowGameObject, skillRangeGameObject;
    GameObject feet;
    CapsuleCollider2D capSword, capSkillMelee, bodyPlayer;
    BoxCollider2D arrowBox, skillRangeBox;
    CircleCollider2D feetCircleCollider;
    EdgeCollider2D edgePlayer;
    MeshRenderer meshRenderer;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public bool doJump, doAttack; //cho phép dc attack hoặc jump liên tiếp
    public bool isAttackExactly; //Player đánh trúng monster?
    public bool beImmortal, beFadeIncrease; //Player có bất tử ko?
    public bool isDie; //Player die chưa?
    public int p_maxHealth, p_MaxMana, p_CurrentXP, p_MaxXP, p_Level, p_Attack, p_Defend, p_manaOfSkill;
    public float p_currentManaFloat, p_currentManaFade, p_currentHealthFloat, p_currentHealthFade;
    public bool isIntervalSkill; //SKill đang dc thực hiện gây damage liên tục
    public int numberIndexCharacter;public CharacterType characterType; //thông tin khi save



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
        if (characterType==CharacterType.Melee)
        {
            capSword = swordGameObject.GetComponent<CapsuleCollider2D>();
            capSkillMelee = skillMeleeGameObject.GetComponent<CapsuleCollider2D>();
        }
        else
        {
            arrowBox=GetComponent<BoxCollider2D>();
            skillRangeBox= GetComponent<BoxCollider2D>();
        }
        bodyPlayer = GetComponent<CapsuleCollider2D>();
        edgePlayer = GetComponent<EdgeCollider2D>();
        feet = GameObject.Find("Feet");
        feetCircleCollider=feet.GetComponent<CircleCollider2D>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        doJump = true; doAttack = true;
        p_maxHealth = 100 + 10*(p_Level-1); p_currentHealthFloat = p_maxHealth; p_currentHealthFade = p_maxHealth;
        p_MaxMana = 100 + 10 * (p_Level - 1); p_currentManaFloat = p_MaxMana; p_currentManaFade = p_MaxMana;
        p_manaOfSkill = 40 + 2 * (p_Level - 1);
        p_CurrentXP = 0; p_MaxXP = 100 + 10 * (p_Level - 1);
        p_Attack = 50+ 20 * (p_Level - 1); p_Defend =15 + 2 * (p_Level - 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDie)
        {
            Run();
            FadeImmortal();
        }
    }
    void OnMove(InputValue value)
    {
        if (!isDie&& !(Animation.instance.state==State.Attack))
        {
            moveInput = value.Get<Vector2>();
        }
    }
    public void PlayerAttack()
    {
        
            if (!isDie&& Animation.instance.state==State.Idle &&doAttack)
        {
            if (PlayerController.instance.characterType == CharacterType.Melee)
            {
                swordGameObject.SetActive(true);
                isAttackExactly = true;
                Animation.instance.state = State.Attack;
                doAttack = false;
                Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());
            }
            else
            {
                arrowGameObject.SetActive(true);
                isAttackExactly = true;
                arrowGameObject.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(-20f* arrowGameObject.transform.lossyScale.x, arrowGameObject.GetComponent<Rigidbody2D>().velocity.y);
                Animation.instance.state = State.Attack;
                doAttack = false;
                Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());
            }
        }
    }
    public void PlayerSkill()
    {
        if (!isDie && p_currentManaFloat > p_manaOfSkill && Animation.instance.state == State.Idle)
        {
            Animation.instance.state = State.ChargeSkill;
        }
    }

    void OnJump(InputValue value)
    {
        if (!isDie && Animation.instance.state == State.Idle)
        {
            if (!doJump) { return; }
            if (value.isPressed)
            {
                rigid.velocity = new Vector2(0, jumpSpeed);
                Animation.instance.state = State.Jump;
                doJump = false;
                Invoke("SetIdleState", Animation.instance.GetTimeOfJumpAnimation());
            }
        }
    }
    void Run()
    {
        rigid.velocity = new Vector2(moveInput.x * runSpeed, rigid.velocity.y);


        bool playerHasHorizontalSpeed = Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        
           if(feetCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                if (playerHasHorizontalSpeed && !(Animation.instance.state == State.Attack))
                {
                    Animation.instance.state = State.Run;
                    rigid.velocity = new Vector2(moveInput.x * runSpeed, rigid.velocity.y);
                }
                else
                {
                    if (!(Animation.instance.state == State.Attack) && !(Animation.instance.state == State.Jump)
               && !Input.GetMouseButton(0)
               && !(Animation.instance.state == State.LevelUp)
               && !(Animation.instance.state == State.Injured))
                    {
                        Animation.instance.state = State.Idle;
                    }
                }
                    
            }
           else
            {
                rigid.velocity = new Vector2(moveInput.x * runSpeed * 0.3f, rigid.velocity.y); //0.3 là độ delay di chuyển horizontal
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
    void SetIdleState()
    {
        Animation.instance.state = State.Idle;
        if (characterType==CharacterType.Melee)
        {
            doAttack = true; doJump = true;
            swordGameObject.SetActive(false);
        }
        else
        {
            doAttack = true;
            arrowGameObject.SetActive(false);
            arrowGameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
  
    public void GetLevel()  // Điều chỉnh XP và tăng LV
    {
        if (p_CurrentXP >= p_MaxXP)
        {
            p_CurrentXP = p_CurrentXP - p_MaxXP;
            p_Level++;
            Animation.instance.state = State.LevelUp;
            p_maxHealth = 100 + 10 * (p_Level - 1); p_currentHealthFloat = p_maxHealth; p_currentHealthFade = p_maxHealth;
            p_MaxMana = 100 + 10 * (p_Level - 1); p_currentManaFloat = p_MaxMana; p_currentManaFade = p_MaxMana;
            p_manaOfSkill = 40 + 2 * (p_Level - 1);
            p_MaxXP = 100 + 10 * (p_Level - 1);
            p_Attack = 50 + 20 * (p_Level - 1); p_Defend = 15 + 2 * (p_Level - 1);
        }

        UIManager.instance.levelPlayerTMP.text = p_Level.ToString();
    }
    public void PlayerBeingAttacked(float damage) //Player bị tấn công
    {
        if (beImmortal) { return; }
        beImmortal = true;
        p_currentHealthFloat = p_currentHealthFloat - damage;
        p_currentHealthFade = p_currentHealthFade - damage;
        if (p_currentHealthFloat < 0)
        {
            p_currentHealthFloat = 0; p_currentHealthFade = 0;
            Animation.instance.state = State.Die;
            isDie = true;
            return;

        }
        DelayDeactiveImmortal();
        Animation.instance.state = State.Injured;
        UIManager.instance.ShowDamageDealByMonster((int)damage);
    }
    void DeactiveImmortal()
    {
        beImmortal = false;
    }
    public void DelayDeactiveImmortal()
    {
        Invoke("DeactiveImmortal", 3);
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
  
