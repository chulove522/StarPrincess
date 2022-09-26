using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game2 : MonoBehaviour
{
    // Object references
    public GameObject Sun;
    public GameObject Shield;
    public GameObject Player;
    public GameObject BulletPrefab;
    public GameObject ScoreObject;
    public GameObject LifeObject;
    public GameObject TimeObject;

    // 分數
    public int Score = 0;
    // 生命值
    public int Life = 3;
    // 遊戲時間
    public int MaxGameTime = 16;

    // Score Component
    private Text scoreText;
    private Text lifeText;
    private Text timeText;

    private float nextChangeDirection;
    private float lastShoot;
    private int direction = 1;
    private bool stopped;
    
    private float lastUseShield;

    // Start is called before the first frame update
    void Start()
    {
        Shield.SetActive(false);
        scoreText = ScoreObject.GetComponent<Text>();
        lifeText = LifeObject.GetComponent<Text>();
        timeText = TimeObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( stopped == false)
        {
            // Controll Sun
            if (Time.time > nextChangeDirection)
            {
                direction *= -1;
                nextChangeDirection = Time.time + Random.Range(2, 10);
            }
            if (Sun.transform.position.x <= -550) {
                direction = 1;
                nextChangeDirection = Time.time + Random.Range(2, 10);
            }
            if (Sun.transform.position.x >= 550) {
                direction = -1;
                nextChangeDirection = Time.time + Random.Range(2, 10);
            }
            Sun.transform.position += Vector3.right * direction;


            // Controll Sheild
            if (Time.time - lastUseShield >= 1.5)
            {
                if (Shield.activeSelf)
                {
                    Shield.SetActive(false);
                }
            }
            if (Time.time - lastUseShield >= 2 || lastUseShield == 0)
            {
                if (Input.GetKeyDown("space"))
                {
                    Shield.SetActive(true);
                    lastUseShield = Time.time;
                }
            }

            // Fire
            if (Time.time - lastShoot >= 2)
            {
                var bullet = Instantiate(BulletPrefab, Sun.transform.position, Quaternion.identity, transform);
                Fire fire = bullet.GetComponent<Fire>();
                fire.Target = Player;
                lastShoot = Time.time;
            }

            scoreText.text = "Score: " + Score.ToString();
            lifeText.text = "Life: " + Life.ToString();
            timeText.text = "Time: " +  ((int)(MaxGameTime - Time.time)).ToString();
        }


        if (Life == 0 || Time.time >= MaxGameTime )
        {
            // 死了要結束的處理
            stopped = true;
        }
       
    }
}
