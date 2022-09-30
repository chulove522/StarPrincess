using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Trophies : MonoBehaviour
{
    StarSign[] starsigninfo;
    public GameObject[] starsignObj;
    public Image[] stars;
    private static bool isAllClear = false;
    private Vector3 showplace = new Vector3(0,0,-50f);
    public float showspeed = 20f;

    
    //Image starsignImg;

    private void Start() {
        for (int i = 0; i < 12; i++) {
            starsigninfo[i] = starsignObj[i].GetComponent<StarSign>();
        }
    }

    // Aries=0~Pisces=11
    //private bool[] clear = { false, false, false, false, false, false, false, false, false, false, false, false };

    void setUnlock(int starsignNum) {

        //starsignImg = GameObject.Find(starsign.ToString()).GetComponent<Image>();
        //starsignImg.color = Color.white;
        //GameObject.Find(starsign.ToString()).GetComponent<Image>().color = Color.white;
        //Vector3 nowpos = starsignObj[starsignNum].transform.position;
        //StartCoroutine(MoveTo(starsignObj[starsignNum],nowpos, nowpos+showplace, showspeed));

        //以上廢棄不用

        if(starsigninfo[starsignNum].getClear == false) {
            starsigninfo[starsignNum].setClear(true);
            starsignObj[starsignNum].transform.position += showplace;
            stars[starsignNum].color = Color.white;
        }

        Debug.Log("unlock" + starsignNum.ToString());
    }

    public void FinishStage(int stage) { 
        if (stage ==1) { //fire
            setUnlock(0);
            setUnlock(4);
            setUnlock(8);

        }
        else if(stage == 2) { //soil
            setUnlock(1);
            setUnlock(5);
            setUnlock(9);


        }
        else if(stage == 3) { //water
            setUnlock(3);
            setUnlock(7);
            setUnlock(11);

        }
        else if (stage == 4) { //wind
            setUnlock(2);
            setUnlock(6);
            setUnlock(10);
            isAllClear = true;
            setTheFire();
        }
    }

    //for judges!! fast clear to see the result
    public void setAllClear() {
        for (int i = 0; i < 12; i++) {
            setUnlock(i);
        }
        isAllClear = true;
        setTheFire();
    }

    //fireworks!s
    public void setTheFire() { 
        //allclear scripts
        if(isAllClear == true) {
            Debug.Log("全通關");
        }

    }
    public void initClear() {
        isAllClear = false;
        for (int i = 0; i < 12; i++) {
            if (starsigninfo[i].getClear == true) {
                //如果人在前面救移回來
                starsignObj[i].transform.position -= showplace;
                starsigninfo[i].setClear(false);
            }


            stars[i].color = Color.gray;
        }
    }
    

}
