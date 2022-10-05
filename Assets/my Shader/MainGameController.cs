using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;
using static NasaScript;
//using static NasaScript.DIALOG_ID;

public class MainGameController : MonoBehaviour {
    [SerializeField]  //以免不小心拉動到
    AudioClip[] audioClips;
    public static MainGameController Instance { get; private set; }

    public static MainGameController BufferInstance { get; private set; }

    public AudioClip[] audioEffects;
    private AudioSource audioSource; //一律掛載在maingameobject上方


    //public GameObject[] hideThese;
    public GameObject LoadingInterface; //loading panel本身
    public GameObject GameOverRetry;  //輸了prefab/如果關卡沒有輸贏.那就隨便拉一個fake
    public GameObject GameWinScreen;  //贏了prefab/隨便拉一個fake.同上.如果沒有輸贏就fake.有輸贏就是認真寫1~4
    public Image loadingImg;  //把5star圖片拉近來這邊

    static string nameOfNowScene;
    /// <summary>
    /// 僅代表關卡./*請存檔就更新*/
    /// </summary>
    static int NowGame;

    public static int TargetScenceNumber;

    static NasaScript.DIALOG_ID DIALOG_ID;


    static Dictionary<scenesName, int> scenedic = new Dictionary<scenesName, int>();
    /*
     * scenesName 
     * 0 = "SpaceScene" 
     * 1 ="Maker"
     * 2= "Travel"
       6= "Game1" // 光子
       3= "Game2" // 閃焰
       4= "Game3" // 移開恆星
       5= "Game4", // 泡泡龍
       7= "DialogScene"
       
    */

    [SerializeField]
    //static int DialogueNum =1 ;  /// <summary>
                                 /// 先拿來測試啊啊啊之後要記得改掉!!!預設是1!!
                                 /// 設定其他數字可以直接看其他劇情!
                                 /// </summary>
    
    /*static List<AsyncOperation> scenes = new List<AsyncOperation>();
    // 所有需要載入的的場景list  事先把場景名稱取名吧我怕打錯字*/
    public enum scenesName{SpaceScene = 0, Maker = 1, Travel = 2,
        Game1 = 6,Game2 = 3 ,Game3 = 4,Game4 = 5,
        DialogScene = 7, other = 8};
    

    void Start() {

        Debug.Log("start called");
        DontDestroyOnLoad(this.gameObject);


        audioSource = this.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;

        LoadingInterface = GameObject.Find("Canvas/Loading");
        GameOverRetry = GameObject.Find("Canvas/gameover");
        GameWinScreen = GameObject.Find("Canvas/gamewin");
        loadingImg = GameObject.Find("Canvas/Loading/5star").GetComponent<Image>();

        Instance.hideAll();
    }
    // Update is called once per frame
    void hideAll() {
        GameOverRetry.SetActive(false);
        GameWinScreen.SetActive(false);
        LoadingInterface.SetActive(false);
    }
    public int getSceneNumByName(scenesName name) {
        return scenedic[name];
    }


    /*/ 設定第幾個對話!! /*/
    public static void setDialog(DIALOG_ID dia) {
        Debug.Log("dia ID:" + dia.ToString());
        DIALOG_ID = dia;
    }
    public static DIALOG_ID getDialog() { 
        Debug.Log("載入對話" + DIALOG_ID.ToString()); 
        return DIALOG_ID;  
        
    }

    

