using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int lives;
    private float gameTime;
    public AudioClip musicClip;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public GameObject gameOverpanel;
    private AudioSource gameMusic;
    void Start()
    {
        lives = 1;
        gameMusic = GetComponent<AudioSource>();
        gameOverpanel.gameObject.SetActive(false);
    }

    void Update()
    {
        GameGUI();
    }
    private void GameGUI()
    {
        livesText.text = "Lives: " + lives.ToString("F0");
        gameTime += Time.deltaTime;
        timeText.text = "Time: " + gameTime.ToString("F2");
    }
    public void MusicSystem()
    {
        gameMusic.volume = 0.1f;
        gameMusic.PlayOneShot(musicClip);
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverpanel.gameObject.SetActive(true);
    }
}
