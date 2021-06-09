using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchNumber : MonoBehaviour
{
    private bool isReady = true;
    public AudioSource passwordPushSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && isReady == true)
        {
            if (transform.CompareTag("isPushed"))
            {
                transform.tag = "isNotPushed";
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.098f);
            }
            else
            {
                passwordPushSound.Play();
                transform.tag = "isPushed";
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.02f);
            }
            isReady = false;
            Invoke("Locked", 2.0f);
        }
    }

    void Locked()
    {
        isReady = true;
    }

    public void initializeState()
    {
        transform.tag = "isNotPushed";
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.098f);
        isReady = false;
        Invoke("Locked", 2.0f);
    }
}