using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combinekey : MonoBehaviour
{
    public GameObject realKey;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(realKey, transform.position, transform.rotation);
        }
    }
}
