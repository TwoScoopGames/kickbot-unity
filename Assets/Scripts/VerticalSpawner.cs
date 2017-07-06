using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class VerticalSpawner : MonoBehaviour {

  public Vector3 velocity;

  private float cameraWidth;
  private float cameraHeight;
  private float wallHeight;
  private List<GameObject> items = new List<GameObject>();

  // Use this for initialization
  protected virtual void Start() {
    wallHeight = ItemHeight();
  }

  protected abstract float ItemHeight();

  // Update is called once per frame
  protected virtual void Update() {
    SetCameraDimensions();
    if (velocity.y > 0) {
      PopulateWallsDown();
    } else {
      PopulateWallsUp();
    }
    SetVelocities();
  }

  private void PopulateWallsDown() {
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
      var instances = OnSpawn(bottomItemTag, new Vector3(transform.position.x, y, 0f));
      bottomItemTag = instances[0].tag;
      items.AddRange(instances);
    }

    for (var i = items.Count - 1; i >= 0; i--) {
      var item = items[i];
      if (item.transform.position.y > cameraTop) {
        items.RemoveAt(i);
        Destroy(item);
      }
    }
  }

  public void ChangeDirection() {
    velocity *= -1;
    items.Reverse();
  }

  private void PopulateWallsUp() {
    var top = Camera.main.transform.position.y + (cameraHeight / 2f) + (wallHeight / 2f);
    var cameraBottom = Camera.main.transform.position.y - (cameraHeight / 2f) - (wallHeight / 2f);
    var bottom = cameraBottom;

    var topItem = items.LastOrDefault();
    var topItemTag = "Wall";
    if (topItem) {
      bottom = topItem.transform.position.y + wallHeight;
      topItemTag = topItem.tag;
    }

    for (var y = bottom; y < top; y += wallHeight) {
      var instances = OnSpawn(topItemTag, new Vector3(transform.position.x, y, 0f));
      topItemTag = instances[0].tag;
      items.AddRange(instances);
    }

    for (var i = items.Count - 1; i >= 0; i--) {
      var item = items[i];
      if (item.transform.position.y < cameraBottom) {
        items.RemoveAt(i);
        Destroy(item);
      }
    }
  }

  protected abstract IList<GameObject> OnSpawn(string lastTag, Vector3 position);

  private void SetVelocities() {
    foreach (var item in items) {
      SetVelocity(item);
    }
  }

  private void SetVelocity(GameObject instance) {
    Movement2D movement = instance.GetComponent<Movement2D>();
    movement.velocity = velocity;
  }

  private void SetCameraDimensions() {
    Camera camera = Camera.main;
    cameraHeight = 2f * camera.orthographicSize;
    cameraWidth = cameraHeight * camera.aspect;
  }
}
