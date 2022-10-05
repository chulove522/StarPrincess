using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Trophies : MonoBehaviour
{
    /*這些都只是為了畫面顯示 沒有真正存檔的用途*/
    static int stageCleared;
    public GameObject[] starsignObj;
    public Image[] stars; //裝黃色 星星
    //private static bool isAllClear = false;  
    private static bool[] starCleared={false, false, false, false, false, false, false, false, false, false, false, false};
    private Vector3 showplace = new Vector3(0,0,-50f);
    public float showspeed = 20f;

    
    //Image starsignImg;

    private void Start() {
        /*呼叫存檔*/
        stageCleared = PlayerPrefs.GetInt("Stage", 0); //1~4 0:new

        /*
        if (stageCleared == 4)
            isAllClear = true;
        else
            isAllClear = false;

        */
        /*打開版面即刷新畫面*/
        FinishStage();

    }


    //在每次破關後都呼叫main.save存起來歐!

    // Aries=0~Pisces=11

    void setUnlock(int starsignNum) {

        //在trophies panel 中. 如果是主畫面.可以看到星座 . 在主遊戲中使用panel並沒有星座圖示.只有星星 
        //如果不是主畫面. obj 拉12個fake代替.

        if (starCleared[starsignNum] == false)
            starsignObj[starsignNum].transform.position += showplace;

        stars[starsignNum].color = Color.white;

        starCleared[starsignNum] = true;

        Debug.Log("unlock" + starsignNum.ToString());
    }
    void resetUnlock(int starsignNum) {

        if(starCleared[starsignNum] == true)
            starsignObj[starsignNum].transform.position -= showplace;

        stars[starsignNum].color = Color.gray;

        starCleared[starsignNum] = false;

    }
    /*每次開啟面板都呼叫他!*/
    public void FinishStage() {
        if (stageCleared >= 1) { //fire
            setUnlock(0);
            setUnlock(4);
            setUnlock(8);

        }
        if(stageCleared >= 2) { //soil
            setUnlock(1);
            setUnlock(5);
            setUnlock(9);


        }
        if(stageCleared >= 3) { //water
            setUnlock(3);
            setUnlock(7);
            setUnlock(11);

        }
        if (stageCleared == 4) { //wind
            setUnlock(2);
            setUnlock(6);
            setUnlock(10);
            //isAllClear = true;
            setTheFire();
        }

    }

    //for judges!! fast clear to see the result
    public void setAllClear() {
        for (int i = 0; i < 12; i++)
            setUnlock(i);

        //isAllClear = true;
        setTheFire();

    }

    //fireworks!s
    public void setTheFire() { 

        Debug.Log("全通關");
    }
    public void initClear() {

        for (int i = 0; i < 12; i++) {
            resetUnlock(i);
        }

        //isAllClear = false;
        
    }

    public void clearAllDangerous() {
        /*uiux裝兩層*/
        /*跳出警告視窗是否重置一切存檔*/
        initClear(); 
        MainGameController.clearAllstatic();

    }


    /*遊戲通關呼叫他*/
    public void showAward() {

        FinishStage();
        gameObject.SetActive(true);

        Debug.Log("you found 3 stars!");
    }

}
