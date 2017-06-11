using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallSpawner : VerticalSpawner {

  public GameObject[] walls;
  public GameObject[] windows;

  protected override float ItemHeight() {
    var renderer = walls[0].GetComponent<SpriteRenderer>();
    return renderer.sprite.bounds.size.y;
  }

  protected override GameObject Next(string lastTag) {
    var windowOrWall = Random.RandomRange(0f, 1f) > 0.9f ? windows : walls;
    var possibilities = lastTag == "Wall" ? windowOrWall : walls;
    return possibilities[Random.Range(0, possibilities.Length)];
  }
}
