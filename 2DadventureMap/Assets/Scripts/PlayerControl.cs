using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Player player;
    bool onGround = false;

    [Range(0, .5f)] [SerializeField] public float m_MovementSmoothing = .05f;
    public Transform groundCheck;
    public float groundedRadius = .2f;
    public Animator animator;
    AnimationManager animationManager;
    private Vector3 m_Velocity = Vector3.zero;
    public Rigidbody2D rb;
    float horizontalValue;
    float verticalValue;
    //Player Combat
    public Transform attackPoint;
    public float attackRange = .5f;
    public LayerMask enemyLayer;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animationManager = new AnimationManager(animator);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        
        horizontalValue = Input.GetAxisRaw("Horizontal");
        verticalValue = Input.GetAxisRaw("Vertical");
        if (horizontalValue != 0 && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveCharacter(new Vector2(horizontalValue * PlayerValues.RunValue, rb.velocity.y), Input.GetKey(KeyCode.RightArrow));
            animationManager.Run();
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (Time.time >= nextAttackTime)
                {
                    animationManager.attack();
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            animationManager.notRun();
        }
        if (onGround)
        {
            if (verticalValue > 0)
            {
                animationManager.Jump();
                jumpCharacter();
            }
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                onGround = true;
            }
        }
        
    }
    void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyTest>().takeDamage(PlayerValues.DamageValue))
            {
                player.killed(enemy.GetComponent<EnemyTest>().enemyType);
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(groundCheck.position, groundedRadius);
        }
    }
    
    void moveCharacter(Vector2 movement, bool side)
    {
        if (side)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
        }
        rb.velocity = Vector3.SmoothDamp(rb.velocity, movement, ref m_Velocity, m_MovementSmoothing);

    }
    void jumpCharacter()
    {
        rb.velocity = new Vector2(rb.velocity.x, PlayerValues.JumpForce);
        onGround = false;
    }
}
