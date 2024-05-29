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

public class PlayerController : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rigid;
    public static PlayerController instance;
    public GameObject swordGameObject, skillGameObject;
    GameObject feet;
    CapsuleCollider2D capSword, capSkill, bodyPlayer;
    BoxCollider2D feetBoxCollider;
    EdgeCollider2D edgePlayer;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public bool doJump, doAttack; //cho phép dc attack hoặc jump liên tiếp
    public bool isAttackExactly; //Player đánh trúng monster?
    public bool beImmortal; //Player có bất tử ko?
    public bool isDie; //Player die chưa?
    public int p_maxHealth, p_MaxMana, p_CurrentXP, p_MaxXP, p_Level, p_Attack, p_Defend, p_manaOfSkill;
    public float p_currentManaFloat, p_currentManaFade, p_currentHealthFloat, p_currentHealthFade;
    public bool isIntervalSkill; //SKill đang dc thực hiện gây damage liên tục
   

    private void Awake()
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
        capSword = swordGameObject.GetComponent<CapsuleCollider2D>();
        capSkill = skillGameObject.GetComponent<CapsuleCollider2D>();
        bodyPlayer = GetComponent<CapsuleCollider2D>();
        edgePlayer = GetComponent<EdgeCollider2D>();
        feet = GameObject.Find("Feet");
        feetBoxCollider=feet.GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        doJump = true; doAttack = true;
        p_maxHealth = 100; p_currentHealthFloat = p_maxHealth; p_currentHealthFade = p_maxHealth;
        p_MaxMana = 100; p_currentManaFloat = p_MaxMana; p_currentManaFade = p_MaxMana;
        p_manaOfSkill = 40;
        p_CurrentXP = 0; p_MaxXP = 100; p_Level = 1;
        p_Attack = Random.Range(50, 60); p_Defend = Random.Range(10, 20);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDie)
        {
            Run();
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
        if (!isDie)
        {
            if (!doAttack)
            {
                return;
            }

            swordGameObject.SetActive(true);
            isAttackExactly = true;
            Animation.instance.state = State.Attack;
            doAttack = false;
            Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());

        }
    }
    public void PlayerSkill()
    {
        if (!isDie && p_currentManaFloat > p_manaOfSkill)
        {
            Animation.instance.state = State.ChargeSkill;
        }
    }

    void OnJump(InputValue value)
    {
        if (!isDie)
        {
            if (!doJump) { return; }
            if (value.isPressed&& feetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
        
           if(feetBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
                rigid.velocity = new Vector2(moveInput.x * runSpeed * 0.2f, rigid.velocity.y);
                if(!(Animation.instance.state == State.Jump))
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
        doAttack = true; doJump = true;
        swordGameObject.SetActive(false);
    }
    void DeactiveImmortal()
    {
        beImmortal = false;
    }
    public void DelayDeactiveImmortal()
    {
        Invoke("DeactiveImmortal", 3);
    }
    /*  void AttackExactly(Collision2D colliderMon)
      {
          if (colliderMon.collider.TryGetComponent(out Monster monBeHit))
          {
          }
      }*/
    public void GetLevel()  // Điều chỉnh XP và tăng LV
    {
        if (p_CurrentXP > p_MaxXP)
        {
            p_CurrentXP = p_CurrentXP - p_MaxXP;
            p_MaxXP += 10;
            p_Level++;
            Animation.instance.state = State.LevelUp;
            p_maxHealth += 10; p_MaxMana += 10;
            p_currentHealthFloat = p_maxHealth; p_currentManaFloat = p_MaxMana; p_currentHealthFade = p_maxHealth; p_currentManaFade = p_MaxMana;
            p_Attack += 5; p_Defend += 5;
        }

        UIManager.instance.levelPlayerTMP.text = p_Level.ToString();
    }
    public void PlayerBeingAttacked(int damage) //Player bị tấn công
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
        UIManager.instance.ShowDamageDealByMonster(damage);

    }
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
  
