using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FDemonController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private Transform fdemonGFX;
    [SerializeField] private ParticleSystem particles;

    [SerializeField] private AudioSource growl;

    private float flySpeed = 400f;
    private float nextWaypointDistance = 1f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private float timer;
    private float cooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void Update()
    {
        if (timer > 0 && path.GetTotalLength() < 2f)
        {
            timer -= Time.deltaTime;
        }
        else 
        {
            Shoot();
            timer = cooldown;
        }
        //growl.Play();
    }

    private void Shoot()
    {
        particles.Play();
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * flySpeed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x > 0.05f)
        {
            fdemonGFX.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (rb.velocity.x < -0.05f)
        {
            fdemonGFX.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
