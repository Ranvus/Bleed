using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;

    private float timer;
    private float cooldown = 3f;

    void Start()
    {
        
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Shoot();
            timer = cooldown;
        }
    }

    private void Shoot()
    {
        particles.Play();
    }
}
