using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PictureController : MonoBehaviour
{
    private bool isMin = false;
    private bool isMax = false;
    private bool isNone = true;

    public TextMeshProUGUI InfoText;
    public AudioSource hurtSound;
    public AudioSource ElectricSound;
    public AudioSource WrongWorkSound;

    public GameObject Hearts;
    private GameObject DestroyHeart;
    private int coroutineCount = 0;
    private bool isCoroutine = false;
    public GameObject ParticleEffect;

    public void MaxLever()
    {
        Debug.Log("Max");
        isMin = false;
        isMax = true;
        isNone = false;
    }
    public void MinLever()
    {
        isMin = true;
        isMax = false;
        isNone = false;
    }
    public void NoneLever()
    {
        isMin = false;
        isMax = false;
        isNone = true;
    }

    private void Update()
    {
        if(isMin && transform.localPosition.x > -7.1f)
        {
            transform.position = new Vector3(transform.localPosition.x - 0.2f * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
        }
        else if (isMax && transform.localPosition.x < -5.0f)
        {
            transform.position = new Vector3(transform.localPosition.x + 0.2f * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);
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

    private void Start()
    {
        ParticleEffect.SetActive(false);
        Hearts.SetActive(false);
    }

    public void TrapLever()
    {
        hurtSound.Play();
        ElectricSound.Play();
        ParticleEffect.SetActive(true);
        Invoke("LoseHeart", 2.0f);
    }

    void LoseHeart()
    {
        InfoText.text = string.Format("Yor are trapped");
        Hearts.SetActive(true);
        ParticleEffect.SetActive(false);

        if (GameObject.FindGameObjectWithTag("Heart") != null)
        {
            DestroyHeart = Hearts.transform.GetChild(0).gameObject;
            isCoroutine = true;
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
