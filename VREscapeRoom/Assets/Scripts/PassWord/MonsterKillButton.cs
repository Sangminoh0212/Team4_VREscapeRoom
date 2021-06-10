using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MonsterKillButton : MonoBehaviour
{
    private bool isPushed = false;
    public AudioSource PushSound;

    public AudioSource AttackSound;
    public AudioSource MonsterDyingSound;
    public AudioSource FlameSound;

    public GameObject Flame;
    public GameObject PasswordHint;
    public GameObject MonsterObject;

    private bool isCoroutine = false;

    private void Start()
    {
        Flame.SetActive(false);
        PasswordHint.SetActive(false);
    }

    public void DoorSelectExit(SelectExitEventArgs args)
    {
        if (!isCoroutine)
        {
            StartCoroutine("Attacker");
            isCoroutine = true;
        }
    }

    IEnumerator Attacker()
    {
        while (true)
        {
            AttackSound.Play();
            yield return new WaitForSeconds(5.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && isPushed == false)
        {
            PushSound.Play();
            StopCoroutine("Attacker");
            transform.localScale = new Vector3(transform.localScale.x, 0.04487888f, transform.localScale.z);

            isPushed = true;

            FlameSound.Play();
            Flame.SetActive(true);
            Destroy(MonsterObject);
            MonsterDyingSound.Play();

            Invoke("TurnOff", 2.0f);
        }
    }

    public void TurnOff()
    {
        Flame.SetActive(false);
        PasswordHint.SetActive(true);
    }
}