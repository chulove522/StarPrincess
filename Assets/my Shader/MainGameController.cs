using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameController : MonoBehaviour {
    [SerializeField]
    private AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start() {
        audioSource = this.GetComponent<AudioSource>();
    }
    // Update is called once per frame


    void Update()
    {
        
    }
}
