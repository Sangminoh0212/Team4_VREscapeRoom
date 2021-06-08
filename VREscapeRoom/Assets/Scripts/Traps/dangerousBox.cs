using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerousBox : MonoBehaviour
{
    [SerializeField] private ParticleSystem gas;

    public void Start()
    {
        gas.Stop();
    }

    public void Gas()
    {
        gas.Play();
        Invoke("Stop", 3);
    }

    private void Stop()
    {
        gas.Stop();
        Destroy(gameObject);
    }
}
