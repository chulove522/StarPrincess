using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPhotonScore : MonoBehaviour
{
    public int numberCollected = 0;
    public int scoreMultiple = 100;

    public int GetScore() {
        return numberCollected * scoreMultiple;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "PhotonGetScore") {
            Destroy(other);
            numberCollected++;
            // TODO remove this log after integeration
            Debug.Log("socre is " + GetScore());
        }
    }
}
