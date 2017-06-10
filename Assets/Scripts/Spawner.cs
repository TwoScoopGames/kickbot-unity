using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour {

  public GameObject[] prefabs;

  private float cameraWidth;
  private float cameraHeight;
  private float itemHeight;
  private List<GameObject> items = new List<GameObject>();

  // Use this for initialization
  void Start() {
    SetCameraDimensions();

    var renderer = prefabs[0].GetComponent<SpriteRenderer>();
    itemHeight = renderer.sprite.bounds.size.y;
  }

  // Update is called once per frame
  void Update() {
    SetCameraDimensions();

    var cameraTop = Camera.main.transform.position.y + (cameraHeight / 2f) + (itemHeight / 2f);
    var top = cameraTop;

    var bottomItem = items.LastOrDefault();
    if (bottomItem) {
      top = bottomItem.transform.position.y - itemHeight;
    }

    var bottom = Camera.main.transform.position.y - (cameraHeight / 2f) - (itemHeight / 2f);

    for (var y = top; y > bottom; y -= itemHeight) {
      var prefab = prefabs[Random.Range(0, prefabs.Length)];
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
