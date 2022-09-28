using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    public Game2 Game;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet") 
        {
            Destroy(coll.gameObject);
            Game.minusLife();
            Game.showLife();
        }
    }
}
