using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acceleration2D : MonoBehaviour {

  public Vector3 acceleration;

  private Movement2D movement;

  // Use this for initialization
  void Start () {
    movement = GetComponent<Movement2D>();
  }

  // Update is called once per frame
  void Update () {
    movement.velocity += acceleration * Time.deltaTime;
  }
}
