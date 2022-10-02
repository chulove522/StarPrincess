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
        controller.StartGame();
    }
}