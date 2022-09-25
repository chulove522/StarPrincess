using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    private GameObject rotateobj;
    private Sprite image;
    public bool isaBall;
    private Vector3 rotationvector = new Vector3(0f, 0f, 1f); //icon rotation only has Z axes
    void Start()
    {
        rotateobj = gameObject;
        if(isaBall)
            rotationvector = new Vector3(1f, 1f, 1f); //ball rotation has 3 axes

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotateobj.transform.Rotate(rotationvector, 0.5f);
    }
}
