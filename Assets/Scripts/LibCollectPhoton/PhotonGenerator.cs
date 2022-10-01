﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonGenerator : MonoBehaviour
{
    // shot photon to the player
    public GameObject player;
    public GameObject photon;
    public float waitForSeconds = 1.0f;
    public float photonSpeed = 5.0f;

    void GenPhoton() {
        Vector3 dirToPlayer3 = player.transform.position - transform.position;
        Vector2 dirToPlayer2 = (new Vector2(dirToPlayer3.x, dirToPlayer3.y)).normalized;
        // ref: https://answers.unity.com/questions/808262/how-to-instantiate-a-prefab-with-initial-velocity.html
        GameObject newPhoton = Instantiate(photon, transform.position, Quaternion.identity);
        newPhoton.GetComponent<Rigidbody2D>().velocity = photonSpeed * dirToPlayer2;
    }

   IEnumerator LoopGenPhoton() {
      //Declare a yield instruction.
      WaitForSeconds wait = new WaitForSeconds(waitForSeconds);
      while (true) {
         GenPhoton();
         yield return wait; // pause
      }
   }

    void Start()
    {
        // ref: https://forum.unity.com/threads/loop-with-a-timer.696965/
        StartCoroutine(LoopGenPhoton());
    }
}