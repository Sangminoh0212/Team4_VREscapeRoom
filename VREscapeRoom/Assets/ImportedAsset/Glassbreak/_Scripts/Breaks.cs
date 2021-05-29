using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaks : MonoBehaviour
{
    public Transform brokenObject;

    public void breakWindow()
    {
        Destroy(gameObject);
        Instantiate(brokenObject, transform.position, transform.rotation);
        brokenObject.localScale = transform .localScale;
    }
}
