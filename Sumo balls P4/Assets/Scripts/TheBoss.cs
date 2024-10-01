using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBoss : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody theBossRb;
    private GameObject player;
    public GameObject minions;


    void Start()
    {
        theBossRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        StartCoroutine(GenerateMinions());
    }


    void Update()
    {
        

        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        theBossRb.AddForce(lookDirection * speed * 2);

        if (transform.position.y < -3 || transform.position.y > 3)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateMinions()
    {
        yield return new WaitForSeconds(4);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(minions, (player.transform.position - transform.position / 2), Quaternion.identity);
        }
        StartCoroutine(GenerateMinions());
    }
}
