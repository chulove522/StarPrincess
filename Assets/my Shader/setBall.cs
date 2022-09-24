using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setBall : MonoBehaviour{
    public GameObject Ball;
    public Material HueMat;
    private Slider slidebar;
    public float HueVal;
    public float ScaleVal;

    void Start()
    {
        slidebar = this.gameObject.GetComponent<Slider>();
    }
    void OnEnable() {
        //Subscribe to the Slider Click event
        slidebar.onValueChanged.AddListener(delegate { setSizeCallBack(slidebar); });
    }
    //Will be called when Slider changes
    public void setSizeCallBack(Slider slidebar) {
        //Debug.Log("Slider Changed: " + slidebar.value);
        ScaleVal = slidebar.value;
        Ball.transform.localScale = new Vector3(4+ScaleVal, 4+ScaleVal, 4+ScaleVal);
    }

    public void setHueColor() {
        HueVal = slidebar.value;
        HueMat.SetFloat("_hue", HueVal); //踩雷 要加底線並且以Material內的refrence name為對象
    }

    public void setSize() {
    }
    public void setBrightness() {
    }

    public void setBasicColor() {
    }


}
