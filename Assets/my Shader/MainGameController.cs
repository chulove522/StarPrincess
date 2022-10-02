using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class MainGameController : MonoBehaviour {
    [SerializeField]  //以免不小心拉動到
    AudioClip[] audioClips;
    public static MainGameController Instance { get; private set; }
    public AudioClip[] audioEffects;
    private AudioSource audioSource; //一律掛載在maingameobject上方


    //public GameObject[] hideThese;
    public GameObject LoadingInterface; //loading panel本身
    public GameObject GameOverRetry;  //輸了prefab/如果關卡沒有輸贏.那就隨便拉一個fake
    public GameObject GameWinScreen;  //贏了prefab/隨便拉一個fake.同上.如果沒有輸贏就fake.有輸贏就是認真寫1~4
    public Image loadingImg;  //把5star圖片拉近來這邊
    public GameObject TrophiesPanel;
    public Button closeTro; //成就系統的叉叉壞了所以弄一個新的

    static string nameOfNowScene;
    static int NowGame;

    public static int TargetScenceNumber;


    [SerializeField]
    static int DialogueNum =1 ;  /// <summary>
                                 /// 先拿來測試啊啊啊之後要記得改掉!!!預設是1!!
                                 /// 設定其他數字可以直接看其他劇情!
                                 /// </summary>
    
    static List<AsyncOperation> scenes = new List<AsyncOperation>();
    // 所有需要載入的的場景list  事先把場景名稱取名吧我怕打錯字
    string[] scenesName = { "SpaceScene", "Maker", "Travel" ,
        "Game1","Game2","Game3","Game4",
        "DialogScene","other",};


    void Start() {

        Debug.Log("start called");
        DontDestroyOnLoad(this.gameObject);
        hideAll();



        audioSource = this.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        setAudioClip();
        audioSource.Play();

    }
    // Update is called once per frame
    void hideAll() {
        GameOverRetry.SetActive(false);
        GameWinScreen.SetActive(false);
        LoadingInterface.SetActive(false);
        TrophiesPanel.SetActive(false);
        closeTro?.gameObject.SetActive(false);
    }


    /*/ 設定第幾個對話!! /*/
    public static void setDialog(int dia) {
        DialogueNum = dia;
    }
    public static int getDialog() { Debug.Log("載入對話" + DialogueNum.ToString()); 
        if (DialogueNum > 0) return DialogueNum; else return 1;  
        
    }

    

    private static bool showornot = false;


    void setAudioClip() {
        nameOfNowScene = SceneManager.GetActiveScene().name;
        //nameOfNowScene = Instance.scenesName[TargetScenceNumber].ToString();

        Debug.Log("now!:" + nameOfNowScene);
        if (nameOfNowScene == scenesName[7]) {  //DialogScene
            //DialogScene
            audioSource.clip = audioClips[5];  //對話歌 嘟 嘟~嘟

        }
        else if (nameOfNowScene == scenesName[2]) {  //Travel
            //Travel
            audioSource.clip = audioClips[5]; //探險歌
        }
        else if (nameOfNowScene == scenesName[0]) {  //SpaceScene
            //spacetitle
            audioSource.clip = audioClips[6]; //宇宙歌
        }
        else if (nameOfNowScene == scenesName[1]) {  //Maker
            //Maker
            audioSource.clip = audioClips[3];  //愉快歌
        }
        else if (nameOfNowScene == scenesName[3]) {  //stage01
            //stage01
            NowGame = 1;
            audioSource.clip = audioClips[4];
        }
        else if (nameOfNowScene == scenesName[4]) {   //stage02
            //stage02
            NowGame = 2;
            audioSource.clip = audioClips[3];
        }
        else if (nameOfNowScene == scenesName[5]) {  //stage03
            //stage03
            NowGame = 3;
            audioSource.clip = audioClips[2];
        }
        else if (nameOfNowScene == scenesName[6]) {  //stage04
            //stage04
            NowGame = 4;
            audioSource.clip = audioClips[0];
        }
        else {
            //意料之外的scene名稱
            Debug.Log("在main的setAudioClip之中 有意料之外的scene名稱");
            audioSource.clip = audioClips[3];
        }

    }
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }
    }


    /*
     * scenesName 
     * 0 = "SpaceScene" 
     * 1 ="Maker"
     * 2= "Travel"
       6= "game1" // 光子
       3= "game02" // 閃焰
       4= "game03" // 移開恆星
       5= "game04", // 泡泡龍
       7= "DialogScene"
       8="Conversation02"

       遊玩順序 : 遊玩順序是：game02➡️game03➡️game04➡️game01 
       也就是：恆星閃焰3➡️找到最亮恆星4➡️恆星的一生5➡️用望遠鏡看見光子6
     */

    /// <summary>
    /// 星門走這
    /// </summary>
    /// <param name="stargate"></param>
    public static void setTargetScene(int t) {
        if (t >= 0 && t < 8)
            TargetScenceNumber = t;
        else
            Debug.Log("目標場景數字不正確");
    }

    //restart按鈕走這
    public void setTargetSceneBtn() {
        Debug.Log("set restart stage" + NowGame.ToString());
        TargetScenceNumber = NowGame + 2;

    }



    //這邊打開loading畫面並且作場景跳轉
    static IEnumerator Loading() {
        float totalProgress = 0;
        for (int i = 0; i < scenes.Count; i++) {
            while (!scenes[i].isDone) {
                totalProgress += scenes[i].progress;
                Instance.loadingImg.fillAmount = totalProgress / scenes.Count;
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);

        showLoadingScreen(false);
        Instance.hideAll();
        Instance.setAudioClip();
        Instance.audioSource.Play();

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
    static int getSceneNum(int gamenum) {
        return gamenum + 2;
    }
    
    /*game1234接口在這裡*/
    /// <summary>
    /// 請在輸贏時叫這兩個方法. 並且自己去暫停自己遊戲中的協程(自行處理)
    /// </summary>
    /// <param name="here"></param>
    /// 


    /*這兩個孩子是接口*/
    public static void GameOver() {
        Instance.GameOverRetry.SetActive(true);
        //設定Target
        setTargetScene(getSceneNum(NowGame));
    }
    public void GameWin() {
        Debug.Log("Win, NowGame: " + NowGame.ToString());
        Win(NowGame);
    }
    /*居然是在播音樂時決定now game 太神奇ㄌ*/

    /// <summary>
    /// 轉換到目標場景. 請先設置Target再衝
    /// </summary>
    public void StartGame() {
        Debug.Log("StartGame");
        // hideTheseThings(true);
        showLoadingScreen(true);
        
        SceneManager.LoadScene(nameOfNowScene,LoadSceneMode.Single);
        //scenes.Add(SceneManager.LoadSceneAsync("Travel",LoadSceneMode.Additive));

        Instance.StartCoroutine(Loading());

    }



    public void Win(int stageNum) {
        Instance.GameWinScreen.SetActive(true);


        if (stageNum > 0 && stageNum < 5) {  //1234
            Save(stageNum);
            setTargetScene(7); //回到主對話
            if(stageNum == 1)
                setDialog(4);
            else if (stageNum == 2)
                setDialog(5);
            else if (stageNum == 3)
                setDialog(6);
            else if (stageNum == 4)
                setDialog(7);
        }
        else if(stageNum ==0) {
            //星球
            setTargetScene(7); //回到主對話
            setDialog(2);  //捏好星球

        }
        else
            Debug.LogError("破關數字設定不正確.應該是1~4");

        showAward();
        
    }


    
    /*破關的記錄*/
    static void Save(int stagenum) {
        Debug.Log("stage saved." + stagenum.ToString());
        PlayerPrefs.SetInt("Stage", stagenum);
    }

    public void showAward() {
        
        Instance.TrophiesPanel.GetComponent<Trophies>().FinishStage();
        Instance.TrophiesPanel.SetActive(true);
        Instance.closeTro.gameObject.SetActive(true);

        Debug.Log("you found 3 stars!");
    }


    //世紀大銷毀
    /*
    public void clearAll() {

        PlayerPrefs.DeleteAll();
        TrophiesPanel.GetComponent<Trophies>().initClear();
    }
    */
}
