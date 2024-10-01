using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidbody;
    Collider2D enemyFeetCollider;
    bool movingForward = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyFeetCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        //if (enemyFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        //{
        //    Debug.Log("touch");
        //    moveSpeed = moveSpeed * -2;
        //}
        myRigidbody.velocity = new Vector2(moveSpeed, 0);
        FlipSprite();

    }
    void FlipSprite()
    {

        transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
    }


}
