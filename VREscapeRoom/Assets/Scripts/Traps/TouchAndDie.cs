using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TouchAndDie : MonoBehaviour
{
    public TextMeshProUGUI InfoText;
    public AudioSource electricSound;
    public AudioSource hurtSound;

    public GameObject Hearts;
    private GameObject DestroyHeart;
    private int coroutineCount = 0;
    private bool isCoroutine = false;

    private void Start()
    {
        Hearts.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("MainCamera"))
        {
            electricSound.Play();
            hurtSound.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("LoseHeart", 2.0f);
        }
    }

    void LoseHeart()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        InfoText.text = string.Format("Yor are trapped");
        Hearts.SetActive(true);

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

    private void Update()
    {
        if(isCoroutine && coroutineCount == 4)
        {
            StopCoroutine("flicker");
            isCoroutine = false;
            coroutineCount = 0;

            InfoText.text = string.Format("");
        }
    }
}
