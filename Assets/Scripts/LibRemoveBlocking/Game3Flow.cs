using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game3Flow : MonoBehaviour
{
    public Text TimerText;
    public int MaxGameTime=60;
    public MainGameController mainGame;
    // Start is called before the first frame update

    public void Game03Start() {
        StartCoroutine(showtime());
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
        
    }


    IEnumerator showtime() {
        // TODO: condider menu "close" pressed time instread of Time.time
        // TODO: update showtime per frame
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            GameOver();
        TimerText.text = "Time: " + remaintime.ToString();
        yield return null;
    }

    
}
