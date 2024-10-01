using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public int lives;
    public bool isGameActive;
    public bool abbleToCut = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI Pause;
    public Slider volumeSlider;
    public AudioSource audioSource;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseBackground;
    private TrailRenderer trail;

    bool onPause = false;

    void Start()
    {
        volumeSlider.value = audioSource.volume;
        trail = GameObject.Find("Trail").GetComponent<TrailRenderer>();
        trail.emitting = false;
    }


    void Update()
    {
        audioSource.volume = volumeSlider.value;
        mouseListener();
        OnPause();
    }

    private void mouseListener()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;  // Adjust this value based on your setup
        trail.transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            StartTrail();
            abbleToCut = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopTrail();
            abbleToCut = false;
        }
    }

    private void StartTrail()
    {
        trail.emitting = true;  // Start emitting the trail
    }

    private void StopTrail()
    {
        trail.emitting = false;  // Stop emitting the trail
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score:" + score;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives = 3;
        spawnRate /= difficulty;
        scoreText.text = "Score:" + score;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }

    void OnPause()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!onPause && isGameActive)
            {
                Time.timeScale = 0;
                pauseBackground.gameObject.SetActive(true);
                Pause.gameObject.SetActive(true);

                onPause = true;
                isGameActive = false;
            }
            else if (onPause && !isGameActive)
            {
                Time.timeScale = 1.0f;
                pauseBackground.gameObject.SetActive(false);
                Pause.gameObject.SetActive(false);
                onPause = false;
                isGameActive = true;
            }
        }
    }
}
