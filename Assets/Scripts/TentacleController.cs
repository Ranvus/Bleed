using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{
    const string LEFT = "left";
    const string RIGHT = "right";

    [SerializeField] private Transform castPos;

    private float baseCastDistance = .1f;

    private Rigidbody2D rb;
    private float moveSpeed = 2f;

    private string facingDirection;

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale;
        facingDirection = RIGHT;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }

    private void FixedUpdate() 
    {
        float vX = moveSpeed;

        if (facingDirection == LEFT)
        {
            vX = -moveSpeed;
        }
        //Движение объекта
        rb.velocity = new Vector2(vX, rb.velocity.y);

        if (IsHittingWall() || IsNearEdge())
        {
            if (facingDirection == LEFT)
            {
                ChangeFacingDirection(RIGHT);
            }
            else
            {
                ChangeFacingDirection(LEFT);
            }
        }
    }

    private void ChangeFacingDirection(string newDirection)
    {
        Vector3 newScale = baseScale;

        if (newDirection == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }

        transform.localScale = newScale;

        facingDirection = newDirection;
    }

    private bool IsHittingWall()
    {
        bool val = false;

        float castDistance = baseCastDistance;

        //Определение расстояния до препятствия
        if (facingDirection == LEFT)
        {
            castDistance = -baseCastDistance;
        }
        else
        {
            castDistance = baseCastDistance;
        }

        //Определение конца препятсвия 
        Vector3 targetPos = castPos.position;
        targetPos.x += castDistance;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }

    private bool IsNearEdge()
    {
        bool val = true;

        float castDistance = baseCastDistance;

        //Определение конца препятсвия 
        Vector3 targetPos = castPos.position;
        targetPos.y -= castDistance;

        Debug.DrawLine(castPos.position, targetPos, Color.red);

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
}