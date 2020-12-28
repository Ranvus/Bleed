using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FDemonGFX : MonoBehaviour
{
    [SerializeField] AIPath aiPath;
    [SerializeField] ParticleSystem particles;

    private float timer;
    private float cooldown = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Shoot()
    {
        particles.Play();
    }
}
