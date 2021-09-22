using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    AnimationManager animationManager;
    public Rigidbody2D rigidbody;

    public Transform attackPoint;
    public Transform castPoint;
    public Transform castPointSec;
    Transform target;
    public Transform basePosition;

    public SpriteRenderer spriteRenderer;

    public HealthBarScript healthBar;

    public LayerMask enemyLayer;
    public EnemyTypes enemyType;

    public int maxHealth = 100;

    int currentHealth;


    public float scanDistance;
    public float targetDistance;
    public float attackDistance;
    public float attackRange;
    

    public float moveSpeed;
    public int damage;

    public bool attackAble;

    public float attackRate = 5f;
    float nextAttackTime = 0f;

    void Start()
    {
        animationManager = new AnimationManager(GetComponent<Animator>());
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    bool side = true;
    void Update()
    {
        if (target != null)
        {
            
            if (isTargetClose(target,targetDistance))
            {
                if (transform.position.x < target.transform.position.x)
                {
                    side = true;
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else if (transform.position.x > target.transform.position.x)
                {
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                    side = false;
                }
                if (isTargetClose(target,attackDistance))
                {
                    animationManager.Idle();
                    if (Time.time >= nextAttackTime)
                    {
                        animationManager.attack();
                        nextAttackTime = Time.time + attackRate;
                    }

                }
                else
                {
                    chase(target);
                }
            }
            else if (!isTargetClose(basePosition,5))
            {
                chase(basePosition);
            }
            else
            {
                animationManager.Idle();
            }
        }
        else
        {
            sendRaycast(scanDistance);
            sendRay(scanDistance);
        }



        if (attackAble)
        {
            attack();
            attackAble = false;
        }
    }
    void sendRaycast(float distance)
    {

        Vector2 endPos = castPoint.position + Vector3.right * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag =="Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        Debug.DrawLine(castPoint.position, endPos, Color.blue);
    }
    void sendRay(float distance)
    {
        Vector2 endPos = castPointSec.position + Vector3.left * distance;
        RaycastHit2D hit = Physics2D.Linecast(castPointSec.position, endPos);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                target = hit.collider.gameObject.transform;
            }
        }
        Debug.DrawLine(castPointSec.position, endPos, Color.blue);
    }
    bool isTargetClose(Transform targetTransform, float range)
    {
        if (targetTransform == null)
        {
            return false;
        }
        if (range > Vector2.Distance(transform.position, targetTransform.position))
        {
            return true;
        }
        return false;
    }
    private void attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.transform.tag == "Player")
            {
                enemy.transform.gameObject.GetComponent<Player>().TakeDamage(damage);
            }
            /*
            if (enemy.transform.tag == "Enemy")
            {
                enemy.GetComponent<EnemyTest>().takeDamage(damage);
            }
            */
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    public bool takeDamage(int damage)
    {
        currentHealth -= damage;
        animationManager.Damage();
        healthBar.SetSlider(currentHealth);
        if (currentHealth <= 0)
        {
            die();
            return true;
        }
        return false;
    }
     void die()
    {
        spriteRenderer.color = Color.black;
        animationManager.Dead();
        Debug.Log("start cor");
        dead();
    }
    void dead()
    {
        StartCoroutine(WaitandDead(.40f));
    }
    IEnumerator WaitandDead(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("endcor");
        Destroy(gameObject);
    }


    private void chase(Transform targetTransform)
    {
        animationManager.Run();
        if (side)
        {
            rigidbody.velocity = new Vector2(moveSpeed, 0);
        }
        else
        {
            rigidbody.velocity = new Vector2(-moveSpeed, 0);
        }
        
    }
}
