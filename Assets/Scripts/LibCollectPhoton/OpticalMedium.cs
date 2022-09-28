using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpticalMedium : MonoBehaviour
{
    public float refractiveIndex = 1.0f; // can assign new value from unity UI
    private float invRefractiveIndex;
    void Start()
    {
        // TODO handle divide by zero
        invRefractiveIndex = 1.0f/refractiveIndex;
    }

    void Update()
    {
        
    }

    // Heckbert's method: a fast way to solve Snell's Law
    private Vector2 HeckbertMethod(float refractiveIndexSrc, float refractiveIndexDst, Vector2 incidence, Vector2 normal)
    {
        float n = refractiveIndexSrc / refractiveIndexDst; 
        float c1 = Mathf.Clamp(-Vector2.Dot(incidence, normal), -1.0f, 1.0f);
        float s1 = Mathf.Sqrt(1-c1*c1);
        float squarec2 = Mathf.Max((1.0f - n * n * (1.0f - c1 * c1)), 0.0f);
        float c2 = Mathf.Sqrt(squarec2);
        return n * incidence + (n * c1 - c2) * normal;
    }

    // TODO get normal from trigger event
    private Vector2 centerToCllider(Collider2D other) {
        Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 otherPos = other.transform.position;
        Vector2 normal = (otherPos - pos2D).normalized;
        return normal;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO generalize it
        //  Assume other is always photon, enter from refractiveIndex = 1.0
        //  Assume this object is a circle collider
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        float speed = rb.velocity.magnitude;
        Vector2 dir = HeckbertMethod(1.0f, refractiveIndex, rb.velocity.normalized, centerToCllider(other));
        rb.velocity = speed * refractiveIndex * dir;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        float speed = rb.velocity.magnitude;
        Vector2 dir = HeckbertMethod(refractiveIndex, 1.0f, rb.velocity.normalized, -centerToCllider(other));
        rb.velocity = speed * invRefractiveIndex * dir;
    }
}
