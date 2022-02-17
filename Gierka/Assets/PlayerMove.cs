using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed;
    public float points;//ile punktow potrzeba do zebrania kluczyka
    private float pointsCollected = 0;//licznik punktow zebranych
    private float gravel = 0;//dodatkowy licznik zebranego zwiru
    bool key = false;
    bool chest = false;
    public string currentLevel;//ustaw obecny poziom
    public string nextLevel;//ustaw nastepny poziom

    void Update()
    {
        //animacje bohatera
        if (Input.GetAxis("Horizontal") != 0)
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(Input.GetAxis("Horizontal")));
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(Input.GetAxis("Vertical")));
        }

        //rotacja bohatera
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }

        //poruszanie sie bohatera
        transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime / 100, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime / 100, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Points"))
        {
            SoundManager.PlaySound("getPoint_m1");
            print("Collecting the point");
            Destroy(collision.gameObject);
            pointsCollected++;
        }
        if(collision.CompareTag("Heart"))
        {
            SoundManager.PlaySound("getPoint_m1");
            print("Collecting the heart");
            Destroy(collision.gameObject);
            moveSpeed = moveSpeed + 20;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Key")
        {
            if (pointsCollected >= points)
            {
                SoundManager.PlaySound("getKey_m1");
                print("Collecting the key");
                Destroy(collision.gameObject);
                key = true;
            }
        }

        if(collision.gameObject.tag == "Chest")
        {
            if (key == true)
            {
                SoundManager.PlaySound("openChest_m1");
                print("You passed the level");
                chest = true;
                SceneManager.LoadScene(nextLevel);
            }
        }

        if (collision.gameObject.tag == "Gravel")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("You dug the gravel");
            Destroy(collision.gameObject);
            gravel++;
        }

        if (collision.gameObject.tag == "Enemies")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("Restart level!");
            SceneManager.LoadScene(currentLevel);
        }

        if (collision.gameObject.tag == "FastEnemies")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("Restart level.");
            SceneManager.LoadScene(currentLevel);
        }

        if (collision.gameObject.tag == "Holes")
        {
            SoundManager.PlaySound("breakGravel_m1");
            print("Restart level");
            SceneManager.LoadScene(currentLevel);
        }
    }
}
