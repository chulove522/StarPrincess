using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static MainGameController.SCENE_ID;

public class Stargate : MonoBehaviour
{
    
    // public GameObject[] stargates; //4games
    //CapsuleCollider col;
    public int nowstage;  /// <summary>
    /// 方便測試所以寫public
    /// </summary>

    
    void Start() {
        
    }

    void OnTriggerEnter2D(Collider2D player) {
        nowstage = PlayerPrefs.GetInt("Stage", 0);  //1~4(4 = FinishAll) . 0 = All not done.

        Debug.Log("touch"+ player.name);
        //stage here doesn't has the same meaning as torphies's.
        //trophies's stage means the cleared stage.
        //here, means the uncleared stage.
        /////game02 =4 //game03 = 5//game04 =6//game01 = 3 ////
        if (player.name == "Player"){

            Debug.Log("nowstage=" + nowstage.ToString());
        
            switch (nowstage) {

                case 0:
                    MainGameController.setTargetScene(MainGameController.scenesName.Game1);
                    
                    MainGameController.Instance.StartGame();
                    break;
                case 1:
                    MainGameController.setTargetScene(MainGameController.scenesName.Game2);
                    MainGameController.Instance.StartGame();
                    break;
                case 2:
                    MainGameController.setTargetScene(MainGameController.scenesName.Game3);
                    MainGameController.Instance.StartGame();
                    break;
                case 3:
                    MainGameController.setTargetScene(MainGameController.scenesName.Game4);
                    MainGameController.Instance.StartGame();
                    break;

            }
            Debug.Log("enter star gate:"+ nowstage);
        }
    }
}

/*
public class gate : MonoBehaviour {
    bool isDone;
    
    public gate() {
        isDone = false;
    }
    

}
麻煩死了

 */
