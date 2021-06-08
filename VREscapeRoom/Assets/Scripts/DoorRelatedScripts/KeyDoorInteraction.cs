using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyDoorInteraction : MonoBehaviour
{
    public GameObject handle;

    public void Start()
    {

    }

    // 일단은 닿기만 하면 사라지게 만듬. 나중에 닿으면 true -> 문 door operator 가능하게 만들 예정

    public void SelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Key!!");
        handle.SetActive(true);

    }

}
