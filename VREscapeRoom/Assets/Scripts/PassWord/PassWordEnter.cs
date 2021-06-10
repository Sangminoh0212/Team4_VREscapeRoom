using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class PassWordEnter : MonoBehaviour
{
    private int EnteredNumberCount = 0;

    public TextMeshProUGUI InfoText;
    public GameObject GameClearPanel;
    public AudioSource wrongPasswordSound;
    public AudioSource hurtSound;

    public AudioSource bgmSound;
    public AudioSource GameClearSound;

    public GameObject Hearts;
    private GameObject DestroyHeart;
    private int coroutineCount = 0;
    private bool isCoroutine = false;

    public UnityEvent OnPasswordEntered;

    private void Start()
    {
        GameClearPanel.SetActive(false);
    }
    void Update()
    {
        if (!isCoroutine)
        {
            EnteredNumberCount = 0;

            for (int i = 1; i < 10; i++)
            {
                if (transform.Find(i.ToString()).CompareTag("isPushed"))
                {
                    EnteredNumberCount++;
                }
            }

            if (EnteredNumberCount >= 4)
            {
                if (transform.Find(3.ToString()).CompareTag("isPushed") && transform.Find(5.ToString()).CompareTag("isPushed") && transform.Find(7.ToString()).CompareTag("isPushed") && transform.Find(9.ToString()).CompareTag("isPushed"))
                {
                    InfoText.text = string.Format("You open the door");
                    OnPasswordEntered.Invoke();
                    Invoke("GameClear", 2.0f);
                }
                else
                {
                    wrongPasswordSound.Play();
                    hurtSound.Play();
                    InfoText.text = string.Format("Password is wrong");
                    isCoroutine = true;
                    OnPasswordEntered.Invoke();
                    Invoke("LoseHeart", 1.0f);
                }
            }
        }

        if (isCoroutine && coroutineCount == 4)
        {
            StopCoroutine("flicker");
            isCoroutine = false;
            coroutineCount = 0;

            InfoText.text = string.Format("");
            Hearts.SetActive(true);
            if (GameObject.FindGameObjectWithTag("Heart") == null)
            {
                GameObject life = GameObject.FindGameObjectWithTag("Life");
                Destroy(life);
            }
            Hearts.SetActive(false);
        }
    }

    void GameClear()
    {
        InfoText.text = string.Format("");
        GameClearPanel.SetActive(true);
        bgmSound.Stop();
        GameClearSound.Play();
        InfoText.text = string.Format("Game clear");
        Time.timeScale = 0;
    }

    void LoseHeart()
    {
        Hearts.SetActive(true);

        if (GameObject.FindGameObjectWithTag("Heart") != null)
        {
            DestroyHeart = Hearts.transform.GetChild(0).gameObject;
            StartCoroutine("flicker");
        }
    }

    IEnumerator flicker()
    {
        while (true)
        {
            if (Hearts.activeInHierarchy && coroutineCount == 3)
            {
                Destroy(DestroyHeart.gameObject);
                Hearts.SetActive(false);
                coroutineCount += 1;
            }
            else if (DestroyHeart.activeInHierarchy)
            {
                coroutineCount += 1;

                DestroyHeart.SetActive(false);
            }
            else
            {
                DestroyHeart.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
}