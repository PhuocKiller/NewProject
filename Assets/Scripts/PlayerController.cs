using Spine;
using Spine.Unity;
using Spine.Unity.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    Vector2 moveInput;
    Rigidbody2D rigid;
    public static PlayerController instance;
    public GameObject sword;
    CapsuleCollider2D capSword, bodyPlayer;
    
    EdgeCollider2D edgePlayer;
    public float runSpeed = 10f;
    public float jumpSpeed = 5f;
    public bool doJump, doAttack; //cho phép dc attack hoặc jump liên tiếp
    public bool isAttackExactly; //Player đánh trúng monster?
    public bool beImmortal; //Player có bất tử ko?
    public bool isDie; //Player die chưa?
    public int p_maxHealth, p_currentHealth, p_currentMana, p_MaxMana, p_CurrentXP, p_MaxXP, p_Level, p_Attack, p_Defend;
    public Bars healthBar, manaBar, XPBar;
    public TextMeshProUGUI levelPlayerTMP;
    public static PlayerController instanceNew;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        rigid = GetComponent<Rigidbody2D>();
        capSword=sword.GetComponent<CapsuleCollider2D>();
        bodyPlayer= GetComponent<CapsuleCollider2D>();
        edgePlayer =GetComponent<EdgeCollider2D>();


    }
    // Start is called before the first frame update
    void Start()
    {
        doJump = true; doAttack = true;
        p_maxHealth = 100; p_currentHealth = p_maxHealth;
        p_MaxMana = 100; p_currentMana = p_MaxMana;
        p_CurrentXP = 0; p_MaxXP = 100; p_Level = 1;
        p_Attack = Random.Range(50, 60); p_Defend = Random.Range(10, 20);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if(!isDie)
        {
            Run();
            healthBar.UpdateBar(p_currentHealth, p_maxHealth);
            manaBar.UpdateBar(p_currentMana, p_MaxMana);
            UpdateXP();

        }
    }
  
    void OnMove(InputValue value)
    {
            moveInput = value.Get<Vector2>();
    }
    void OnAttack(InputValue value)
    {
        if (!doAttack)
        {
            return;
        }
        
       if (value.isPressed)
        { sword.SetActive(true);
            isAttackExactly = true;
            Animation.instance.state = State.Attack;
            doAttack = false;
            Invoke("SetIdleState", Animation.instance.GetTimeOfAttackAnimation());
        }
    }
    void OnSkill(InputValue value)
    {
        Animation.instance.state = State.MainSkill;
    }

    
    void AttackExactly (Collision2D colliderMon)
    {
        if (colliderMon.collider.TryGetComponent(out Monster monBeHit)) 
        {
        }
    }
    void OnJump(InputValue value)
    {   if (!doJump) { return; }
        if (value.isPressed) 
        { 
            rigid.velocity = new Vector2(0, jumpSpeed);
            Animation.instance.state = State.Jump;
            doJump = false;
            Invoke("SetIdleState", Animation.instance.GetTimeOfJumpAnimation());
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
                && !(Animation.instance.state == State.MainSkill) && !(Animation.instance.state == State.ChargeSkill)
                && !(Animation.instance.state == State.LevelUp) && !(Animation.instance.state == State.MainSkill)
                && !(Animation.instance.state == State.Injured))
                Animation.instance.state = State.Idle;
            
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
        sword.SetActive(false);
    }
    void DeactiveImmortal()
    {
        beImmortal = false;
    }
    public void DelayDeactiveImmortal()
    {
        Invoke("DeactiveImmortal", 3);
    }
    void UpdateXP()
    {
        if(p_CurrentXP>p_MaxXP)
        {
            p_CurrentXP=p_CurrentXP-p_MaxXP;
            p_MaxXP += 10;
            p_Level++;
            Animation.instance.state = State.LevelUp;
        }
        XPBar.UpdateBar(p_CurrentXP, p_MaxXP);
        levelPlayerTMP.text = p_Level.ToString();
    }
   
}
