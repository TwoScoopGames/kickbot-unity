using UnityEngine;
using UnityEngine.UI;

public class SetTextColorForNewHighScore : MonoBehaviour {

  public Color color;

  private Text text;

  // Use this for initialization
  void Start () {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update () {
    if (GameManager.instance.WasNewHighScore()) {
      Debug.Log("new high score");
      text.color = color;
    }
  }
}
