using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 14;
    private float maxSpeed = 18;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int pointValue;

    public ParticleSystem explosionParticle;
    private LineRenderer lineRenderer;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(TorqueForce(), TorqueForce(), TorqueForce(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

    }

    void Update()
    {
        
    }

    private void OnMouseExit()
    {
        if (gameManager.isGameActive && gameManager.abbleToCut)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            gameManager.UpdateScore(pointValue);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float TorqueForce()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.isGameActive) 
        { 
            Destroy(gameObject);

            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.lives -= 1;
                gameManager.livesText.text = "Lives: " + gameManager.lives;

                if (gameManager.lives <= 0)
                {
                    gameManager.GameOver();
                }
            }
        }
    }
}
