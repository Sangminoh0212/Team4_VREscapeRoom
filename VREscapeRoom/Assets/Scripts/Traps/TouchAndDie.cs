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

    public GameObject Hearts;
    private GameObject DestroyHeart;

    private void Start()
    {
        Hearts.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("MainCamera"))
        {
            electricSound.Play();
            transform.GetChild(0).gameObject.SetActive(true);
            InfoText.text = string.Format("Yor are trapped");
            Invoke("LoseHeart", 2.0f);
        }
    }

    void LoseHeart()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        InfoText.text = string.Format("");
        if (GameObject.FindGameObjectWithTag("Heart") != null)
        {
            DestroyHeart = Hearts.gameObject.transform.GetChild(0).gameObject;
            StartCoroutine("flicker");
        }
    }

    IEnumerator flicker()
    {
        while (true)
        {
            if(Hearts.activeInHierarchy)
            {
                Hearts.SetActive(false);
            }
            else
                Hearts.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
