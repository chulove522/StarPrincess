using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Rotate(0, 0, 5);
        if (Target != null)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, Time.time / 100f);
        }
    }
}
