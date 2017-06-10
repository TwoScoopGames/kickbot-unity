using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {

  public GameObject[] prefabs;

  private float cameraWidth;
  private float cameraHeight;
  private List<GameObject> items = new List<GameObject>();

  // Use this for initialization
  void Start() {
    SetCameraDimensions();
  }

  // Update is called once per frame
  void Update() {
    SetCameraDimensions();

    var prefab = prefabs[Random.Range(0, prefabs.Length)];
    var renderer = prefab.GetComponent<SpriteRenderer>();

    var top = Camera.main.transform.position.y + (cameraHeight / 2f);

    var bottomItem = items.LastOrDefault();
    if (bottomItem) {
      top = bottomItem.transform.position.y - renderer.sprite.bounds.size.y;
    }

    var bottom = Camera.main.transform.position.y - (cameraHeight / 2f);
    for (var y = top; y > bottom; y -= renderer.sprite.bounds.size.y) {
      GameObject instance = Instantiate(prefab, new Vector3(0, y, 0f), Quaternion.identity);
      items.Add(instance);
    }
  }

  private void SetCameraDimensions() {
    Camera camera = Camera.main;
    cameraHeight = 2f * camera.orthographicSize;
    cameraWidth = cameraHeight * camera.aspect;
  }
}
