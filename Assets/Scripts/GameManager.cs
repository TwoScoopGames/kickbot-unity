﻿using UnityEngine;

public class GameManager : MonoBehaviour { 

  public static GameManager instance;
  public float gameOverScreenTime = 1f;

  public GameObject gameOverScreen;
  public GameObject gameOverOverlay;
  private Flash gameOverFlash;

  private bool waitingToStart = true;
  private ParticleSystem particlesDust;

  // Use this for initialization
  void Awake () {
    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);

    particlesDust = GameObject.Find("particles-dust").GetComponent<ParticleSystem>();

    gameOverFlash = gameOverOverlay.GetComponent<Flash>();
  }

  public void StartGame() {
    if (!waitingToStart) {
      return;
    }
    waitingToStart = false;

    StartMusic.instance.Play();

    var title = GameObject.Find("Title UI");
    title.SetActive(false);

    var player = GameObject.Find("Player");
    var acceleration = player.GetComponent<Acceleration2D>();
    acceleration.enabled = true;

    var backgroundSpawner = GameObject.Find("Background Spawner");
    VerticalSpawner spawner = backgroundSpawner.GetComponent<BackgroundSpawner>();
    spawner.ChangeDirection();

    var wallLeftSpawner = GameObject.Find("Wall Left Spawner");
    spawner = wallLeftSpawner.GetComponent<WallSpawner>();
    spawner.ChangeDirection();

    var wallRightSpawner = GameObject.Find("Wall Right Spawner");
    spawner = wallRightSpawner.GetComponent<WallSpawner>();
    spawner.ChangeDirection();

    var dustTransform = particlesDust.transform;
    dustTransform.eulerAngles = new Vector3 (-dustTransform.eulerAngles.x, 0, 0);

  }

  public void PlayerDied(int score) {
    var highScore = PlayerPrefs.GetInt("High Score");
    if (score > highScore) {
      PlayerPrefs.SetInt("High Score", score);
      Debug.Log(string.Format("New high score: {0}", score));
    }

    gameOverScreen.SetActive(true);
    gameOverFlash.Begin();

    Invoke("Restart", gameOverScreenTime);
  }

  public void Restart() {
    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    waitingToStart = true;
  }

  private bool didIncrement;
  private int hazardCounter;
  private string side;

  public string GetHazard() {
    if (didIncrement) {
      return side;
    }
    didIncrement = true;
    if (hazardCounter == 0) {
      side = null;
    } else {
      side = Random.Range(0f, 1f) > 0.5f ? "Left" : "Right";
    }
    hazardCounter = (hazardCounter + 1) % 3;
    return side;
  }

  // Update is called once per frame
  void Update () {
    didIncrement = false;
  }
}
