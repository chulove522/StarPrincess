using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour {
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    public GameObject[] hideThese;
    public GameObject LoadingInterface;
    public Image loadingImg;
    Trophies t = new Trophies();

    //所有需要載入的的場景list
    List<AsyncOperation> scenes = new List<AsyncOperation>();
    //事先把場景名稱取名吧我怕打錯字
    string[] scenesName = { "SpaceScene", "Maker", "Travel" ,
        "stage01","stage02","stage03","stage04",
        "Conversation01","Conversation02",};

    private bool showornot = false;
    void Start() {
        
        audioSource = this.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        setAudioClip();
        audioSource.Play();
    }
    // Update is called once per frame

    void setAudioClip() {
        string name = SceneManager.GetActiveScene().name;
        if (name == scenesName[0]) {
            //spacetitle
            audioSource.clip = audioClips[6];
        }else if(name == scenesName[1]) {
            //Makeer
            audioSource.clip = audioClips[3];
        }

    }
    void Update()
    {
        
    }
    void Save(int stagenum) {
        PlayerPrefs.SetInt("Stage",stagenum);
        //PlayerPrefs.GetString("Stage");



    }
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

    public void clearAll() {

        PlayerPrefs.DeleteAll();
        t.initClear();
    }
    public void Win(int stage) {
        t.FinishStage(stage);
    }
    public void Lose() {

    }
    public void showAward() {
        Debug.Log("you found 3 stars!");
    }
}
