using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text MyScoreText;
    public Text MyScoreText2;
    private int ScoreNumber;
    private int MoveSpeed;
    public int NeedScore;
    void Start()
    {
        ScoreNumber = 0;
        MoveSpeed = 200;
        MyScoreText.text = "Score: " + ScoreNumber + "/" + NeedScore;
        MyScoreText2.text = "Speed: " + MoveSpeed + " ";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Points")
        {
            ScoreNumber += 1;
            Debug.Log("Zebrano punkt!");
            Destroy(collision.gameObject);
            MyScoreText.text = "Punkty: " + ScoreNumber + "/" + NeedScore;
        }
        if (collision.tag == "Heart")
        {
            MoveSpeed += 20;
            Debug.Log("Dodano 20 jednostek predkosci");
            Destroy(collision.gameObject);
            MyScoreText2.text = "Speed: " + MoveSpeed + " ";
        }
    }
}
