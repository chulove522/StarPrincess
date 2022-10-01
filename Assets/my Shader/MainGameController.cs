﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {
    [SerializeField]  //以免不小心拉動到
    AudioClip[] audioClips;

    public AudioClip[] audioEffects;
    private AudioSource audioSource; //一律掛載在maingameobject上方


    public GameObject[] hideThese;
    public GameObject LoadingInterface; //loading panel本身
    public GameObject GameOverRetry;  //輸了prefab/如果關卡沒有輸贏.那就隨便拉一個fake
    public GameObject GameWinScreen;  //贏了prefab/隨便拉一個fake
    public Image loadingImg;  //把5star圖片拉近來這邊
    public GameObject TrophiesPanel;
    Trophies t;


    [SerializeField]
    static int DialogueNum = 1;  /// <summary>
    /// 先拿來測試啊啊啊之後要記得改掉!!!預設是1!!
    /// </summary>



    public static void setDialog(int dia) {
        DialogueNum = dia;
    }
    public static int getDialog => DialogueNum;

    //所有需要載入的的場景list
    List<AsyncOperation> scenes = new List<AsyncOperation>();
    //事先把場景名稱取名吧我怕打錯字
    string[] scenesName = { "SpaceScene", "Maker", "Travel" ,
        "Game1","Game2","Game3","Game4",
        "DialogScene","other",};

    private bool showornot = false;
    void Start() {
        GameOverRetry.SetActive(false);
        GameWinScreen.SetActive(false);
        audioSource = this.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        setAudioClip();
        audioSource.Play();
        t = TrophiesPanel.transform.GetComponent<Trophies>();

    }
    // Update is called once per frame

    void setAudioClip() {
        string name = SceneManager.GetActiveScene().name;


        if (name == scenesName[7]) {  //DialogScene
            //DialogScene
            audioSource.clip = audioClips[5];  //對話歌 嘟 嘟~嘟

        }
        else if (name == scenesName[2]) {  //Travel
            //Travel
            audioSource.clip = audioClips[5]; //探險歌
        }
        else if (name == scenesName[0]) {  //SpaceScene
            //spacetitle
            audioSource.clip = audioClips[6]; //宇宙歌
        }
        else if (name == scenesName[1]) {  //Maker
            //Maker
            audioSource.clip = audioClips[3];  //愉快歌
        }
        else if (name == scenesName[3]) {  //stage01
            //stage01
            audioSource.clip = audioClips[4];
        }
        else if (name == scenesName[4]) {   //stage02
            //stage02
            audioSource.clip = audioClips[3];
        }
        else if (name == scenesName[5]) {  //stage03
            //stage03
            audioSource.clip = audioClips[2];
        }
        else if (name == scenesName[6]) {  //stage04
            //stage04
            audioSource.clip = audioClips[0];
        }
        else {
            //意料之外的scene名稱
            Debug.Log("在main的setAudioClip之中 有意料之外的scene名稱");
            audioSource.clip = audioClips[3];
        }

    }
    void Update()
    {
        
    }


    /*
     * scenesName 
     * 0 = "SpaceScene" 
     * 1 ="Maker"
     * 2= "Travel"
       3= "stage01"
       4= "stage02"
       5= "stage03"
       6= "stage04",
       7= "DialogScene"
       8="Conversation02"
     */
    public void StartGame(int scenceNumber) {

        hideTheseThings(true);
        showLoadingScreen();
        scenes.Add(SceneManager.LoadSceneAsync(scenesName[scenceNumber]));
        //scenes.Add(SceneManager.LoadSceneAsync("Travel",LoadSceneMode.Additive));

        StartCoroutine(Loading());
    }

    //這邊打開loading畫面並且作場景跳轉
    IEnumerator Loading() {
        float totalProgress = 0;
        for (int i = 0; i < scenes.Count; i++) {
            while (!scenes[i].isDone) {
                totalProgress += scenes[i].progress;
                loadingImg.fillAmount = totalProgress / scenes.Count;
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);

    }
    public void showLoadingScreen() {
        LoadingInterface.SetActive(true);
    }
    public void hideTheseThings(bool s) {
        showornot = s;
        for (int i = 0; i < hideThese.Length; i++) {
            hideThese[i].SetActive(showornot);
        }
    }

    public void GameOver() {
        GameOverRetry.SetActive(true);
    }
    public void GameWin() {
        GameWinScreen.SetActive(true);
    }
    public void clearAll() {

        PlayerPrefs.DeleteAll();
        t.initClear();
    }
    public void Win(int stageNum) {
        if (stageNum > 0 && stageNum < 5)
            Save(stageNum);
        else
            Debug.LogError("破關數字設定不正確.應該是1~4");
        t.FinishStage();
    }

    void Save(int stagenum) {
        PlayerPrefs.SetInt("Stage", stagenum);
    }

    public void Lose() {

    }
    public void showAward() {
        Debug.Log("you found 3 stars!");
    }
}
