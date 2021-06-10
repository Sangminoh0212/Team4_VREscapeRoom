using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class MonsterController : MonoBehaviour
{
    private Animator animator;
    private bool isSeeUser = false;
    public GameObject Player;

    public AudioSource AttackSound;
    public AudioSource MonsterDyingSound;
    public AudioSource hurtSound;
    public AudioSource GameOverSound;
    private Collider col;

    public TextMeshProUGUI InfoText;
    public GameObject GameOverPanel;

    private void Start()
    {
        GameOverPanel.SetActive(false);
        animator = GetComponent<Animator>();
        col = GetComponent<Collider>();

    } 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Axe"))
        {
            MonsterDyingSound.Play();
            animator.SetBool("IsDying", true);
            isSeeUser = false;
            col.enabled = false;
            Destroy(gameObject, 3.0f);
        }
    }

    public void DoorSelectExited(SelectExitEventArgs args)
    {
        if (!isSeeUser)
        {
            isSeeUser = true;
            AttackSound.Play();
            animator.SetBool("IsWalking", true);
            InfoText.text = string.Format("Find Axe or Knife!");
            Invoke("ResetText", 2.0f);
        }
    }

    void ResetText()
    {
        InfoText.text = string.Format("");
    }

    private void FixedUpdate()
    {
        if (isSeeUser)
        {
            var lookPos = Player.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.fixedDeltaTime);

            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, 0.3f * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") || other.gameObject.CompareTag("MainCamera"))
        {
            hurtSound.Play();
            GameOverSound.Play();
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}