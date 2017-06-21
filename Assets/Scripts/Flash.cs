using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

  public float flashTime;
  public float maxAlpha = 1.0f;
  public bool runOnStart = true;

  private Image image;
  private float currentTime = -1;

  // Use this for initialization
  void Start () {
    image = GetComponent<Image>();
    currentTime = runOnStart ? 0 : -1;
  }

  public void Begin() {
    currentTime = 0;
  }

  // Update is called once per frame
  void Update () {
    if (currentTime < 0) {
      return;
    }

    currentTime += Time.deltaTime;

    var color = image.color;
    if (currentTime > flashTime) {
      color.a = 0f;
      currentTime = -1;
    } else {
      color.a = Mathf.Min(maxAlpha, Mathf.Sin(currentTime / flashTime * Mathf.PI));
    }
    image.color = color;
  }
}
