using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGenerator : MonoBehaviour {
    // shot photon to the player
    public GameObject player;
    public GameObject photon;
    public float waitForSeconds = 1.0f;
    public float photonSpeed = 5.0f;
    public bool enableShot = false;

    void GenPhoton() {
        Vector3 dirToPlayer3 = player.transform.position - transform.position;
        Vector2 dirToPlayer2 = (new Vector2(dirToPlayer3.x, dirToPlayer3.y)).normalized;
        
        GameObject newPhoton = Instantiate(photon, transform.position, Quaternion.identity);
        newPhoton.GetComponent<Rigidbody2D>().velocity = photonSpeed * dirToPlayer2;
    }

    IEnumerator LoopGenPhoton() {
        //Declare a yield instruction.
        WaitForSeconds wait = new WaitForSeconds(waitForSeconds);
        while (enableShot) {
            GenPhoton();
            yield return wait; // pause
        }
    }

    void Start() {
    }

    void SetEnableGenPhoton(bool enable) {
        enableShot = enable;
        if (enableShot) {
            // ref: https://forum.unity.com/threads/loop-with-a-timer.696965/
            StartCoroutine(LoopGenPhoton());
        }
    }
    /*
   放button 上*/
    public void StartGame01() {
        
        SetEnableGenPhoton(true);
    }
    /*
   放button 上*/
    public void endGame01() {
        SetEnableGenPhoton(false);

    }
}
