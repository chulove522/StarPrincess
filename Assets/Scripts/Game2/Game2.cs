//using System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class Game2 : MonoBehaviour
{
    MainGameController mainGameController;
    // Object references
    public GameObject Sun;
    public GameObject Shield;
    public GameObject Player;
    public GameObject BulletPrefab;
    public GameObject ScoreObject;
    public GameObject LifeObject;
    public GameObject TimeObject;

    // 分數
    private int Score = 0;
    // 生命值
    private int Life = 8;
    // 遊戲時間
    public int MaxGameTime = 16;
    public int StartingTime;
    int GameEndTime = 0;

    // Score Component
    private Text scoreText;
    private Text lifeText;
    private Text timeText;

    private float nextChangeDirection;

    private int direction = 1;
    private bool stopped;
    
    private int lastUseShield=10;
    private GameObject bulletClone;

    //
    //private List<GameObject> activeBullets;

    // Start is called before the first frame update
    void Start()
    {
        mainGameController = UnityEngine.GameObject.Find("MainGameController").GetComponent<MainGameController>();
        Shield.SetActive(false);
        scoreText = ScoreObject.GetComponent<Text>();
        lifeText = LifeObject.GetComponent<Text>();
        timeText = TimeObject.GetComponent<Text>();
        
    }
    /*接口!!!!!!!!!!!!!*/
    public void gameStart() {
        SetStartingTime();
 
        routineSunMovement = StartCoroutine(MoveSun());
    }

    // Update is called once per frame
    void Update()
    {
        int remaintime = (int)Mathf.Round(GameEndTime - Time.time);
        if (remaintime <= 0)
            remaintime = 0;
        if (remaintime == 0)
            gameover();

        updateTimerText(remaintime);


        // Controll Sheild
        if (lastUseShield > 0)  //可以使用次數=10 並且每1秒才能開啟一個
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(shieldControl());
                    lastUseShield -= 1;
                    //lastUseShield = Time.time;
                }
            }
    }
    IEnumerator shieldControl() {
        Shield.SetActive(true);
        yield return new WaitForSeconds(1f);
        Shield.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
    //亂放
    Coroutine routineBulletMovement;
    Coroutine routineSunMovement;
    void makeBullet() {
        // Fire 拿掉 不要讓子彈有自我意識
        bulletClone = (GameObject)Instantiate(BulletPrefab, Sun.transform.localPosition, Quaternion.identity);
        bulletClone.transform.SetParent(transform);
        bulletClone.transform.localScale = Vector3.one;
        bulletClone.SetActive(true);
        routineBulletMovement = StartCoroutine(MoveTo(bulletClone, bulletClone.transform.position, Player.transform.position, 40));
    }
    public void gameover() {
        // 死了要結束的處理
        stopped = true;
        StopCoroutine(routineBulletMovement);
        StopCoroutine(routineSunMovement);
        MainGameController.GameOver();
    }


    //時間到就贏
    public void gamewin() {
        StopCoroutine(routineSunMovement);
        StopCoroutine(routineBulletMovement);
        MainGameController.GameWin();
    }
    public void addScore() {
        this.Score += 10;
    }
    public void minusLife() {
        this.Life -= 1;
    }


    public void showscore() {
        scoreText.text = "Score: " + Score.ToString();
        StopCoroutine(routineBulletMovement);
    }
    public void showLife() {
        lifeText.text = "Life: " + Life.ToString();
        StopCoroutine(routineBulletMovement);
        if (Life == 0) {
            gameover();
        }
    }

    void updateTimerText(int t) {
        timeText.text = "Time: " + t.ToString();


    }
    void SetStartingTime() {
        StartingTime = (int)Mathf.Ceil(Time.time);
        GameEndTime = MaxGameTime + StartingTime;
    }


    /*
    public void showtime() {
        int remaintime = (MaxGameTime - Time.time) > 0 ? (int)(MaxGameTime - Time.time) : 0;
        if (remaintime == 0)
            gamewin();
        timeText.text = "Time: " + remaintime.ToString();
    }
    
     */
    private IEnumerator MoveSun() {
        while(stopped != true) {
            // Controll Sun
            if (Time.time > nextChangeDirection) {
                direction *= -1;
                nextChangeDirection = Time.time + UnityEngine.Random.Range(2, 10);
            }
            if (Sun.transform.position.x <= -1.3f) {
                direction = 1;
                nextChangeDirection = Time.time + UnityEngine.Random.Range(2, 10);
            }
            if (Sun.transform.position.x >= 1.3f) {
                direction = -1;
                nextChangeDirection = Time.time + UnityEngine.Random.Range(2, 10);
            }

            StartCoroutine(MoveTo(Sun, Sun.transform.position, Sun.transform.position+Vector3.right * direction, 15));
            makeBullet();
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator MoveTo(GameObject obj, Vector3 currentPos, Vector3 targetPos, float speed) {


        var duration = 20 / speed;

        var timePassed = 0f;
        while (timePassed < duration) {
            // always a factor between 0 and 1
            var factor = timePassed / duration;

            obj.transform.position = Vector3.Lerp(currentPos, targetPos, factor);

            // increase timePassed with Mathf.Min to avoid overshooting
            timePassed += Math.Min(Time.deltaTime, duration - timePassed);

            // "Pause" the routine here, render this frame and continue
            // from here in the next frame
            yield return null;
        }

        // move done!
        yield return null;
    }
}
