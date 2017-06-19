using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour {

  public float flashTime;

  private Image image;
  private float currentTime = -1;

  // Use this for initialization
  void Start () {
    image = GetComponent<Image>();
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
      color.a = Mathf.Sin(currentTime / flashTime * Mathf.PI);
    }
    image.color = color;
  }
}
