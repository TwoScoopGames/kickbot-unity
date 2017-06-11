using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

  public static GameManager instance;

  private bool waitingToStart = true;

  // Use this for initialization
  void Awake () {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);
  }

  public void StartGame() {
    if (!waitingToStart) {
      return;
    }
    waitingToStart = false;

    var player = GameObject.Find("Player");
    var acceleration = player.GetComponent<Acceleration2D>();
    acceleration.enabled = true;

    var backgroundSpawner = GameObject.Find("Background Spawner");
    VerticalSpawner spawner = backgroundSpawner.GetComponent<Spawner>();
    spawner.ChangeDirection();

    var wallLeftSpawner = GameObject.Find("Wall Left Spawner");
    spawner = wallLeftSpawner.GetComponent<WallSpawner>();
    spawner.ChangeDirection();

    var wallRightSpawner = GameObject.Find("Wall Right Spawner");
    spawner = wallRightSpawner.GetComponent<WallSpawner>();
    spawner.ChangeDirection();
  }

  // Update is called once per frame
  void Update () {
    
  }
}
