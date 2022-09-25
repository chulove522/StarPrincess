
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trophes : MonoBehaviour
{
    public GameObject MainGameController;
    public GameObject[] starsignObj;
    private bool isAllClear = false;
    private Vector3 showplace = new Vector3(0,0,-1000f);
    public float showspeed = 20f;
    //Image starsignImg;

    private void Start() {
        
    }
    public enum Constellations {
        Aries=0, Taurus = 0, Gemini = 0, Cancer = 0, Leo = 0, Virgo = 0,
        Libra = 0, Scorpio = 0, Sagittarius = 0, Capricorn = 0, Aquarius = 0, Pisces = 0
    };
    // Aries=0~Pisces=11

    public void setUnlock(int starsignNum) {

        //starsignImg = GameObject.Find(starsign.ToString()).GetComponent<Image>();
        //starsignImg.color = Color.white;
        //GameObject.Find(starsign.ToString()).GetComponent<Image>().color = Color.white;\
        Vector3 nowpos = starsignObj[starsignNum].transform.position;
        StartCoroutine(MoveTo(nowpos, nowpos+showplace, showspeed));

        //starsignObj[starsignNum].transform.Translate(nowpos + showplace);
        Debug.Log("unlock" + starsignNum.ToString());


        
        foreach (Constellations sign in Enum.GetValues(typeof(Constellations))) {
            if(sign == 0) {
                break;
            }
        }
        // if didn't break
        setAllClear();
    }

    //for judges!! fast clear to see the result
    public void setAllClear() {
        for (int i = 0; i < 11; i++) {
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
    }
    private IEnumerator MoveTo(Vector3 currentPos, Vector3 targetPos, float speed) {

        var duration = 1000 / speed;

        var timePassed = 0f;
        while (timePassed < duration) {
            // always a factor between 0 and 1
            var factor = timePassed / duration;

            transform.position = Vector3.Lerp(currentPos, targetPos, factor);

            // increase timePassed with Mathf.Min to avoid overshooting
            timePassed += Math.Min(Time.deltaTime, duration - timePassed);

            // "Pause" the routine here, render this frame and continue
            // from here in the next frame
            yield return null;
        }

        transform.position = targetPos;

        // Something you want to do when moving is done
    }
}
