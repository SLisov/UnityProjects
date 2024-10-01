using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RocketLunch : MonoBehaviour
{
    public float flySpeed = 1200f;
    public float strengthHit = 15f;
    public GameObject enemy;
    Rigidbody rbRocket;
    Transform target;



    void Start()
    {
        rbRocket = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (enemy != null) 
        {
                Vector3 lookDirection = (enemy.transform.position - transform.position).normalized;
                rbRocket.AddForce(lookDirection * flySpeed * Time.deltaTime);
                target = enemy.transform;
                transform.LookAt(target);
                Destroy(gameObject,5f);
        }
        else
        {
            Destroy(gameObject);
        }

        if (transform.position.z > 12.2 || transform.position.z < -12.2)
        {
            Destroy(gameObject);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            rb.AddForce(awayFromPlayer * strengthHit * Time.deltaTime, ForceMode.Impulse);
            Destroy(gameObject);
        }

    }
}
