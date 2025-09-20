using System.Collections;
using UnityEngine;

public class ParticlesDestroy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyParticles());
    }

    IEnumerator DestroyParticles()
    {
        while (GetComponent<ParticleSystem>().isPlaying)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
