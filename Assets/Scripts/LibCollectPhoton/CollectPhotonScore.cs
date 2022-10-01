using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPhotonScore : MonoBehaviour
{
    [SerializeField]
    int numberCollected = 0;
    [SerializeField]
    int scoreMultiple = 10;

    public Text ScoreText;

    public Text TimerText;

    public int MaxGameTime=15;

    public bool stop;

    MainGameController mainGameController;

    public GameObject[] stars;

    Move[] move;

    private void Start() {
        mainGameController = UnityEngine.GameObject.Find("MainGameController").GetComponent<MainGameController>();
        numberCollected = 0;
        stop = false;
        
        for (int i = 0; i < stars.Length; i++) {
            move[i] = stars[i].GetComponent<Move>();
        }
        

    }
    private void FixedUpdate() {
        showtime();
    }
    public void showtime() {
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            gameover();
        TimerText.text = "Time: " + remaintime.ToString();
    }
    public void gamewin() {
        if(stop == false)
            mainGameController.GameWin();

        stopAll();

    }
    void stopAll() {
        stop = true;
        for (int i = 0; i < stars.Length; i++) {
            move[i].stopall();
        }
        
    }
    public void gameover() {
        numberCollected = 0;
        if (stop == false)
            mainGameController.GameOver();

        stopAll();
    }


    public int GetScore() {
        return numberCollected * scoreMultiple;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        int score;
        if (other.tag == "PhotonGetScore") {
            Destroy(other);
            numberCollected++;
            // TODO remove this log after integeration
            //Debug.Log("socre is " + GetScore());
            score = GetScore();
            ScoreText.text = "Score: " + score.ToString();
            if (score == 100)
                gamewin();
        }
    }
}
