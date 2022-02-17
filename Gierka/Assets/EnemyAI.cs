using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    public AIPath aiPath;
    private float distanceFromPlayer;//odleglosc wroga od gracza
    public float moveSpeed;
    public float stoppingDistance;//na jakiej odlegosci wrog ma sie zatrzymac
    public float nextWaypointDistance;//odleglosc od punktu nawigacyjnego
    int currentWaypoint = 0;//biezacy punkt na sciezce
    bool reachedEndOfPath = false;//czy dotarlismy do punktu koncowego
    Path path;
    Rigidbody2D rb;
    Seeker seeker;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)//upewniam sie, czy nie ma zadnych bledow
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        //animacja i rotacja wrogiego bohatera
        if (rb.velocity.x >= 0.01f)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);//opcjonalna rotacja
            transform.rotation = new Quaternion(0, 0, 0, 0);
            animator.SetFloat("moveSpeed", Mathf.Abs(1.0f));
        }
        else if (rb.velocity.x <= -0.01f)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);//opcjonalna rotacja
            transform.rotation = new Quaternion(0, 180, 0, 0);
            animator.SetFloat("moveSpeed", Mathf.Abs(1.0f));
        }
        else
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(0.0f));
        }

        //upewniam sie, czy ma sciezke
        if (path == null)
        {
            return;
        }

        //upewniam sie, czy przeciwnik dotarl do konca
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        //uzyskujemy kierunek do naszego punktu
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        distanceFromPlayer = Vector2.Distance(transform.position, target.position);
        //poruszanie sie wroga
        if (distanceFromPlayer < stoppingDistance && -distanceFromPlayer < stoppingDistance)
        {
            Vector2 force = direction * moveSpeed * Time.deltaTime;//Time.deltaTime predkosc poruszania sie powiazana z FPS-ami
            rb.AddForce(force);
        }
    }
}
