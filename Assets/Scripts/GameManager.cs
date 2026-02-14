using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private int score;
    private float time;
    private object landerRigidbody2D;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Lander.Instance.OnCoinPickUp += Lander_OnCoinPickUp;
        Lander.Instance.OnLanded += Lander_OnLanded;
    }

    private void Update()
    {
        time += Time.deltaTime;
    }
    private void Lander_OnLanded(object sender, Lander.OnLandedEventArgs e)
    {
        AddScore(e.score);
    }

    private void Lander_OnCoinPickUp(object sender, EventArgs e)
    {
        AddScore(500);
    }

    public void AddScore(int addScoreAmount)
    {
        score += addScoreAmount;
        Debug.Log(score);
    }

    public int GetScore()
    {
        return score;
    }

    public float GetTime()
    {
        return time;
    }   
}
