using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallSpawner : MonoBehaviour {

  public GameObject[] walls;
  public GameObject[] windows;

  private float cameraWidth;
  private float cameraHeight;
  private float wallHeight;
  private List<GameObject> items = new List<GameObject>();

  // Use this for initialization
  void Start() {
    SetCameraDimensions();

    var renderer = walls[0].GetComponent<SpriteRenderer>();
    wallHeight = renderer.sprite.bounds.size.y;
  }

  // Update is called once per frame
  void Update() {
    SetCameraDimensions();

    var cameraTop = Camera.main.transform.position.y + (cameraHeight / 2f) + (wallHeight / 2f);
    var top = cameraTop;

    var bottomItem = items.LastOrDefault();
    var bottomItemTag = "Wall";
    if (bottomItem) {
      top = bottomItem.transform.position.y - wallHeight;
      bottomItemTag = bottomItem.tag;
    }

    var bottom = Camera.main.transform.position.y - (cameraHeight / 2f) - (wallHeight / 2f);

    for (var y = top; y > bottom; y -= wallHeight) {
      var windowOrWall = Random.RandomRange(0f, 1f) > 0.9f ? windows : walls;
      var possibilities = bottomItemTag == "Wall" ? windowOrWall : walls;
      var prefab = possibilities[Random.Range(0, possibilities.Length)];
      GameObject instance = Instantiate(prefab, new Vector3(transform.position.x, y, 0f), Quaternion.identity);
      items.Add(instance);
    }

    for (var i = items.Count - 1; i >= 0; i--) {
      var item = items[i];
      if (item.transform.position.y > cameraTop) {
        items.RemoveAt(i);
        Destroy(item);
      }
    }
  }

  private void SetCameraDimensions() {
    Camera camera = Camera.main;
    cameraHeight = 2f * camera.orthographicSize;
    cameraWidth = cameraHeight * camera.aspect;
  }
}
