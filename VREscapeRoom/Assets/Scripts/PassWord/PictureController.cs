using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureController : MonoBehaviour
{
    private bool isMin = false;
    private bool isMax = false;
    private bool isNone = true;

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
    }

}
