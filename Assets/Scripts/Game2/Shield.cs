using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Game2 game;
    
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Bullet") 
        {
            Destroy(coll.gameObject);
            game.addScore();
            game.showscore();
        }
    }
}
