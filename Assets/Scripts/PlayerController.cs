using Spine;
using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    Vector2 moveInput;
    Rigidbody2D rigid;
    public static PlayerController instance;
    public GameObject swordGameObject, skillGameObject;
    CapsuleCollider2D capSword, capSkill, bodyPlayer;
    
    EdgeCollider2D edgePlayer;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public bool doJump, doAttack; //cho phép dc attack hoặc jump liên tiếp
    public bool isAttackExactly; //Player đánh trúng monster?
    public bool beImmortal; //Player có bất tử ko?
    public bool isDie; //Player die chưa?
    public int p_maxHealth, p_currentHealth, p_MaxMana, p_CurrentXP, p_MaxXP, p_Level, p_Attack, p_Defend, manaOfSkill;
    public float p_currentManaFloat;
    public bool isIntervalSkill; //SKill đang dc thực hiện gây damage liên tục
    public TextMeshProUGUI levelPlayerTMP;
    public static PlayerController instanceNew;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rigid = GetComponent<Rigidbody2D>();
        capSword=swordGameObject.GetComponent<CapsuleCollider2D>();
        capSkill=skillGameObject.GetComponent<CapsuleCollider2D>();
        bodyPlayer = GetComponent<CapsuleCollider2D>();
        edgePlayer =GetComponent<EdgeCollider2D>();


    }
    // Start is called before the first frame update
    void Start()
    {
        doJump = true; doAttack = true;
        p_maxHealth = 100; p_currentHealth = p_maxHealth;
        p_MaxMana = 100; p_currentManaFloat = p_MaxMana;
        manaOfSkill = 40;
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
      if (!isDie)
        {
            moveInput = value.Get<Vector2>();
        }
    }
    void OnAttack(InputValue value)
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
    void OnSkill(InputValue value)
    {
        if (!isDie&&p_currentManaFloat>manaOfSkill)
        {
            Animation.instance.state = State.ChargeSkill;
        }
    }

    void OnJump(InputValue value)
        {  if (!isDie)
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
        rigid.velocity = new Vector2(moveInput.x*runSpeed, rigid.velocity.y);
        bool playerHasHorizontalSpeed= Mathf.Abs(rigid.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed && !(Animation.instance.state == State.Attack) 
            && bodyPlayer.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
                Animation.instance.state = State.Run;
        }

        else
        {
            if (!(Animation.instance.state == State.Attack) && !(Animation.instance.state == State.Jump)
                && !Input.GetMouseButton(1)
                && !(Animation.instance.state == State.LevelUp) 
                && !(Animation.instance.state == State.Injured))
            {
                Animation.instance.state = State.Idle;
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
        doAttack = true; doJump=true;
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
        if(p_CurrentXP>p_MaxXP)
        {
            p_CurrentXP=p_CurrentXP-p_MaxXP;
            p_MaxXP += 10;
            p_Level++;
            Animation.instance.state = State.LevelUp;
            p_maxHealth +=10;  p_MaxMana+= 10;
            p_currentHealth = p_maxHealth; p_currentManaFloat = p_MaxMana;
            p_Attack += 5; p_Defend += 5;
        }
        
        levelPlayerTMP.text = p_Level.ToString();
    }
    public void PlayerBeingAttacked(int damage) //Player bị tấn công
    {
            if (PlayerController.instance.beImmortal) { return; }
            PlayerController.instance.beImmortal = true;
            PlayerController.instance.p_currentHealth = PlayerController.instance.p_currentHealth - damage;
            if (PlayerController.instance.p_currentHealth < 0)
            {
                PlayerController.instance.p_currentHealth = 0;
                Animation.instance.state = State.Die;
                PlayerController.instance.isDie = true;
                return;

            }
            PlayerController.instance.DelayDeactiveImmortal();
            Animation.instance.state = State.Injured;
        
    }
}
