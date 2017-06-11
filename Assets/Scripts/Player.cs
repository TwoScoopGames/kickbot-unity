using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  private Animator animator;
  private Collider2D collider;
  private Movement2D movement;
  private SpriteRenderer renderer;

  private bool onWall = true;
  private float leftJumpTime = -1;
  private float rightJumpTime = -1;

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();
    collider = GetComponent<Collider2D>();
    movement = GetComponent<Movement2D>();
    renderer = GetComponent<SpriteRenderer>();
  }

  private float Oscillate(float current, float period) {
    return Mathf.Sin(current / period * Mathf.PI);
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (onWall) {
      return;
    }
    if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Window") {
      movement.velocity.x = 0;
      movement.velocity.y = 0;
      onWall = true;
      leftJumpTime = -1;
      rightJumpTime = -1;

      animator.SetTrigger("Land");

      var wallIsOnLeft = transform.position.x < 0;
      if (wallIsOnLeft) {
        var v = transform.position;
        v.x = other.transform.position.x +
          (other.bounds.size.x / 2f) +
          (collider.bounds.size.x / 2f);
        transform.position = v;

        renderer.flipX = false;
      } else {
        var v = transform.position;
        v.x = other.transform.position.x -
          (other.bounds.size.x / 2f) -
          (collider.bounds.size.x / 2f);
        transform.position = v;

        renderer.flipX = true;
      }
    }
  }

  // Update is called once per frame
  void Update () {
    var axis = Input.GetAxisRaw("Horizontal");
    var left = axis < 0;
    var right = axis > 0;

    if (!onWall) {
      if (leftJumpTime >= 0 && leftJumpTime <= 0.2f) {
        leftJumpTime += Time.deltaTime;
        movement.velocity.x = Oscillate(leftJumpTime + 0.1f, 0.2f) * 1000f / 3f;
      }
      if (rightJumpTime >= 0 && rightJumpTime <= 0.2f) {
        rightJumpTime += Time.deltaTime;
        movement.velocity.x = -Oscillate(rightJumpTime + 0.1f, 0.2f) * 1000f / 3f;
      }
      return;
    }

    var wallIsOnLeft = transform.position.x < 0;

    if (left || right) {
      GameManager.instance.StartGame();
      movement.velocity.y = 500;
      onWall = false;
      // jump sound
      animator.SetTrigger("Jump");
    }
    if (left) {
      if (wallIsOnLeft) {
        // sin wave bullshit
        leftJumpTime = 0;
      } else {
        movement.velocity.x = -333.333f;
      }
    } else if (right) {
      if (wallIsOnLeft) {
        movement.velocity.x = 333.333f;
      } else {
        // sine wave bullshit
        rightJumpTime = 0;
      }
    }
  }
}
