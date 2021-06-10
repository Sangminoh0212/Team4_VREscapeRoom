using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dangerousBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem gas;

    public TextMeshProUGUI InfoText;
    public AudioSource hurtSound;

    public GameObject Hearts;
    private GameObject DestroyHeart;
    private int coroutineCount = 0;
    private bool isCoroutine = false;

    public void Start()
    {
        gas.Stop();
        Hearts.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("MainCamera"))
        {
            gas.Play();
            hurtSound.Play();
            
            Invoke("LoseHeart", 2.0f);
        }
    }

    void LoseHeart()
    {
        gas.Stop();
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
}
