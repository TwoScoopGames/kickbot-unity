using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour {

  public Sprite[] frames;
  public float frameRate = 1f;

  private int currentFrame;
  private float time;
  private Image renderer;

  // Use this for initialization
  void Start () {
    renderer = GetComponent<Image>();
  }

  // Update is called once per frame
  void Update () {
    time += Time.deltaTime;

    int framesToAdvance = (int)(time * frameRate);
    currentFrame = (currentFrame + framesToAdvance) % frames.Length;
    time -= framesToAdvance / frameRate;

    renderer.sprite = frames[currentFrame];
  }
}
