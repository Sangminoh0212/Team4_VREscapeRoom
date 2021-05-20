using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassWordEnter : MonoBehaviour
{
    private bool[] isPushed;
    private int count = 0;
    private bool isGameover = false;

    public TextMeshProUGUI InfoText;
    public AudioSource gameOverSound;
    public AudioSource wrongPasswordSound;


    void Update()
    {
        count = 0;

        for (int i = 1; i < 10; i++)
        {
            if (transform.Find(i.ToString()).CompareTag("isPushed"))
            {
                count++;
            }
        }

        if (count >= 4 && !isGameover)
        {
            isGameover = true;
            if (transform.Find(1.ToString()).CompareTag("isPushed") && transform.Find(2.ToString()).CompareTag("isPushed") && transform.Find(3.ToString()).CompareTag("isPushed") && transform.Find(4.ToString()).CompareTag("isPushed"))
            {
                InfoText.text = string.Format("You open the door");
                Invoke("GameClear", 2.0f);
            }
            else
            {
                wrongPasswordSound.Play();
                InfoText.text = string.Format("Password is wrong");
                Invoke("GameOverMessage", 2.0f);
            }
        }
    }

    void GameOverMessage()
    {
        gameOverSound.Play();
        InfoText.text = string.Format("Game Over");
        Time.timeScale = 0;
    }

    void GameClear()
    {
        InfoText.text = string.Format("Game clear");
        Time.timeScale = 0;
    }
}

