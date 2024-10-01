using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public ParticleSystem smokeEffect;
    AudioSource brokenWalk;
    bool aggressive = true;
    Animator animator;
    public float enemySpeed = 3.0f;
    Vector2 newPosition;
    Rigidbody2D rigidbody2d;
    public bool vertical;
    public float timer = 3.0f;
    float currentTime;
    int enemyDirection = 1;
    public bool canMoveBothSides;
    float random;


    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        newPosition = rigidbody2d.position;
        animator = GetComponent<Animator>();
        brokenWalk = GetComponent<AudioSource>();
        currentTime = timer;

    }

    private void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            enemyDirection = - enemyDirection;
            currentTime = timer;

            random = Random.Range(1f, 3f);

            if (canMoveBothSides && !vertical && random > 2f && enemyDirection == 1)
            {
                vertical = true;
            }
            else if(canMoveBothSides && vertical && random < 2f && enemyDirection == 1)
            {
                vertical = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!aggressive)
        {
            return;
        }

        if (!vertical)
        {
            animator.SetFloat("Move X", -enemyDirection);
            animator.SetFloat("Move Y", 0);
            newPosition.x -= enemyDirection * enemySpeed * Time.deltaTime;
        }
        else
        {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", -enemyDirection);
            newPosition.y -= enemyDirection * enemySpeed * Time.deltaTime;

        }
        
        rigidbody2d.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            Debug.Log("HIT!");
        } 
    }

    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        brokenWalk.Stop();
        smokeEffect.Stop();
    }
}
