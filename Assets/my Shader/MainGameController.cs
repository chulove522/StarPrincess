﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;
    Trophies t = new Trophies();

    void Start() {
        
        audioSource = this.GetComponent<AudioSource>();
    }
    // Update is called once per frame


    void Update()
    {
        
    }
    void Save(int stagenum) {
        PlayerPrefs.SetInt("Stage",stagenum);
        //PlayerPrefs.GetString("Stage");



    }
    public void clearAll() {

        PlayerPrefs.DeleteAll();
        t.initClear();
    }
    public void Win(int stage) {
        t.FinishStage(stage);
    }
    public void Lose() {

    }
    public void showAward() {
        Debug.Log("you found 3 stars!");
    }
}
