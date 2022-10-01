using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*this is game01 main !!!!*/
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

    //PhotonGenerator[] pg ;
    //
    //Move[] move;

    private void Start() {
        mainGameController = UnityEngine.GameObject.Find("MainGameController").GetComponent<MainGameController>();
        numberCollected = 0;
        stop = false;
        Debug.Log("startgame01");

        /*
        for (int i = 0; i < stars.Length; i++) {

            //  這招行不通
            //  move[i] = stars[i].GetComponent<Move>();

            //  這招行不通
            //pg[i] = stars[i].GetComponent<PhotonGenerator>();
            Debug.Log(move[i].ToString());
        }*/


    }
    /*
     放button 上*/
    public void startGame01() {
        for (int i = 0; i < stars.Length; i++) {
            stars[i].GetComponent<Move>().StartGame01();
            stars[i].GetComponent<PhotonGenerator>().StartGame01();
        }

        StartCoroutine(showtime());
        //    public void StartGame01() StartGame(); put button
    }
  
    IEnumerator showtime() {
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            gameover();
        TimerText.text = "Time: " + remaintime.ToString();

        yield return null;
    }
    public void gamewin() {
        if(stop == false)
            mainGameController.GameWin();

        stopAll();

    }
    void stopAll() {
        stop = true;
        for (int i = 0; i < stars.Length; i++) {
            stars[i].GetComponent<Move>().stopall();
            stars[i].GetComponent<PhotonGenerator>().endGame01();
        }
        StopAllCoroutines();
        
    }
    /*
       放button 上*/
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
/*this is game 01 main!*/