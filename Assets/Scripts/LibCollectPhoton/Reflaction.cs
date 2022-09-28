using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflaction : MonoBehaviour
{
    //Assume Enter and Exit the same Medim
    //  TODO: save current reflactive index
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
    }

    private void OnTriggerExit(Collider other)
    {

    }
}
