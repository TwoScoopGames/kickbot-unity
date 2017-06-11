using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour {


  private AudioSource music;
  bool isPlaying;
  public static StartMusic instance;



  void Awake () {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }


	void Start () {
    music = GetComponent<AudioSource>();
    isPlaying = false;
	}


	public void Play () {

    if (!isPlaying) {
      isPlaying = true;
      music.Play();
    }


	}
}
