using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MonsterController : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
}