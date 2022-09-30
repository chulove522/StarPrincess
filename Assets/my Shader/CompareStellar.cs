using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CompareStellar : MonoBehaviour {
    private float HueVal, ScaleVal;
    private int BallPointer;
    //Canvas canvas;

    [SerializeField] static int result;

    /*public*/
    public Text starNameText;   //the title
    public Text planetNameTextChinese;
    public Text planetNameTextEnglish;
    public Text planetIntroText;
    //public Text planetSpectrumText;
    public Text planetMassText;
    public Text planetColorText;
    public Text planetTemperatureText;
    public Text planetDistanceText;

    /*stars*/
    public GameObject[] starSigns;
    public Material[] mat;
    public GameObject theStarYoumade;


    static readonly Dictionary<int, string> starlist = new Dictionary<int, string>(){
        {0, "Aries"},
        {1, "Taurus"},
        {2, "Gemini" },
        {3, "Cancer" },
        {4, "Leo" },
        {5, "Virgo" },
        {6, "Libra" },
        {7, "Scorpio" },
        {8, "Sagittarius" },
        {9, "Capricorn"},
        {10, "Aquarius"},
        {11, "Pisces" }
        
    }; //星座代號查詢
/*
 Aries = 0, Taurus = 1, Gemini = 2, Cancer = 3, Leo = 4, Virgo = 5,
    Libra = 6, Scorpio = 7, Sagittarius = 8, Capricorn = 9, Aquarius = 10, Pisces = 11
*/

    static string[] ChineseName = {"α Ari 婁宿三",
    "畢宿五",
    "北河三",
    "柳宿增十β",
    "軒轅十四",
    "角宿一",
    "氐宿四",
    "心宿二",
    "箕宿三",
    "壘壁陣四",
    "虛宿一",
    "外屏七", };
    static string[] EnName = {
    "Hamal， Hemal， Hamul， Ras Hammel， El Nath， Arietis",
    "K5III",
    "Al-Ras al-Taum al-Mu'ahar, Muekher al Dzira, Posterior Brachii",
    "K4III",
    "Cor Leonis, Qalb, Kabelaced, Qalb al-Asad, Rex",
    "B1V",
    "Zubenelgenubi, Zuben Elgenubi, Kiffa Australis, Elkhiffa australis",
    "Cor Scorpii, Qalb al-Aqrab, Vespertilo",
    "Kaus Australis",
    "A5mF2 (IV)",
    "G0Ib",
    "Alpherg, Kullat-Nunu",
    };
    static string[] Color =
    {
        "Light Orange橙色色調輝光",
        "Orange 橙色",
        "Orange 橙色",
        "Orange 橘色",
        "White 白色",
        "Blue 藍巨星",
        "Green! 唯一的綠色!",
        "Red 紅超巨星",
        "Blue white藍白",
        "White 白巨星",
        "Yellow 黃色色調",
        "Yellow 黃色"
    };
    static string[] Temperature = {
        "4480 K",
        "3900 K",
        "4666 K ",
        "4039 K",
        "10300 K",
        "22400 K",
        "12300 K",
        "3400 K",
        "9800 K",
        "7700 K",
        "5700 K",
        "4930 K",
    };
    static string[] Mass =
    {
        "1.5 M☉",
        "1.16 M☉",
        "1.91 M☉",
        "2 M☉",
        "3.5 M☉",
        "10.25 M☉",
        "3.5 M☉",
        "15.5 M☉",
        "4.4 M☉",
        "1 M☉",
        "6.0–6.5 M☉",
        "2.3"
    };
    static string[] Distance =
    {
      "66",
      "-0.63",
      "34",
      "-1.22",
      "77",
      "-3.55",
      "77",
      "604",
      "145",
      "2.49",
      "-3.47",
      "294"
    };
    static string[] intro =
    {
        "視星等為2.0。他是一顆巨星",
        "紅巨星比太陽更大",
        "它是最接近太陽的巨星",
        "又稱Al Tarf",
        "英文名Regulus，也是全天空二十五顆最明亮的恆星之一，為第21亮的恆星",
        "是全天空第十六亮的恆星",
        "這種質量大的恆星通常有著藍白色的顏色，但氐宿四的顏色卻是綠色的，令到這顆恆星成為唯一一顆肉眼可見的綠色恆星",
        "夜空中第14亮的星",
        "已經脫離了主序星階段，逐漸向著巨星演化",
        "是摩羯座的四合星系統，距地球大約39光年",
        "視星等2.90等的超巨星，距離990光年",
        "在望遠鏡中可以看到一顆呈黃色，一顆呈藍色"

    };


    private void Start() {

        //canvas = this.transform.GetComponent<Canvas>();
        HueVal = PlayerPrefs.GetFloat("PlanetColor", 0);
        ScaleVal = PlayerPrefs.GetFloat("PlanetSize", 0);
        BallPointer = PlayerPrefs.GetInt("PlanetMat", 1);


        Debug.Log("hue = "+HueVal); Debug.Log(ScaleVal); Debug.Log(BallPointer);
        theStarYoumade.GetComponent<Renderer>().material = mat[BallPointer];

        ShowCompareScreen();
    }
    public static string returnname(int num) {
        return starlist[num];
    }

    int compare() {
        if (HueVal < 0.1) {  //red 1個
            result = 7;
        }
        else if (HueVal < 0.4) { //orange 4個
            if(ScaleVal<0.2)
                result = 1;
            else if (ScaleVal < 0.2)
                result = 0;
            else if (ScaleVal < 0.6)
                result = 2;
            else
                result = 3;
        }
        else if (HueVal < 0.61) {  //yellow 2個
            if (ScaleVal < 0.5)
                result = 11;
            else
                result = 10;

        }
        else if (HueVal < 0.7) {  //white 2個
            if (ScaleVal < 0.5)
                result = 9;
            else
                result = 4;
        }
        else if (HueVal < 0.75) {  //green
            result = 6;
        }
        else if (HueVal < 0.8) {  //blue white
                result = 8;
        }
        else if (HueVal <= 1) { //blue
            result = 5;
        }


        return result;
    }
    void showResult(int result) {
        for (int i = 0; i < 12; i++) {
            starSigns[i].SetActive(false);
        }
        starSigns[result].SetActive(true);
        starNameText.text = starlist[result];
        planetNameTextChinese.text = ChineseName[result];
        planetNameTextEnglish.text = EnName[result];
        planetMassText.text = Mass[result];
        planetColorText.text = Color[result];
        planetTemperatureText.text = Temperature[result];
        planetDistanceText.text = Distance[result];
        planetIntroText.text = intro[result];
    }
    public void ShowCompareScreen() {
        compare();
        showResult(result);
    }
    public void ShowOriginal() {
        compare();
        showResult(result);

    }
    public void ShowLeft() {
        if (result == 0)
            result += 11;
        else
            result -= 1;
        showResult(result);

    }
    public void ShowRight() {
        if (result == 11)
            result -= 11;
        else
            result += 1;
        showResult(result);

    }

    //刪乾淨,應該暫時用不到他
    public void ResetPlanet() {
        PlayerPrefs.DeleteKey("PlanetColor");
        PlayerPrefs.DeleteKey("PlaneSize");
        PlayerPrefs.DeleteKey("PlanetMat");
    }
}
