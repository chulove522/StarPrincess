using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trophies : MonoBehaviour
{
    public GameObject MainGameController;
    public GameObject[] starsignObj;
    public Image[] stars;
    private bool isAllClear = false;
    private Vector3 showplace = new Vector3(0,0,-50f);
    public float showspeed = 20f;
    //Image starsignImg;

    private void Start() {
        
    }
    public enum Constellations {
        Aries=0, Taurus = 1, Gemini = 2, Cancer = 3, Leo = 4, Virgo = 5,
        Libra = 6, Scorpio = 7, Sagittarius = 8, Capricorn = 9, Aquarius = 10, Pisces = 11
    };
    // Aries=0~Pisces=11
    private bool[] clear = { false, false, false, false, false, false, false, false, false, false, false, false };

    public void setUnlock(int starsignNum) {

        //starsignImg = GameObject.Find(starsign.ToString()).GetComponent<Image>();
        //starsignImg.color = Color.white;
        //GameObject.Find(starsign.ToString()).GetComponent<Image>().color = Color.white;
        //Vector3 nowpos = starsignObj[starsignNum].transform.position;
        //StartCoroutine(MoveTo(starsignObj[starsignNum],nowpos, nowpos+showplace, showspeed));

        //以上廢棄不用

        clear[starsignNum] = true;
        starsignObj[starsignNum].transform.position += showplace;
        stars[starsignNum].color = Color.white;
        Debug.Log("unlock" + starsignNum.ToString());
    }

    void FinishStage(int stage) { 
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
            starsignObj[i].transform.position -= showplace;
            clear[i] = false;
            stars[i].color = Color.gray;
        }
    }
    private IEnumerator MoveTo(GameObject obj, Vector3 currentPos, Vector3 targetPos, float speed) {

        var duration = 1000 / speed;

        var timePassed = 0f;
        while (timePassed < duration) {
            // always a factor between 0 and 1
            var factor = timePassed / duration;

            obj.transform.position = Vector3.Lerp(currentPos, targetPos, factor);

            // increase timePassed with Mathf.Min to avoid overshooting
            timePassed += Math.Min(Time.deltaTime, duration - timePassed);

            // "Pause" the routine here, render this frame and continue
            // from here in the next frame
            yield return null;
        }

        transform.position = targetPos;

        // move done!
    }

}
