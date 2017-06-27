using UnityEngine;
using UnityEngine.UI;

public class SetTextForNewHighScore : MonoBehaviour {

  public string message;

  private Text text;

  // Use this for initialization
  void Start () {
    text = GetComponent<Text>();
  }

  // Update is called once per frame
  void Update () {
    if (GameManager.instance.WasNewHighScore()) {
      text.text = message;
    }
  }
}