    void setAudioClip(int TargetScenceNumber) {
        

        Debug.Log("now target!:" + TargetScenceNumber);

        if (TargetScenceNumber == getSceneNumByName(scenesName.DialogScene)) {  //DialogScene
            //DialogScene
            audioSource.clip = audioClips[0];  //對話歌 嘟 嘟~嘟

        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Travel)) {  //Travel
            //Travel
            audioSource.clip = audioClips[5]; //探險歌
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.SpaceScene)) {  //SpaceScene
            //spacetitle
            audioSource.clip = audioClips[6]; //宇宙歌
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Maker)) {  //Maker
            //Maker
            audioSource.clip = audioClips[3];  //愉快歌
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Game1)) {  //stage01
            //stage01
            NowGame = 4;
            audioSource.clip = audioClips[4];
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Game2)) {   //stage02
            //stage02
            NowGame = 1;
            
            audioSource.clip = audioClips[3];
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Game3)) {  //stage03
            //stage03
            NowGame = 2;
            audioSource.clip = audioClips[2];
        }
        else if (TargetScenceNumber == getSceneNumByName(scenesName.Game4)) {  //stage04
            //stage04
            NowGame = 3;
            
            audioSource.clip = audioClips[0];
        }
        else {
            //意料之外的scene名稱
            Debug.Log("在main的setAudioClip之中 有例外發生");
            audioSource.clip = audioClips[3];
        }

    }
    void Awake() {

        if (scenedic.Count ==0 ) {

            scenedic.Add(scenesName.SpaceScene, 0);
            scenedic.Add(scenesName.Maker, 1);
            scenedic.Add(scenesName.Travel, 2);
            scenedic.Add(scenesName.Game2, 3);
            scenedic.Add(scenesName.Game3, 4);
            scenedic.Add(scenesName.Game4, 5);
            scenedic.Add(scenesName.Game1, 6);
            scenedic.Add(scenesName.DialogScene, 7);
        }


        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }

    
    /*
       遊玩順序 : 遊玩順序是：game02➡️game03➡️game04➡️game01 
       也就是：恆星閃焰3➡️找到最亮恆星4➡️恆星的一生5➡️用望遠鏡看見光子6
     */


    /// <summary>
    /// 星門走這
    /// </summary>
    /// <param name="stargate"></param>
    public static void setTargetScene(scenesName name) {
        int target = scenedic[name];
        Debug.Log("set taarget =" + target);
        if (target < 8)
            TargetScenceNumber = target;
        else
            Debug.Log("目標場景文字不正確");
    }




    //這邊打開loading畫面並且作場景跳轉
    static IEnumerator Loading() {
        showLoadingScreen(true);
        //nameOfNowScene = SceneManager.GetActiveScene().name;
        AsyncOperation op = SceneManager.LoadSceneAsync(TargetScenceNumber);
        
        float progress;
        while (!op.isDone) {
            progress = Mathf.Clamp01(op.progress / .9f);
            Instance.loadingImg.fillAmount = progress;
            
            yield return null;
        }
        Debug.Log("loading done");
        showLoadingScreen(false);
        Instance.hideAll();
        Instance.setAudioClip(TargetScenceNumber);
        Instance.audioSource.Play();
        yield return null;
    }
    public static void showLoadingScreen(bool show) {

        Instance.LoadingInterface.SetActive(show);
    }


    /*

    public static void hideTheseThings(bool s) {
        showornot = s;
        for (int i = 0; i < hideThese.Length; i++) {
            hideThese[i].SetActive(showornot);
        }
    }
    */
    
    /*game1234接口在這裡*/
    /// <summary>
    /// 請在輸贏時叫這兩個方法. 並且自己去暫停自己遊戲中的協程(自行處理)
    /// </summary>
    /// <param name="here"></param>
    /// 


    /*這兩個孩子是接口*/
    public static void GameOver() {
        Instance.GameOverRetry.SetActive(true);
        // 掛在按鈕上 StartGame();
    }
    public void GameWin() {
        Save(NowGame);
        Debug.Log("Win, NowGame: " + NowGame.ToString());
        //Win(NowGame);
        setTargetScene(scenesName.DialogScene);

        if (NowGame == 1)
            setDialog(DIALOG_ID.GAME_TALK_01);
        else if (NowGame == 2)
            setDialog(DIALOG_ID.GAME_TALK_02);
        else if (NowGame == 3)
            setDialog(DIALOG_ID.GAME_TALK_03);
        else if (NowGame == 4)
            setDialog(DIALOG_ID.GAME_TALK_04);


        Instance.GameWinScreen.SetActive(true);
    }
    /*居然是在播音樂時決定now game 太神奇ㄌ*/

    /// <summary>
    /// 轉換到目標場景. 請先設置Target再衝
    /// </summary>
    public void StartGame() {
        Debug.Log("StartGame");
        nameOfNowScene = SceneManager.GetActiveScene().name;
        Instance.StartCoroutine(Loading());

    }


    //純粹展開對話並不是遊戲贏了
    //所以沒有必要解成就跟開win視窗
    public void GameNextTalk(){
        //為了保險起見 
        //NowGame = 0;
        nameOfNowScene = SceneManager.GetActiveScene().name;

        Debug.Log("nameOfNowScene" + nameOfNowScene);

        if (nameOfNowScene == scenesName.SpaceScene.ToString()) { //剛進遊戲

            setTargetScene(scenesName.DialogScene);
            setDialog(DIALOG_ID.OPENING);
            Debug.Log("開場白");
            
        }
        else if (nameOfNowScene == scenesName.DialogScene.ToString()) {
            if(PlayerPrefs.GetInt("MakerDone") == 1) {
                Debug.Log("去Travel");
                setTargetScene(scenesName.Travel);
            }
            else {

                Debug.Log("去maker");
                setTargetScene(scenesName.Maker);
            }

            
        }
        else if (nameOfNowScene == scenesName.Maker.ToString()) {
            //從Maker離開
            setTargetScene(scenesName.DialogScene);
            setDialog(DIALOG_ID.BOOSS_TALK_01);
            Debug.Log("捏好星球的劇情");


        }
        
        else if (nameOfNowScene == scenesName.Travel.ToString() && PlayerPrefs.GetInt("Stage") == 4) {
            //當你回到星球上 並且通關
            setDialog(NasaScript.DIALOG_ID.TALK_END);
            setTargetScene(MainGameController.scenesName.DialogScene);
        }
        else if (nameOfNowScene == scenesName.DialogScene.ToString() && PlayerPrefs.GetInt("Stage") == 4) { 
            //恩. 暫時沒有這個規畫

        
        }
        else if (nameOfNowScene == scenesName.Travel.ToString()) {  //到達星球時開啟通訊器的劇情

            //這段捨棄了
            setDialog(DIALOG_ID.BOOSS_TALK_02);

            setTargetScene(scenesName.Game2);   //到星球就開始遊戲囉!
        }
        else
            Debug.LogError("遊戲對話設定不正確.");

    }

    /*請存檔就更新*/

    /*破關的記錄*/
    static void Save(int nowgame) {
        Debug.Log("stage saved." + nowgame.ToString());
        PlayerPrefs.SetInt("Stage", nowgame);   //這是為了成就系統而存的
    }

   


    //世紀大銷毀 裝在tro銷毀按鈕上
    
    public static void clearAllstatic() {

        PlayerPrefs.DeleteAll();
        
    }
    public void clearAllBtn() {

        PlayerPrefs.DeleteAll();

    }

}
