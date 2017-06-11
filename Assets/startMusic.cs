using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMusic : MonoBehaviour {
  AudioSource music;
  bool isPlaying;

	// Use this for initialization
	void Start () {
    music = GetComponent<AudioSource>();
    isPlaying = false;
	}

	// Update is called once per frame
	void Update () {

    var axis = Input.GetAxisRaw("Horizontal");
    var left = axis < 0;
    var right = axis > 0;


    if (left || right) {
      if (!isPlaying) {
        isPlaying = true;
        music.Play();
      }
    }

	}
}
