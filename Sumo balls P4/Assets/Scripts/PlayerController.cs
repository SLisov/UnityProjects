using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float powerUpStrength = 10f;
    public float explosionForce = 2f;
    public float explosionRadius = 5f;
    bool isFalling = false;
    public bool hasPowerUp = false;
    public bool hasPowerUpRocket = false;
    public bool hasPowerUpSmash = false;
    public GameObject powerUpIndicator;
    public GameObject rocketsLunch;
    private GameObject focalPoint;
    Rigidbody playerRb;
    /// <summary>
    bool smashing = false;
    float floorY;
    public float hangTime = 0.3f;
    public float smashSpeed = 50f;
    /// </summary>
    void Start()
    {
        // Disable powerup Indicator
        powerUpIndicator.gameObject.SetActive(false);
        // Get Component RigidBody of the Player
        playerRb = GetComponent<Rigidbody>();
        // Get empty GameObject Focal Point with Main Camera as Child
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Get Verticals Input
        float forwardInput = Input.GetAxis("Vertical");
        // Move Player to the forward direction
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        // Set up proper position of PowerUp indicator
        powerUpIndicator.transform.position = transform.position + new Vector3 (0, -0.3f,0);

        // Call function lunchRockets if the F button is pressed
        if (Input.GetKeyDown(KeyCode.F) && hasPowerUpRocket)
        {
            LucnhRockets();
        }

        if (Input.GetKeyDown(KeyCode.Space) && hasPowerUpSmash && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }

        ///////////////////////
        if (transform.position.y < -1)
        {
            transform.position = new Vector3(0, 0.092f, 0);
        }
        //////////////////////
    }

    // Function Lunch rocket when the player lunch rockets in to all enemies
    void LucnhRockets()
    {
        if (GameObject.FindWithTag("Enemy"))
        {
            // Put all enemies's gameObjects in to varialbe 
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            // Instantiate rockets and give all enemies from enemies variable like aim to it
            for (int i = 0; i < enemies.Length; i++)
            {
                GameObject rocketTmp = Instantiate(rocketsLunch, transform.position + Vector3.up, Quaternion.identity);
                rocketTmp.GetComponent<RocketLunch>().enemy = enemies[i];
            }
        }
    }

    // Function SmashAtack when the player hop up into the air and smash down onto the ground
    IEnumerator Smash()
    {
        // NOT MY VARIANT
        ////////////////////////////////
        
        var enemies = FindObjectsOfType<Enemy>();
        //Store the y position before taking off
        floorY = transform.position.y;
        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            //move the player up while still keeping their x velocity.
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        //Now move the player down
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }
        //Cycle through all enemies.
        for (int i = 0; i < enemies.Length; i++)
        {
            //Apply an explosion force that originates from our position.
            if (enemies[i] != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
                transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }
        //We are no longer smashing, so set the boolean to false
        smashing = false;
        // MY VARIANT
        ////////////////////////////////
        //// Player jump in air
        //playerRb.AddForce(Vector3.up * 50f,ForceMode.Impulse);
        //// If player is above 6 points by Y-axis get player down onto ground
        //if (transform.position.y > 6f)
        //{
        //    isFalling = true;
        //    // Get down player
        //    playerRb.AddForce(new Vector3(0, -150f, 0), ForceMode.Impulse);
        //    return;
        //}
        //StartCoroutine(SmashBack());
        //IEnumerator SmashBack()
        //{
        //    yield return new WaitForSeconds(0.3f);
        //    SmashAttack();
        //}


    }

    

    // Check if the Player get on PowerUp's triger
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PowerUp")
        {
            hasPowerUp = true;
            // Destroy powerUp indicator on the field 
            Destroy(other.gameObject);
            // Enable has powerUp indicator on the player
            powerUpIndicator.gameObject.SetActive(true);
            // Start timer of powerUp ability
            StartCoroutine(PowerUpCountDownRoutine());
        }

        if (other.tag == "PowerUpRocket")
        {
            hasPowerUpRocket = true;
            // Destroy powerUp indicator on the field 
            Destroy(other.gameObject);
            // Enable has powerUp indicator on the player
            powerUpIndicator.gameObject.SetActive(true);
            // Start timer of powerUp ability
            StartCoroutine(PowerUpRocketCountDownRoutine());
        }

        if (other.tag == "PowerUpSmash")
        {
            hasPowerUpSmash = true;
            // Destroy powerUp indicator on the field 
            Destroy(other.gameObject);
            // Enable has powerUp indicator on the player
            powerUpIndicator.gameObject.SetActive(true);
            // Start timer of powerUp ability
            StartCoroutine(PowerUpSmashCountDownRoutine());
        }
    }
    // Set timer to diactivate powerUp ability after 7 seconds
    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        // Diactivate powerUp
        hasPowerUp = false;
        // Diactivate powerUp indicator on the player
        if (!hasPowerUp && !hasPowerUpRocket && !hasPowerUpSmash)
        {
            powerUpIndicator.gameObject.SetActive(false);
        }

    }

    IEnumerator PowerUpRocketCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        // Diactivate powerUp
        hasPowerUpRocket = false;
        // Diactivate powerUp indicator on the player
        if (!hasPowerUp && !hasPowerUpRocket && !hasPowerUpSmash)
        {
            powerUpIndicator.gameObject.SetActive(false);
        }
    }

    IEnumerator PowerUpSmashCountDownRoutine()
    {
        yield return new WaitForSeconds(7);
        // Diactivate powerUp
        hasPowerUpSmash = false;
        // Diactivate powerUp indicator on the player
        if (!hasPowerUp && !hasPowerUpRocket && !hasPowerUpSmash)
        {
            powerUpIndicator.gameObject.SetActive(false);
        }
    }


    // Check collision with another object
    private void OnCollisionEnter(Collision collision)
    {   // Check if we collision with enemy object and has poewrUp
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp) 
        {
            // Get rigidBody of the enemy object
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb.mass > 2)
            {
                // Push away enemy from the player
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                rb.AddForce(awayFromPlayer * powerUpStrength * 3, ForceMode.Impulse);
            }
            else
            {
                // Push away enemy from the player
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                rb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }

        }

        if (isFalling && collision.gameObject.CompareTag("Ground")) 
        {
            isFalling = false; // Reset the flag

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; i++)
            {
                // Push away enemy from the player
                Vector3 awayFromPlayer = ((enemies[i].gameObject.transform.position - transform.position)).normalized;
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
    }
}
