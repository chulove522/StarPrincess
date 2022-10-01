using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSign : MonoBehaviour {
    //public GameObject stellar;  //裝整個星座的主要星星用的
    /// <summary>
    /// 這個東西應該用不著.廢棄
    /// </summary>
    public byte starNum;
    [SerializeField] string Name;
    [SerializeField] static bool _clear;
    

    public enum Constellations {
        Aries = 0, Taurus = 1, Gemini = 2, Cancer = 3, Leo = 4, Virgo = 5,
        Libra = 6, Scorpio = 7, Sagittarius = 8, Capricorn = 9, Aquarius = 10, Pisces = 11
    };


    public bool getClear => _clear;
    public void setClear(bool c) { _clear = c; }


    public void Start() {
        _clear = false;
        Name = CompareStellar.returnname(starNum);
        
        //hideAll();
    }

    public void hideAll() {
        //stellar.SetActive(false);
    }



}
