using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Animator anim;
    public ParticleSystem bloodAmount;

    [Header("Horizontal movement variables")]
    protected Vector2 dir;
    private float moveSpeed = 7f;
    private bool facingRight = true;

    public ParticleSystem dust;
    private bool wasOnGround;

    [Header("Jump variables")]
    private float jumpForce = 7f;
    private bool jumpRequest;
    private float jumpBufferLength = .1f;
    [SerializeField] private float jumpBufferCount;
    private float hangTime = 0.1f;
    [SerializeField] private float hangCounter;

    [Header("Gravity variables")]
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 4f;

    [Header("Ground check variables")]
    [SerializeField] private LayerMask ground;
    [SerializeField] private bool isGrounded;

    [Header("Animation variables")]
    private bool isRunning;

    [Header("Blood variables")]
    public float maxBlood;
    public float currentBlood;
    public BloodBarController bloodBar;

    //Функция, выполняющаяся при "включении" объекта
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        maxBlood = bloodAmount.main.duration;
        currentBlood = maxBlood;
        bloodBar.SetMaxBlood(maxBlood);
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapBox(new Vector2(transform.position.x + 0.01f, transform.position.y - 0.5f), new Vector2(0.39f, 0.01f), 0f, ground);

        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");

        dir = new Vector2(xRaw, yRaw);

        //Coyote jump
        hangCounter -= Time.deltaTime;
        if (isGrounded)
        {
            hangCounter = hangTime;
        }

        //Jump buffer
        jumpBufferCount -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = jumpBufferLength;
        }

        //Jump
        if (jumpBufferCount > 0 && hangCounter > 0)
        {
            hangCounter = 0;
            jumpRequest = true;
            CreateDust();
        }

        //Animation
        if (dir.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        UpdateAnimations();

        //Dust effect before jump
        if (!wasOnGround && isGrounded)
        {
            CreateDust();
        }

        wasOnGround = isGrounded;

        //Bleeding
        currentBlood = maxBlood - bloodAmount.time;
        bloodBar.SetBlood(currentBlood);
    }

    private void FixedUpdate()
    {
        Run(dir);

        if (jumpRequest)
        {
            Jump();
            jumpRequest = false;
        }

        //Gravity add
        if (!isGrounded)
        {
            if (rb.velocity.y < 0)
            {
                rb.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime, ForceMode2D.Impulse);
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && !isGrounded)
            {
                rb.AddForce(Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
    }

    private void Run(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
        if ((dir.x > 0 && !facingRight) || (dir.x < 0 && facingRight))
        {
            Flip();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("vSpeed", rb.velocity.y);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        /*Первое значение - вектор, указывает центр квадрата. Для того чтобы нарисовать куб ниже игрока 
        стоит отнять от значения Y сумму 0,5 и половины размера квадрата. Второе значение - вектор - размер*/
        Gizmos.DrawCube(new Vector2(transform.position.x + 0.01f, transform.position.y - 0.5f),
            new Vector2(0.39f, 0.01f));
    }

    void CreateDust()
    {
        dust.Play();
    }
}
