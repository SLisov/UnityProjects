using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    private AudioSource m_AudioSource;
    private bool m_levelCompleted;
    private void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && !m_levelCompleted)
        {
            m_AudioSource.Play();
            m_levelCompleted = true;
            Invoke("CompletedLevel", 2f);
            
        }
    }

    //private void CompletedLevel()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
