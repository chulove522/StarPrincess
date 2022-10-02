using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*this is game01 main !!!!*/
public class Game1 : MonoBehaviour
{
    [SerializeField]
    int numberCollected = 0;
    [SerializeField]
    int scoreMultiple = 10;

    public Text ScoreText;

    public Text TimerText;

    public int MaxGameTime=50;
    int GameEndTime = 0;
    int StartingTime = 0;
    bool isGameStarted = false;
    bool isGameEnd = false;

    //MainGameController mainGameController;

    public GameObject[] stars;

    //PhotonGenerator[] pg ;
    //
    //Move[] move;

    private void Start() {
        
        SetTimeText(MaxGameTime);
    }

    private void Update() {
        if (!isGameStarted || isGameEnd)
            return;
        int remaintime = (int) Mathf.Round(GameEndTime - Time.time);
        if (remaintime <= 0)
            remaintime = 0;
        if (remaintime == 0)
            GameOver();
        SetTimeText(remaintime);
    }
    /*
     放button 上*/
    
    public void SetStartingTime() {
        StartingTime = (int) Mathf.Ceil(Time.time);
        GameEndTime = MaxGameTime + StartingTime;
    }
    public void startGame01() {
        numberCollected = 0;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].GetComponent<Move>().StartGame01();
            stars[i].GetComponent<PhotonGenerator>().StartGame01();
        }
        SetStartingTime();
        isGameStarted = true;
    }

    public void RestartGame01() {
        // I'm asleep
        SceneManager.LoadScene("Game1");
    }

    void SetTimeText(int remaintime) {
        TimerText.text = "Time: " + remaintime.ToString();
    }
    public void GameWin() {
        MainGameController.GameWin();
        OnGameStop();
    }

    void OnGameStop() {
        isGameEnd = true;
        stopAll();
        isGameStarted = false;
    }
    void stopAll() {
        if (!isGameStarted)
            return;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].GetComponent<Move>().stopall();
            stars[i].GetComponent<PhotonGenerator>().endGame01();
        }
    }
    /*
       放button 上*/
    public void GameOver() {
        MainGameController.GameOver();
        OnGameStop();
    }


    public int GetScore() {
        return numberCollected * scoreMultiple;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PhotonGetScore") {
            Destroy(other);

            if (!isGameStarted || isGameEnd)
                return;

            numberCollected++;
            int score = GetScore();
            ScoreText.text = "Score: " + score.ToString();
            if (score == 100)
                GameWin();
        }
    }
}
/*this is game 01 main!*/