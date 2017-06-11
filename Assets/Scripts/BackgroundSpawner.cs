using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundSpawner : VerticalSpawner {

  public GameObject[] prefabs;

  protected override float ItemHeight() {
    var renderer = prefabs[0].GetComponent<SpriteRenderer>();
    return renderer.sprite.bounds.size.y;
  }

  protected override GameObject Next(string lastTag) {
    return prefabs[Random.Range(0, prefabs.Length)];
  }
}
