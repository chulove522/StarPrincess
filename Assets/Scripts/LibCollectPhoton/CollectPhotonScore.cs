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

    public static bool stop;

    MainGameController maincontrol;

    private void Start() {
        numberCollected = 0;
        stop = false;
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
        maincontrol.GameWin();

        stop = true;

    }
    public void gameover() {
        numberCollected = 0;
        if (stop == false)
            maincontrol.GameOver();

        stop = true;
    }


    public int GetScore() {
        return numberCollected * scoreMultiple;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PhotonGetScore") {
            Destroy(other);
            numberCollected++;
            // TODO remove this log after integeration
            //Debug.Log("socre is " + GetScore());
            ScoreText.text = GetScore().ToString();
        }
    }
}
