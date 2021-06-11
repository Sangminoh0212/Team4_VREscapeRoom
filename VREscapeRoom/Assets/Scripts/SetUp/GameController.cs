using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI Timer1;
    public TextMeshProUGUI Timer2;
    public GameObject GameoverPanel;

    private float timeRemaining = 600.0f;
    private bool timerIsRunning = false;

    public AudioSource GameoverSound;
    public AudioSource bgmSound;

    private void Start()
    {
        GameoverPanel.SetActive(false);
        timerIsRunning = true;
        bgmSound.Play();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Time.timeScale = 0;
                GameoverPanel.SetActive(true);
                bgmSound.Stop();
                GameoverSound.Play();
            }
        }

        if(timerIsRunning && GameObject.FindGameObjectWithTag("Life") == null)
        {
            timerIsRunning = false;
            Time.timeScale = 0;
            GameoverPanel.SetActive(true);
            bgmSound.Stop();
            GameoverSound.Play();
        }
    }

    void DisplayTime(float timeToDiplay)
    {
        timeToDiplay += 1;

        float minutes = Mathf.FloorToInt(timeToDiplay / 60);
        float seconds = Mathf.FloorToInt(timeToDiplay % 60);
        float milliSeconds = (timeToDiplay % 1) * 1000;

        Timer1.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        Timer2.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
