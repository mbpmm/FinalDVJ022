using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public bool isRaining;
    public float chanceOfRain;
    private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        isRaining = (Random.Range(0f, 1f)) <= chanceOfRain;

        if (isRaining)
        {
            particles.Play();
        }
        else
        {
            particles.Stop();
        }
    }
}
