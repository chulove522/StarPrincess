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
    /// 僅代表關卡
    /// </summary>
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

    /*
    static void addScenePair(string name, SCENE_ID id) {
        idToName.Add(id, name);
        nameToId.Add(name, id);
    }

    static SCENE_ID getSceneID(string sceneName) {
        // TODO error handleing
        return nameToId[sceneName];
    }

    static string getSceneName(SCENE_ID sceneId) {
        // TODO error handleing
        return idToName[sceneId];
    }


    static void initScenePacking() {
        for (int i=0; i < (int) SCENE_ID.MAX_SCENE; i++) {
            SCENE_ID id = (SCENE_ID)i;
            idToAttr.Add(id, new SceneAttr());
            // TODO: better constructor / setter
            idToAttr[id].sceneID = id;
        }
        initSceneIdDialogIdMapping();
        initAudios();
    }

    static void initAudios() {
        // TODO: refactoring, merge it to packing function
        // TODO: or other prefab to store audi list
        idToAttr[SCENE_ID.DIALOG_SCENE].audioPath = "Audio/universe title.mp3";
        idToAttr[SCENE_ID.TRAVEL].audioPath = "Audio/universe title.mp3";
        idToAttr[SCENE_ID.SPACE_SCENE].audioPath = "Audio/urgent.mp3";
        idToAttr[SCENE_ID.MAKER].audioPath = "Audio/maker Blue_Moon.mp3";
        idToAttr[SCENE_ID.GAME_1].audioPath = "Audio/sad and slow.mp3";
        idToAttr[SCENE_ID.GAME_2].audioPath = "Audio/maker Blue_Moon.mp3";
        idToAttr[SCENE_ID.GAME_3].audioPath = "Audio/happyandunkown.mp3";
        idToAttr[SCENE_ID.GAME_4].audioPath = "Audio/conversation happy.mp3";
    }

    static void initScneneIDMapping() {

        addScenePair("SpaceScene", SCENE_ID.SPACE_SCENE);
        addScenePair("Maker", SCENE_ID.MAKER);
        addScenePair("Travel", SCENE_ID.TRAVEL);
        addScenePair("Game1", SCENE_ID.GAME_1);
        addScenePair("Game2", SCENE_ID.GAME_2);
        addScenePair("Game3", SCENE_ID.GAME_3);
        addScenePair("Game4", SCENE_ID.GAME_4);
        addScenePair("DialogScene", SCENE_ID.DIALOG_SCENE);
        addScenePair("other", SCENE_ID.OTHER);
    }

    static void initSceneIdDialogIdMapping() {
        // TODO: update this table
        idToAttr[SCENE_ID.SPACE_SCENE].dialogs.Add(DIALOG_ID.OPENING);

        idToAttr[SCENE_ID.MAKER].dialogs.Add(DIALOG_ID.BOOSS_TALK_01);
        idToAttr[SCENE_ID.MAKER].dialogs.Add(DIALOG_ID.BOOSS_TALK_02);

        idToAttr[SCENE_ID.GAME_1].dialogs.Add(DIALOG_ID.GAME_TALK_01);
        idToAttr[SCENE_ID.GAME_2].dialogs.Add(DIALOG_ID.GAME_TALK_02);
        idToAttr[SCENE_ID.GAME_3].dialogs.Add(DIALOG_ID.GAME_TALK_03);
        idToAttr[SCENE_ID.GAME_4].dialogs.Add(DIALOG_ID.GAME_TALK_04);


        idToAttr[SCENE_ID.OTHER].dialogs.Add(DIALOG_ID.OPENING);
    }

    static AudioClip getAudioWithId(SCENE_ID sceneID) {
        if (idToAttr.ContainsKey(sceneID)) {
            // TODO: cache pool to load audios
            return Resources.Load<AudioClip>(idToAttr[sceneID].audioPath);
        } else {
            return Resources.Load<AudioClip>("Audio/happyandunkown.mp3");
        }
    }
    static DIALOG_ID getDialogID(SCENE_ID sceneId, int seq = 0) {
        // TODO: eror handling
        return idToAttr[sceneId].dialogs[seq];
    }
    static void initTables() {
        initScneneIDMapping();
        initScenePacking();
    }

    static MainGameController() { // static constructor to init table
        initTables();
    }

    void updateInstance() {
        DontDestroyOnLoad(transform.root.gameObject);
        DontDestroyOnLoad(LoadingInterface.transform.root.gameObject);
        DontDestroyOnLoad(loadingImg.transform.root.gameObject);
        BufferInstance = Instance;
        Instance = this;
    }
    */
    void Start() {

        Debug.Log("start called");
        DontDestroyOnLoad(this.gameObject);
        
        
        audioSource = this.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        Instance.setAudioClip();
        audioSource.Play();

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
            audioSource.clip = audioClips[0];  //對話歌 嘟 嘟~嘟

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
        
        switch (MainGameController.Stage) {


            case 0:
                MainGameController.setTargetScene(3);

                MainGameController.Instance.StartGame();
                break;
            case 1:
                MainGameController.setTargetScene(4);
                MainGameController.Instance.StartGame();
                break;
            case 2:
                MainGameController.setTargetScene(5);
                MainGameController.Instance.StartGame();
                break;
            case 3:
                MainGameController.setTargetScene(6);
                MainGameController.Instance.StartGame();
                break;

        }
    }
    public void GameWin() {
        Debug.Log("Win, NowGame: " + NowGame.ToString());
        Win(NowGame);

        Instance.GameWinScreen.SetActive(true);
    }
    /*居然是在播音樂時決定now game 太神奇ㄌ*/

    /// <summary>
    /// 轉換到目標場景. 請先設置Target再衝
    /// </summary>
    public void StartGame() {
        Debug.Log("StartGame");
        // hideTheseThings(true);
        showLoadingScreen(true);
        
        SceneManager.LoadScene(TargetScenceNumber,LoadSceneMode.Single);
        //scenes.Add(SceneManager.LoadSceneAsync("Travel",LoadSceneMode.Additive));
        Instance.StartCoroutine(Loading());

    }



    public void Win(int stageNum) {
        
        if (stageNum > 0 && stageNum < 5) {  //1234 關卡
            Save(stageNum);
            setTargetScene(7); //去對話
            if (stageNum == 1)
                setDialog(4);
            else if (stageNum == 2)
                setDialog(5);
            else if (stageNum == 3)
                setDialog(6);
            else if (stageNum == 4)
                setDialog(7);
        }
        else if (stageNum == 0 && (nameOfNowScene == scenesName[1])) {  //Maker
            //捏好回對話!!                                                            
            setTargetScene(7); //捏好星球!
            setDialog(2);  //捏好星球的劇情

        }
        else if (stageNum == 0){ //第一個狀態!

            setTargetScene(1); //去捏星球!
            //setDialog(2);  //捏好星球的劇情
        }
        else if (stageNum == 5) { 
            //出發囉! 填5!
            setTargetScene(2); //Travel
            setDialog(3);  //到那邊的劇情


        }
        else
            Debug.LogError("破關數字設定不正確.");

    }

    public static int Stage; 

    
    /*破關的記錄*/
    static void Save(int stagenum) {
        Debug.Log("stage saved." + stagenum.ToString());
        //PlayerPrefs.SetInt("Stage", stagenum);
        Stage = stagenum;
    }

   


    //世紀大銷毀
    /*
    public void clearAll() {

        PlayerPrefs.DeleteAll();
        TrophiesPanel.GetComponent<Trophies>().initClear();
    }
    */
}
