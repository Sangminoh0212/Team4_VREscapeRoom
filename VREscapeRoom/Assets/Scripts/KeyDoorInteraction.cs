using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorInteraction : MonoBehaviour
{
    public bool dooropen = false;

    // 일단은 닿기만 하면 사라지게 만듬. 나중에 닿으면 true -> 문 door operator 가능하게 만들 예정
    public void lockeddoor()
    {
        Destroy(transform.gameObject);
    }

}
