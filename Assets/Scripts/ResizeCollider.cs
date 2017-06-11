using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeCollider : MonoBehaviour {

  private Camera camera;
  private BoxCollider2D collider;

  // Use this for initialization
  void Start () {
    camera = GetComponent<Camera>();
    collider = GetComponent<BoxCollider2D>();
  }

  // Update is called once per frame
  void Update () {
    var cameraHeight = 2f * camera.orthographicSize;
    var cameraWidth = cameraHeight * camera.aspect;

    var size = collider.size;
    size.x = cameraWidth;
    size.y = cameraHeight;
    collider.size = size;
  }
}
