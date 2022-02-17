using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    public float walkSpeed;//asd
    public bool mustPatrolX;//obiekt porusza sie po osi X
    public bool mustPatrolY;//obiekt porusza sie po osi Y
    public bool mustPatrolAround;//obiekt przeciwnie do ruchu wskazowek zegara
    public bool mustPatrolAround2;//obiekt zgodnie z ruchem wskazowek zegara
    bool mustPatrolAroundX;
    bool mustPatrolAroundY;
    bool mustPatrolAroundX2;
    bool mustPatrolAroundY2;
    bool mustTurnX;
    bool mustTurnY;
    bool mustTurnAroundX;
    bool mustTurnAroundY;
    public Rigidbody2D rb;
    public Transform GroundCheckPositionX;//opcja dla mustPatrolX
    public Transform GroundCheckPositionXX;//opcja dla mustPatrolAround i mustPatrolAround2
    public Transform GroundCheckPositionY;//opcja dla mustPatrolY
    public Transform GroundCheckPositionYY;//opcja dla mustPatrolAround i mustPatrolAround2
    public LayerMask groundLayer;

    void Start()
    {
        if (mustPatrolAround)
        {
            mustPatrolAroundX = true;
            mustPatrolAroundY = false;
        }
        if (mustPatrolAround2)
        {
            mustPatrolAroundX2 = false;
            mustPatrolAroundY2 = true;
        }
    }

    void Update()
    {
        if (mustPatrolX)
        {
            PatrolX();
        }
        if (mustPatrolY)
        {
            PatrolY();
        }
        if (mustPatrolAround)
        {
            if (mustPatrolAroundX)
            {
                PatrolAroundX();
            }
            if (mustPatrolAroundY)
            {
                PatrolAroundY();
            }
        }
        if (mustPatrolAround2)
        {
            if (mustPatrolAroundX2)
            {
                PatrolAroundX2();
            }
            if (mustPatrolAroundY2)
            {
                PatrolAroundY2();
            }
        }
    }

    private void FixedUpdate()
    {
        if(mustPatrolX)
        {
            mustTurnX = Physics2D.OverlapCircle(GroundCheckPositionX.position, 0.1f, groundLayer);
        }
        if (mustPatrolY)
        {
            mustTurnY = Physics2D.OverlapCircle(GroundCheckPositionY.position, 0.1f, groundLayer);
        }
        if (mustPatrolAroundX || mustPatrolAroundX2)
        {
            mustTurnAroundX = Physics2D.OverlapCircle(GroundCheckPositionYY.position, 0.1f, groundLayer);
        }
        if (mustPatrolAroundY || mustPatrolAroundY2)
        {
            mustTurnAroundY = Physics2D.OverlapCircle(GroundCheckPositionXX.position, 0.1f, groundLayer);
        }
    }

    void PatrolX()
    {
        if (mustTurnX)
        {
            FlipX();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void PatrolY()
    {
        if (mustTurnY)
        {
            FlipY();
        }
        rb.velocity = new Vector2(rb.velocity.x, walkSpeed * Time.fixedDeltaTime);
    }

    void PatrolAroundX()
    {
        if (mustTurnAroundX)
        {
            FlipAroundX();
        }
        rb.velocity = new Vector2(0, walkSpeed * Time.fixedDeltaTime);
    }

    void PatrolAroundY()
    {
        if (mustTurnAroundY)
        {
            FlipAroundY();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);
    }

    void PatrolAroundX2()
    {
        if (mustTurnAroundX)
        {
            FlipAroundX2();
        }
        rb.velocity = new Vector2(0, walkSpeed * Time.fixedDeltaTime);
    }

    void PatrolAroundY2()
    {
        if (mustTurnAroundY)
        {
            FlipAroundY2();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0);
    }

    void FlipX()
    {
        mustPatrolX = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrolX = true;
    }

    void FlipY()
    {
        mustPatrolY = false;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
        walkSpeed *= -1;
        mustPatrolY = true;
    }

    void FlipAroundX()
    {
        mustPatrolAroundX = false;
        mustTurnAroundX = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrolAroundY = true;
    }

    void FlipAroundY()
    {
        mustPatrolAroundY = false;
        mustTurnAroundY = false;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
        walkSpeed *= 1;
        mustPatrolAroundX = true; 
    }

    void FlipAroundX2()
    {
        mustPatrolAroundX2 = false;
        mustTurnAroundX = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= 1;
        mustPatrolAroundY2 = true;
    }

    void FlipAroundY2()
    {
        mustPatrolAroundY2 = false;
        mustTurnAroundY = false;
        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
        walkSpeed *= -1;
        mustPatrolAroundX2 = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemies")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("Enemy fell into the black hole");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "FastEnemies")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("Fast enemy fell into the black hole");
            Destroy(collision.gameObject);
        }
    }
}
