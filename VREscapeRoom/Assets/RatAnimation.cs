using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatAnimation : MonoBehaviour
{
    Animator animator;

    NavMeshAgent m_rat;

    [SerializeField] Transform[] m_tfWayPoints = null;
    int m_count = 0;

    public GameObject redKey;

    void MoveToNextWayPoint()
    {
        if(m_rat.velocity == Vector3.zero)
        {
            m_rat.SetDestination(m_tfWayPoints[m_count++].position);

            if(m_count >= m_tfWayPoints.Length)
            {
                m_count = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        m_rat = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }

    public void Catched()
    {
        animator.SetBool("isCatched", true);
        CancelInvoke("MoveTiNextWayPoint");
    }

    public void Run()
    {
        animator.SetBool("isCatched", false);
        InvokeRepeating("MoveToNextWayPoint", 0f, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife")) {
            Instantiate(redKey, transform.position+Vector3.up, transform.rotation);
            Destroy(gameObject);
        }

    }
}
