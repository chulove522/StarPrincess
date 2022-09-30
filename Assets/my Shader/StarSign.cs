using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSign : MonoBehaviour {
    public GameObject stellar;  //裝整個星座的主要星星用的
    //public Text color;
    //public Text size;
    //public Text introductionText;
    public byte starNUm;
    [SerializeField] string Name;
    private bool _clear=false;
    //private bool _isfront = false; //防止星座前進2次導致顯示錯誤

    public enum Constellations {
        Aries = 0, Taurus = 1, Gemini = 2, Cancer = 3, Leo = 4, Virgo = 5,
        Libra = 6, Scorpio = 7, Sagittarius = 8, Capricorn = 9, Aquarius = 10, Pisces = 11
    };

    //public bool isFront => _isfront;
    public bool getClear => _clear;
    public void setClear(bool c) { _clear = c; }
    //public void setFront(bool f) { _isfront = f; }

    public void Start() {

        Name = CompareStellar.returnname(starNUm);
        
        //hideAll();
    }

    public void hideAll() {
        stellar.SetActive(false);
    }



}
