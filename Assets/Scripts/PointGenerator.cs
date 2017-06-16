using UnityEngine;

public class PointGenerator : MonoBehaviour {

  private Player player;

  private SpriteRenderer renderer;
  private SpriteRenderer playerRenderer;

  // Use this for initialization
  void Start () {
    player = GameObject.Find("Player").GetComponent<Player>();
    renderer = GetComponent<SpriteRenderer>();
    playerRenderer = player.GetComponent<SpriteRenderer>();
  }

  // Update is called once per frame
  void Update () {
    var wallTop = transform.position.y + (renderer.bounds.size.y / 2);
    var playerBottom = player.transform.position.y + (playerRenderer.bounds.size.y / 2);
    if (playerBottom > wallTop) {
      enabled = false;
      player.AddPoint();
    }
  }
}
