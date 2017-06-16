using UnityEngine;

public class ConsumeAxis : MonoBehaviour {

  public string axisName;

  private float axisValue;
  private bool axisWasRead;

  // Update is called once per frame
  void Update() {
    var axis = Input.GetAxisRaw(axisName);
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
