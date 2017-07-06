using UnityEngine;

public class ConsumeAxis : MonoBehaviour {

  public string axisName;

  private float axisValue;
  private bool axisWasRead;

  // Use this for initialization
  void Start () {
    axisValue = 0;
    axisWasRead = false;
  }

  // Update is called once per frame
  void Update() {
    var axis = GameManager.instance.horizontalAxis;
    if (axis != axisValue) {
      axisWasRead = false;
      axisValue = axis;
    }
  }

  public float GetAxisRaw() {
    if (axisWasRead) {
      return 0;
    }
    axisWasRead = true;
    return axisValue;
  }
}
