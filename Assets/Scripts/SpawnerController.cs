using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private AudioSource sound;

    private float timer;
    private float cooldown = 3f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            anim.SetBool("isShooting", false);
        }
        else
        {
            sound.Play();
            anim.SetBool("isShooting", true);
            Shoot();
            timer = cooldown;
        }
    }

    private void Shoot()
    {
        particles.Play();
    }
}
