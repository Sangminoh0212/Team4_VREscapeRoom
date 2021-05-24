using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchAndDie : MonoBehaviour
{
    public TextMeshProUGUI InfoText;
    public AudioSource electricSound;
    public AudioSource gameOverSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            electricSound.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            InfoText.text = string.Format("Yor are trapped");
            Invoke("GameOverMessage", 2.0f);
        }
            else if (other.gameObject.CompareTag("MainCamera"))
            {
            electricSound.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            InfoText.text = string.Format("Yor are trapped");
            Invoke("GameOverMessage", 2.0f);
        }
    }

    void GameOverMessage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        gameOverSound.Play();
        InfoText.text = string.Format("Game Over");
        Time.timeScale = 0;
    }
}
