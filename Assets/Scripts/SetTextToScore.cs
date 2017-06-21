﻿using UnityEngine;
using UnityEngine.UI;

public class SetTextToScore : MonoBehaviour {

  private Text text;

  // Use this for initialization
  void Start () {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update () {
    text.text = GameManager.instance.GetScore().ToString();
  }
}
