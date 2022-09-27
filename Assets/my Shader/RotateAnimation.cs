using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    private GameObject rotateobj;
    private Sprite image;
    public bool isaBall;
    bool isRoating;
    private Vector3 rotationvector = new Vector3(0f, 0f, 1f); //icon rotation only has Z axes
    void Start()
    {
        rotateobj = gameObject;
        if(isaBall)
            rotationvector = new Vector3(1f, 1f, 1f); //ball rotation has 3 axes

    }
    public void turnoffRotate() {
        isRoating = false;
    }
    public void trunonRotate() {
        isRoating = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isRoating)
            rotateobj.transform.Rotate(rotationvector, 0.5f);
    }
}
