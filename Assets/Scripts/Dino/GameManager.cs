using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float speedWorld = 0f;
    float increaseSpeed = 0.1f;
    public bool startGame = false;
    public bool death = false;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI hiscoreText;
    float score = 0;
    [SerializeField] GameObject gameOverPanel;

    [Header("Sounds")]
    [SerializeField] AudioClip point;
    public AudioClip jump;
    [SerializeField] AudioClip die;
    public AudioSource audioSource;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if(startGame && !death) speedWorld += increaseSpeed * Time.deltaTime;
    }

    public void GameOver()
    {
        PlaySound(die);
        death = true;
        speedWorld = 0;
        gameOverPanel.SetActive(true);
        UpdateHighScore();
    }

    public void RestartGame()
    {
        Obstacles[] obstacles = FindObjectsOfType<Obstacles>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        death = false;
        startGame = true;
        speedWorld = 5f;
        gameOverPanel.SetActive(false);
        score = 0;
    }
    void UpdateUI()
    {
        score += speedWorld * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
        int intScore = Mathf.FloorToInt(score);
        if(intScore != 0)
        {
            if (intScore % 100 == 0 && intScore <= 1000) PlaySound(point);
            else if(intScore % 500 == 0) PlaySound(point);
        }
    }
    void UpdateHighScore()
    {
        float highScore = PlayerPrefs.GetFloat("hiscore", 0);
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("hiscore", highScore);
        }
        hiscoreText.text = Mathf.FloorToInt(highScore).ToString("D5");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
