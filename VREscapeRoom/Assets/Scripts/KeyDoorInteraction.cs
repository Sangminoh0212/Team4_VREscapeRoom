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

    // �ϴ��� ��⸸ �ϸ� ������� ����. ���߿� ������ true -> �� door operator �����ϰ� ���� ����

    public void SelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Key!!");
        handle.SetActive(true);

    }

}
