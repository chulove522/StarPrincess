using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3 settings = new Vector3(1,0,0);

    // Update is called once per frame
    void Update()
    {
        this.transform.position += settings;
    }
}
