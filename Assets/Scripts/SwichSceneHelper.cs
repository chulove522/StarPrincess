using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainGameController;

public class SwichSceneHelper : MonoBehaviour
{
    public SCENE_ID nextSceneID;
    public MainGameController controller;
    // Start is called before the first frame update

    public void onClick()
    {
        Debug.Log("next scene ID" + nextSceneID);
        MainGameController.setTargetScene(nextSceneID);
        // workaround
        if (controller)
            controller.StartGame();
        else if (Instance)
            MainGameController.Instance.StartGame();
        else
            Debug.Log("Failed to switch scene");
    }
}