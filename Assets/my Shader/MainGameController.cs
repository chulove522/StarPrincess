using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static NasaScript;

public class MainGameController : MonoBehaviour {
    public static MainGameController Instance { get; private set; }
    public static MainGameController BufferInstance { get; private set; }
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
    static SCENE_ID nowSceneID;
    static SCENE_ID targetSceneID;

    
    [SerializeField]
    static DIALOG_ID DialogueNum = DIALOG_ID.OPENING ;  /// <summary>
                                 /// 先拿來測試啊啊啊之後要記得改掉!!!預設是1!!
                                 /// 設定其他數字可以直接看其他劇情!
                                 /// </summary>
    
    static List<AsyncOperation> scenes = new List<AsyncOperation>();
    public enum SCENE_ID: int {
        MIN_SCENE = 0,
        SPACE_SCENE = 0,
        MAKER = 1,
        TRAVEL = 2,
        GAME_1 = 3,
        GAME_2 = 4,
        GAME_3 = 5,
        GAME_4 = 6, 
        DIALOG_SCENE = 7, 
        OTHER = 8,
        MAX_SCENE
    };

    class SceneAttr {
        // TODO set to private
        public SCENE_ID sceneID;
        public List<DIALOG_ID> dialogs;
        public string audioPath; // path for resources
        public SceneAttr() {
            // TODO better constructor
            // default values
            dialogs = new List<DIALOG_ID>();
            audioPath = "Audio/conversation happy.mp3";
        }
    };
    // TODO: integration all to attr table
    // TODO: improve WTF constructor
    static Dictionary<SCENE_ID, SceneAttr> idToAttr = new Dictionary<SCENE_ID, SceneAttr>();
    static Dictionary<SCENE_ID, string> idToName = new Dictionary<SCENE_ID, string>();
    static Dictionary<string, SCENE_ID> nameToId = new Dictionary<string, SCENE_ID>();


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
    void Start() {

        Debug.Log("start called");
        updateInstance();
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
    public static void setDialog(DIALOG_ID dia) {
        DialogueNum = dia;
    }
    public static DIALOG_ID getDialog() {
        Debug.Log("載入對話" + DialogueNum.ToString());
        // juse let it error, I need exceptoin log
        // TODO error handler
        return DialogueNum;
    }

    private static bool showornot = false;

    void setAudioClip() {
        nameOfNowScene = SceneManager.GetActiveScene().name;
        nowSceneID = getSceneID(nameOfNowScene);
        audioSource.clip = getAudioWithId(nowSceneID);
    }
    void Awake() {
        updateInstance();
    }

    /// <summary>
    /// 星門走這
    /// </summary>
    /// <param name="stargate"></param>
    public static void setTargetScene(SCENE_ID t) {
        if (t >= 0 && t < SCENE_ID.MAX_SCENE)
            targetSceneID = t;
        else
            Debug.Log("目標場景數字不正確");
    }
    // TODO: why to button
    // TODO: restart need get argument
    //restart按鈕走這
    public void setTargetSceneBtn() {
        targetSceneID = nowSceneID;
        Debug.Log("set restart stage" + targetSceneID);
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
        // TODO
        setTargetScene(nowSceneID);
    }
    public void GameWin() {
        Debug.Log("Win, NowGame: " + nowSceneID);
        Win(nowSceneID);
    }
    /*居然是在播音樂時決定now game 太神奇ㄌ*/

    /// <summary>
    /// 轉換到目標場景. 請先設置Target再衝
    /// </summary>
    public void StartGame() {
        Debug.Log("StartGame");
        // hideTheseThings(true);
        showLoadingScreen(true);
        scenes.Add(SceneManager.LoadSceneAsync(getSceneName(targetSceneID)));
        Instance.StartCoroutine(Loading());

    }



    public void Win(SCENE_ID stageId) {
        Instance.GameWinScreen.SetActive(true);
        // TODO: please update table
        // TODO: error handling
        setDialog(getDialogID(stageId));
        showAward();
    }


    
    /*破關的記錄*/
    static void Save(SCENE_ID sceneID) {
        PlayerPrefs.SetInt("Stage", (int) sceneID);
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
