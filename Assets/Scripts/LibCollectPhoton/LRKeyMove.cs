using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRKeyMove : MonoBehaviour
{
    //movement speed in units per second
    public float movementSpeed = 5.0f;
    public float leftBoundary = -10.0f;
    public float rightBoundary = 10.0f;
    void Update()
    {
        //get the Input from Horizontal axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float newX = transform.position.x + horizontalInput * movementSpeed * Time.deltaTime;
        newX = Mathf.Clamp(newX, leftBoundary, rightBoundary);
        //update the position
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
