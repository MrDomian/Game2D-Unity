using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyAI : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed;
    public float stoppingDistance;//na jakiej odlegosci wrog ma sie zatrzymac
    float distanceFromPlayer;//odleglosc wroga od gracza
    float distancePlayerX;//odleglosc wroga od gracza na osi X
    private Transform target;
    Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, target.position);
        distancePlayerX =  target.position.x - rb.position.x;

        //animacja i rotacja wrogiego bohatera
        if (distancePlayerX >= 0.5f)
        {
            if (distancePlayerX > stoppingDistance)
            {
                animator.SetFloat("moveSpeed", Mathf.Abs(0.0f));
            }
            else
            {
                //transform.localScale = new Vector3(1f, 1f, 1f);
                transform.rotation = new Quaternion(0, 0, 0, 0);
                animator.SetFloat("moveSpeed", Mathf.Abs(1.0f));
            }
        }
        else if (distancePlayerX <= -0.5f)
        {
            if (-distancePlayerX > stoppingDistance)
            {
                animator.SetFloat("moveSpeed", Mathf.Abs(0.0f));
            }
            else
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
                transform.rotation = new Quaternion(0, 180, 0, 0);
                animator.SetFloat("moveSpeed", Mathf.Abs(1.0f));
            }
        }

        if (distanceFromPlayer < stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime / 100);
        }
    }
}
