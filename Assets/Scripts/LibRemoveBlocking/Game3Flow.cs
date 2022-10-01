using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Flow : MonoBehaviour
{
    public Text TimerText;
    public Text ScoreText;
    public int MaxGameTime=60;
    public int StartingTime;
    public MainGameController mainGame;

    public void Game03Start() {
        SetStartingTime();
    }

    void GameOver() {
        mainGame.GameOver();
    }

    void GameWin() {
        mainGame.GameWin();
    }
    public void OnEventStartNotBlocking() {
        Debug.Log("Game Win");
        GameWin();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowTime();
        UpdateSocre();
    }

    void UpdateSocre() {
        int score = (MaxGameTime - StartingTime);
        if (score < 0)
            score = 0;
        Debug.Log(score);
        ScoreText.text = "Score: " + score.ToString();
    }

    void SetStartingTime() {
        StartingTime = (int) Mathf.Ceil(Time.time);
        MaxGameTime += StartingTime;
    }

    void ShowTime() {
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            GameOver();
        TimerText.text = "Time: " + remaintime.ToString();
    }
}
