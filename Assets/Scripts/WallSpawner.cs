using Random = UnityEngine.Random;
using UnityEngine;

public class WallSpawner : VerticalSpawner {

  public GameObject[] hazards;
  public GameObject[] walls;
  public GameObject[] windows;

  protected override float ItemHeight() {
    var renderer = walls[0].GetComponent<SpriteRenderer>();
    return renderer.sprite.bounds.size.y;
  }

  protected override GameObject Next(string lastTag) {
    var windowOrWall = Random.Range(0f, 1f) > 0.9f ? windows : walls;
    var possibilities = lastTag == "Wall" ? windowOrWall : walls;
    return possibilities[Random.Range(0, possibilities.Length)];
  }

  protected override GameObject[] OnSpawn(GameObject instance) {
    var position = instance.transform.position;

    var side = position.x > 0 ? "Right" : "Left";
    if (GameManager.instance.GetHazard() != side) {
      return new GameObject[0];
    }

    var prefab = hazards[Random.Range(0, hazards.Length)];
    var prefabRenderer = prefab.GetComponent<SpriteRenderer>();

    var instanceRenderer = instance.GetComponent<SpriteRenderer>();
    var sign = position.x > 0 ? -1 : 1;
    position.x += sign * ((instanceRenderer.bounds.size.x / 2) + (prefabRenderer.bounds.size.x / 2));

    GameObject hazard = Instantiate(prefab, position, Quaternion.identity);
    return new [] { hazard };
  }
}
