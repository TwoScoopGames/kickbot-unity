using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : VerticalSpawner {

  public GameObject[] hazards;
  public GameObject[] walls;
  public GameObject[] windows;

  protected override float ItemHeight() {
    var renderer = walls[0].GetComponent<SpriteRenderer>();
    return renderer.sprite.bounds.size.y;
  }

  protected override IList<GameObject> OnSpawn(string lastTag, Vector3 position) {
    var instances = new List<GameObject>();

    var side = position.x > 0 ? "Right" : "Left";
    var hasHazard = GameManager.instance.GetHazard() == side;

    var windowOrWall = Random.Range(0f, 1f) > 0.9f ? windows : walls;
    var possibilities = lastTag == "Wall" ? windowOrWall : walls;
    var prefab = possibilities[Random.Range(0, possibilities.Length)];
    var instance = Instantiate(prefab, position, Quaternion.identity);
    instances.Add(instance);

    if (hasHazard) {
      var hazard = hazards[Random.Range(0, hazards.Length)];
      var hazardRenderer = hazard.GetComponent<SpriteRenderer>();

      var instanceRenderer = instance.GetComponent<SpriteRenderer>();
      var sign = position.x > 0 ? -1 : 1;
      position.x += sign * ((instanceRenderer.bounds.size.x / 2) + (hazardRenderer.bounds.size.x / 2)) - (2 * sign);

      instances.Add(Instantiate(hazard, position, Quaternion.identity));
    }
    return instances;
  }
}
