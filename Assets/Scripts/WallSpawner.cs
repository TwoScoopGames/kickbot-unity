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
    var prefab = hazards[Random.Range(0, hazards.Length)];
    var position = instance.transform.position;
    GameObject hazard = Instantiate(prefab, position, Quaternion.identity);
    Debug.Log(hazard.tag);
    return new [] { hazard };
  }
}
