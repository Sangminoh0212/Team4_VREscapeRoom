using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffLightButton : MonoBehaviour
{
    private bool isPushed = false;
    private bool isEnterDoor = false;

    public AudioSource PushSound;
    public AudioSource LifhtOffSound;
    public AudioSource AttackSound;

    public GameObject OnLights;
    public GameObject OffLights;
    public GameObject Monster;
    public GameObject Player;

    private void Start()
    {
        OffLights.SetActive(false);
        Monster.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && isPushed == false)
        {
            PushSound.Play();
            transform.localScale = new Vector3(transform.localScale.x, 0.04487888f, transform.localScale.z);

            isPushed = true;
            LifhtOffSound.Play();
            Invoke("TurnOff", 2.0f);
        }
    }

    private void Update()
    {
        if(isPushed && Player.transform.position.x > -1)
        {
            if (!isEnterDoor)
            {
                isEnterDoor = true;
                AttackSound.Play();
            }                

            Invoke("MonsterDisapper",2);
        }

        if (!isEnterDoor && Player.transform.position.x > -1)
            isEnterDoor = true;
    }

    void TurnOff()
    {
        OnLights.SetActive(false);
        OffLights.SetActive(true);
        Monster.SetActive(true);
    }

    void MonsterDisapper()
    {
        Destroy(Monster);
    }
}
