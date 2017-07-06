using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : VerticalSpawner {

  public GameObject[] prefabs;

  protected override float ItemHeight() {
    var renderer = prefabs[0].GetComponent<SpriteRenderer>();
    return renderer.sprite.bounds.size.y;
  }

  protected override IList<GameObject> OnSpawn(string lastTag, Vector3 position) {
    var prefab = prefabs[Random.Range(0, prefabs.Length)];
    var instance = Instantiate(prefab, position, Quaternion.identity);
    var items = new List<GameObject>();
    items.Add(instance);
    return items;
  }
}
