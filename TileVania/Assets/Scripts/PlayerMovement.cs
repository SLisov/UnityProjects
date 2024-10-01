using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField ]AudioClip m_Clip;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Collider2D myBodyCollider;
    Collider2D myFeetCollider;
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 7f;
    [SerializeField] Vector2 ascension = new Vector2 (20f,20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Quaternion relativeRotation = Quaternion.Euler(0, 0, -90);

    Animator myAnimator;
    bool isAlive = true;
    SpriteRenderer mySpriteRenderer;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    
    public void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Lava")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            mySpriteRenderer.color = new Color(71f/255f, 168f/255f, 141f/255f, 255f/255f);
            myRigidbody.velocity = ascension;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void OnFire(InputValue value)
    {
        if (!isAlive) { return; }
        Instantiate(bullet, gun.position, relativeRotation);

    }


    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        myAnimator.SetBool("isRunning", Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        AudioSource.PlayClipAtPoint(m_Clip, playerVelocity);
             
    }

    void ClimbLadder()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            Vector2 climb = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climb;
            myAnimator.SetBool("isClimbing", Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon);
            myRigidbody.gravityScale = 0;
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = 2;
        }
        
    }
}
