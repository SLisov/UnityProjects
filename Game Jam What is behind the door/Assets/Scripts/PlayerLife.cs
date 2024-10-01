using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] AudioClip deathClip;
    Animator animator;
    AudioSource audioSource;
    PlayerMovement playerMovement;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DangerObject"))
        {
            Death();
        }

    }

    private void Death()
    { 
        animator.SetTrigger("death");
        audioSource.PlayOneShot(deathClip);
        playerMovement.isDeath = true;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
