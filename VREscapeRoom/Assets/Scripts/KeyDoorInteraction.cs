using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorInteraction : MonoBehaviour
{
    public bool dooropen = false;

    // �ϴ��� ��⸸ �ϸ� ������� ����. ���߿� ������ true -> �� door operator �����ϰ� ���� ����
    public void lockeddoor()
    {
        Destroy(transform.gameObject);
    }

}
