using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBall : MonoBehaviour{
    public GameObject Ball;
    public Material BallMat;
    public Texture2D[] BallTexture;
    private byte TexPointer= 0;
    //public Texture2D SampleTexture;
    public Slider HueSlidebar,SizeSlider;
    public float HueVal;
    public float ScaleVal;
    private void Start() {

    }

    void OnEnable() {
        //Subscribe to the Slider Click event

        SizeSlider.onValueChanged.AddListener(delegate { setSizeCallBack(SizeSlider); });
        HueSlidebar.onValueChanged.AddListener(delegate { setHueColor(); });
    }
    //Will be called when Slider changes
    public void setSizeCallBack(Slider SizeSlider) {
        //Debug.Log("Slider Changed: " + slidebar.value);
        ScaleVal = SizeSlider.value;
        Ball.transform.localScale = new Vector3(4+ScaleVal, 4+ScaleVal, 4+ScaleVal);
    }

    public void setHueColor() {
        HueVal = HueSlidebar.value;
        BallMat.SetFloat("_hue", HueVal); //踩雷 要加底線並且以Material內的refrence name為對象
    }
    public void changeTexture() {

        if (TexPointer < BallTexture.Length) {
            BallMat.SetTexture("_pattern", BallTexture[TexPointer]);
            TexPointer += 1;
 
        }else {
            BallMat.SetTexture("_pattern", BallTexture[0]);
            TexPointer -= (byte)(BallTexture.Length-1);
        }
    }



}
