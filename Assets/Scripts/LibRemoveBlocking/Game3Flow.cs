using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Flow : MonoBehaviour
{
    public Text TimerText;
    public Text ScoreText;
    public int MaxGameTime=20;
    public int StartingTime;
    //public MainGameController mainGame;
    int GameEndTime = 0;
    bool gameStart = false;
    bool gameEnd = false;

    public void Game03Start() {
        gameStart = true;
        SetStartingTime();
    }
    /*我是用重新載入一次原場景達成restart效果. 同時也再次開啟說明面板*/
    /*這個void我就不使用他了*/
    public void Game03Restart() {
        gameEnd = false;
        Game03Start();
    }

    void OnGameEnd() {
        gameEnd = true;
    }
    void GameOver() {
        OnGameEnd();
        MainGameController.GameOver();
    }

    void GameWin() {
        OnGameEnd();
        MainGameController.GameWin();
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
        int remaintime = (int)Mathf.Round(GameEndTime - Time.time);
        if (remaintime <= 0)
            remaintime = 0;
        if (remaintime == 0)
            GameOver();
        updateTimerText(remaintime);
        UpdateSocre(remaintime);
    }

    void UpdateSocre(int remaintime) {
        int score = (int)Mathf.Ceil(remaintime/(float)(MaxGameTime)*100);
        if (score < 0)
            score = 0;
        ScoreText.text = "Score: " + score.ToString();
    }

    void SetStartingTime() {
        StartingTime = (int) Mathf.Ceil(Time.time);
        GameEndTime = MaxGameTime + StartingTime;
    }

    void updateTimerText(int t) {
        TimerText.text = "Time: " + t.ToString();
    }
}
