using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyDoorInteraction : MonoBehaviour
{
    public GameObject handle;

    public void SelectEntered(SelectEnterEventArgs args)
    {
        handle.SetActive(true);
    }
}
