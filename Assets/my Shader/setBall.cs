using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static NasaScript.DIALOG_ID;
using static MainGameController.SCENE_ID;

public class setBall : MonoBehaviour{
    public GameObject[] Ball;
    public Material BallMat;
    public Texture2D[] BallTexture;
    private byte TexPointer= 0;
    private byte BallPointer=0;
    public Slider HueSlidebar,SizeSlider;
    [SerializeField] 
    float HueVal=0;
    float ScaleVal=0;
    public Vector3[] position;
    new Renderer renderer;

    private void Start() {
       /* for (int i = 0; i < positionObject.Length; i++) {
            position[i] = positionObject[i].transform.position;
        
        }*/
    }

    void OnEnable() {
        //Subscribe to the Slider Click event

        SizeSlider.onValueChanged.AddListener(delegate { setSizeCallBack(SizeSlider); });
        HueSlidebar.onValueChanged.AddListener(delegate { setHueColor(); });
    }




    public void changeBall(int sample) {
        //alpha star
        if (sample == 0 ) {
            BallPointer = 1;
            StartCoroutine(MoveTo(Ball[1], Ball[1].transform.position, position[0], 20f));
            StartCoroutine(MoveTo(Ball[0], Ball[0].transform.position, position[2], 20f));
            StartCoroutine(MoveTo(Ball[2], Ball[2].transform.position, position[1], 20f));
        } //beta star
        else if (sample == 1 ) {
            BallPointer = 0;
            StartCoroutine(MoveTo(Ball[0], Ball[0].transform.position, position[0], 20f));
            StartCoroutine(MoveTo(Ball[2], Ball[2].transform.position, position[2], 20f));
            StartCoroutine(MoveTo(Ball[1], Ball[1].transform.position, position[1], 20f));

        }//theta star
        else if (sample == 2) {
            BallPointer = 2;
            StartCoroutine(MoveTo(Ball[2], Ball[2].transform.position, position[0], 20f));
            StartCoroutine(MoveTo(Ball[1], Ball[1].transform.position, position[2], 20f));
            StartCoroutine(MoveTo(Ball[0], Ball[0].transform.position, position[1], 20f));
        }
        /*
        else {
            BallPointer -= (byte)(Ball.Length-1);

        }*/
        
        renderer = Ball[BallPointer].GetComponent<Renderer>();
        BallMat = renderer.GetComponent<Renderer>().material;

    }
    public void setSizeCallBack(Slider SizeSlider) {
        //Debug.Log("Slider Changed: " + slidebar.value);
        ScaleVal = SizeSlider.value;
        Ball[BallPointer].transform.localScale = new Vector3(2+ScaleVal, 2+ScaleVal, 2+ScaleVal);
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

    private IEnumerator MoveTo(GameObject obj, Vector3 currentPos, Vector3 targetPos, float speed) {


        var duration = 20 / speed;

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

        obj.transform.position = targetPos;

        // move done!
    }

    public void FinishPlanet() {
        /*
        //提供給場景轉換時呼叫出那顆星球 顏色
        public float getColor => HueVal;
        //材質是第幾個
        public byte GetBallMatByte => BallPointer;
        */
        MainGameController.Instance.Win(SPACE_SCENE);  //只有從製作星球到主對話是這個數字
        // TODO: correct dialog ID
        MainGameController.setDialog(BOOSS_TALK_01);
        PlayerPrefs.SetFloat("PlanetColor", HueVal);
        //PlayerPrefs.SetInt("PlanetColor", (int)HueVal);
        PlayerPrefs.SetFloat("PlanetSize", ScaleVal);
        PlayerPrefs.SetInt("PlanetMat", BallPointer);

    }

}
