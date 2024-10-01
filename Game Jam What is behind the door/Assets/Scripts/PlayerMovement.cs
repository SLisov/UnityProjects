using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 14f;
    [SerializeField] private float _playerSpeed = 7f;
    [SerializeField] private float _wallJumpForce = 10f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private AudioClip _clipJump;
    [SerializeField] private AudioClip _clipDoubleJump;
    [SerializeField] private AudioClip[] _clipFootStep;

    Animator _animator;
    Rigidbody2D _rb;
    AudioSource _audioSource;
    BoxCollider2D _boxCollider;

    public int amountOfJumps = 2;
    private int _amountOfJumpsLeft;
    private float _dirX = 0f;
    public float footstepDelay = 0.3f; // Seconds between each footstep sound
    private float _footstepTimer;
    public float airDecelerationFactor = 0.45f;
    public float wallSlideSpeed;
    public float airDeciliration = 60f;
    private float gravityAmplifier = 1.5f;
    public bool isDeath;
    private bool _isWalking;
    private bool _isWallSliding;
    private bool justWallJumped = false;
    private bool CanJump;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = _rb.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (isDeath) { return; }

        LookDiraction();
        GetInput();
        CheckIfWallSliding();
        UpdateAnimation();
        isGrounded();
    }

    private void FixedUpdate()
    {
        Movement();
        //GravityFallAmplifier();
    }

    private void UpdateAnimation()
    {
        _animator.SetFloat("yVelocity", _rb.velocity.y);
        _animator.SetBool("WallSlide", _isWallSliding);
        _animator.SetBool("Run", _isWalking);
    }

    #region Movement

    private void GetInput()
    {
        _dirX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            CheckIfCanJump();
            PlayerJump();
        }
    }

    private void Movement()
    {
        if (!Mathf.Approximately(_dirX, 0f))
        {
            _rb.velocity = new Vector2(_dirX * _playerSpeed, _rb.velocity.y);
        }

        if (_dirX == 0f && !justWallJumped)
        {
            _rb.velocity = new Vector2(Mathf.MoveTowards(_rb.velocity.x, 0, airDeciliration * Time.fixedDeltaTime), _rb.velocity.y);
        }

        if (_isWallSliding)
        {
            if(_rb.velocity.y < -wallSlideSpeed)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
            }
        }

    }

    #endregion

    #region Jump

    private void PlayerJump()
    {   
        if (CanJump)
        {
            if (isGrounded() || inAir() && !onWall())
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

                Debug.Log("Jump!");

                if (_amountOfJumpsLeft == 2)
                {
                    normalJump();
                    //Debug.Log("Normal Jump!");
                }
                else
                {
                    doubleJump();
                    //Debug.Log("Double Jump!");
                }
            }
            else if (onWall())
            {
                wallJump();
            }

            _amountOfJumpsLeft--;
        }
        Debug.Log("Amounts of jumps left: " + _amountOfJumpsLeft);

    }

    private bool inAir() 
    {
        if (Mathf.Abs(_rb.velocity.y) > 0.01f) Debug.Log("In Air!");
        return Mathf.Abs(_rb.velocity.y) > 0.01f;
    }

    private void CheckIfCanJump()
    {
        if (isGrounded() && !inAir() || onWall())
        {
            _amountOfJumpsLeft = amountOfJumps;
        }

        if (_amountOfJumpsLeft == 0)
        {
            CanJump = false;

        }
        else
        {
            CanJump = true;
        }
    }

    void ResetWallJumpFlag()
    {
        justWallJumped = false;
    }

    private void normalJump()
    {
        _animator.SetBool("Idle", false);
        _animator.SetTrigger("Jump");
        _audioSource.PlayOneShot(_clipJump);
    }

    private void doubleJump()
    {
        _animator.SetBool("Idle", false);
        _animator.SetTrigger("Jump");
        _audioSource.PlayOneShot(_clipDoubleJump);
    }

    private void wallJump()
    {
        justWallJumped = true;
        transform.localScale = new Vector2(-Mathf.Sign(transform.localScale.x), transform.localScale.y);
        _rb.velocity = new Vector2(transform.localScale.x * _wallJumpForce, _jumpForce);
        _audioSource.PlayOneShot(_clipJump);
        Invoke("ResetWallJumpFlag", 0.5f);
    }

    //private void GravityFallAmplifier()
    //{
    //    if (inAir())
    //    {
    //        if (_rb.velocity.y < 0.01f)
    //        {
    //            Debug.Log("Moving Gravity!");
    //            _rb.gravityScale = Mathf.MoveTowards(_rb.gravityScale, _rb.gravityScale * gravityAmplifier, 0.5f);
    //        }
    //    }
    //    else
    //    {
    //        _rb.gravityScale = 3f;
    //    }
    //}


    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);

        if (layerName == "Ground")
        {
            _animator.SetBool("Idle", true);
        }
    }

    private void LookDiraction()
    {
        if (!Mathf.Approximately(_dirX, 0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(_dirX), transform.localScale.y, transform.localScale.z);

            if (!inAir() && !onWall())
            {
                Debug.Log("WalkinG!");
                _isWalking = true;
            }
            else
            {
                _isWalking = false;
            }

            _footstepTimer += Time.deltaTime;

            if (_footstepTimer > footstepDelay && isGrounded())
            {
                int index = Random.Range(0, _clipFootStep.Length);
                _audioSource.PlayOneShot(_clipFootStep[index]);
                _footstepTimer = 0f;
            }
        }
        else
        {
            _isWalking = false;
        }
    }

    private void CheckIfWallSliding()
    {
        if (onWall() && !isGrounded() && _rb.velocity.y < 0) 
        {
            _isWallSliding = true;
        }
        else
        {
            _isWallSliding = false;
        }
    }

    private bool isGrounded()
    {
        //RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);
        ////if (raycastHit.collider != null) Debug.Log("Grounded!");
        //return raycastHit.collider != null;

        Collider2D CollidesGround = Physics2D.OverlapBox(_boxCollider.bounds.center,_boxCollider.bounds.size,0,_groundLayer);
        if (CollidesGround != null) Debug.Log("Grounded!");
        return CollidesGround != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, _wallLayer);
        //if (raycastHit.collider != null) Debug.Log("On Wall");
        return raycastHit.collider != null;

    }
}
