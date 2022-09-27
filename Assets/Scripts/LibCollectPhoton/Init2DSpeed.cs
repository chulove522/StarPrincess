using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init2DSpeed : MonoBehaviour
{
    public Vector2 initSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
         Rigidbody2D rb = GetComponent<Rigidbody2D>();
         rb.velocity = initSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
