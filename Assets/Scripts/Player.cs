using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private bool onWall = true;
  private Movement2D movement;

  // Use this for initialization
  void Start () {
    movement = GetComponent<Movement2D>();
  }

  // Update is called once per frame
  void Update () {
    var axis = Input.GetAxisRaw("Horizontal");
    var left = axis < 0;
    var right = axis > 0;

    if (!onWall) {
      return;
    }

    if (left) {
      movement.velocity.x = -40;
    } else if (right) {
      movement.velocity.x = 40;
    }

    var wallIsOnLeft = transform.position.x < 0;

    if (left) {
      if (wallIsOnLeft) {
        // sin wave bullshit
      } else {
        movement.velocity.x = -333.333f;
      }
      movement.velocity.y = 500;
      onWall = false;
      // jump sound
    } else if (right) {
      if (wallIsOnLeft) {
        movement.velocity.x = 333.333f;
      } else {
        // sine wave bullshit
      }
      movement.velocity.y = 500;
      onWall = false;
      // jump sound
    }
  }
}
