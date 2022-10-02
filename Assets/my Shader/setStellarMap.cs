using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setStellarMap : MonoBehaviour
{
    public Material[] mat;  //3kinds
    //Material ballmat;  //self
    //float Hue;
    int matNum;
    void start() {
        //Hue = CompareStellar.getHue;
        matNum = CompareStellar.getBallMapNumber;

        this.gameObject.GetComponent<Renderer>().material = mat[matNum];
        //ballmat 

    }


}
