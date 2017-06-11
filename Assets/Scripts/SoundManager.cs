using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

  public static SoundManager instance;

  void Awake () {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  public void Play (AudioClip[] sounds) {
    int random = Random.Range(0,sounds.Length);
    AudioSource.PlayClipAtPoint(sounds[random], new Vector3(0, 0, 0));
	}

}
