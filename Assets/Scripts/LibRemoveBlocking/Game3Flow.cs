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
    bool gameStart = false;
    bool gameEnd = false;

    public void Game03Start() {
        gameStart = true;
        SetStartingTime();
    }

    void OnGameEnd() {
        gameEnd = true;
    }
    void GameOver() {
        OnGameEnd();
        mainGame.GameOver();
    }

    void GameWin() {
        OnGameEnd();
        mainGame.GameWin();
    }
    public void OnEventStartNotBlocking() {
        GameWin();
    }

    void Start()
    {
        // TODO: better design
        updateTimerText(MaxGameTime);
    }

    // Update is called once per frame
    void Update()
    {
        if ((!gameStart) || gameEnd)
            return;
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            GameOver();
        Debug.Log(gameEnd);
        updateTimerText(remaintime);
        UpdateSocre(remaintime);
    }

    void UpdateSocre(int remaintime) {
        int score = (int)Mathf.Ceil(remaintime/(float)(MaxGameTime - StartingTime)*100);
        if (score < 0)
            score = 0;
        ScoreText.text = "Score: " + score.ToString();
    }

    void SetStartingTime() {
        StartingTime = (int) Mathf.Ceil(Time.time);
        MaxGameTime += StartingTime;
    }

    void updateTimerText(int t) {
        TimerText.text = "Time: " + t.ToString();
    }
}
