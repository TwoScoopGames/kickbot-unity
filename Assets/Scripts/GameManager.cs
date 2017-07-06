using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour { 

  public static GameManager instance;
  public float gameOverScreenTime = 1f;

  public GameObject gameOverScreen;
  private GameObject canvas;

  private bool waitingToStart = true;
  private int score;
  private int highScore;
  private bool newBest;

  public float horizontalAxis = 0;

  // Use this for initialization
  void Awake () {

    Application.targetFrameRate = 60;

    if (instance == null) {
      instance = this;
    } else {
      Destroy(gameObject);
    }
    DontDestroyOnLoad(gameObject);

    highScore = PlayerPrefs.GetInt("High Score");
    setupTouchButtons ();
    SceneManager.sceneLoaded += (d, e) => { setupTouchButtons(); };
  }

  public void StartGame() {
    if (!waitingToStart) {
      return;
    }

    waitingToStart = false;

    StartMusic.instance.Play();

    canvas = GameObject.Find("Canvas");

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

    var particlesDust = GameObject.Find("particles-dust").GetComponent<ParticleSystem>();
    var dustTransform = particlesDust.transform;
    dustTransform.eulerAngles = new Vector3 (-dustTransform.eulerAngles.x, 0, 0);


  }

  public void AddPoint() {
    score++;
  }
  public int GetScore() {
    return score;
  }
  public int GetHighScore() {
    return highScore;
  }
  public bool WasNewHighScore() {
    return newBest;
  }

  public void PlayerDied() {
    if (score > highScore) {
      highScore = score;
      newBest = true;
      PlayerPrefs.SetInt("High Score", highScore);
      Debug.Log(string.Format("New high score: {0}", score));
    }

    var levelUI = GameObject.Find("Level UI");
    levelUI.SetActive(false);

    Instantiate(gameOverScreen, canvas.transform);

    Invoke("Restart", gameOverScreenTime);
  }

  public void Restart() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    waitingToStart = true;
    score = 0;
    newBest = false;
    horizontalAxis = 0;
     
  }

  private bool didIncrement;
  private int hazardCounter;
  private string side;

  public string GetHazard() {
    if (waitingToStart) {
      return null;
    }
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

    if (Input.GetAxisRaw ("Horizontal") != 0){
      horizontalAxis =  Input.GetAxisRaw ("Horizontal");
    }

  }


  void setupTouchButtons(){
    addEventTriggerToButton (GameObject.Find("Left Button"), "PointerDown", (d) => {
      horizontalAxis = -1;
    });
    addEventTriggerToButton (GameObject.Find("Left Button"), "PointerUp", (d) => {
      horizontalAxis = 0;
    });
    addEventTriggerToButton (GameObject.Find("Right Button"), "PointerDown", (d) => {
      horizontalAxis = 1;
    });
    addEventTriggerToButton (GameObject.Find("Right Button"), "PointerUp", (d) => {
      horizontalAxis = 0;
    });
  }

  void addEventTriggerToButton (GameObject button, string triggerType, UnityAction<BaseEventData> buttonFunction) {
    EventTrigger trigger = button.GetComponent<EventTrigger>();
    EventTrigger.Entry entry = new EventTrigger.Entry();
    entry.eventID =  (EventTriggerType)System.Enum.Parse (typeof(EventTriggerType), triggerType);
    entry.callback = new EventTrigger.TriggerEvent();
    UnityEngine.Events.UnityAction<BaseEventData> call = new UnityEngine.Events.UnityAction<BaseEventData>(buttonFunction);
    entry.callback.AddListener(call);
    trigger.triggers.Add(entry);
  }


  void LateUpdate () {
      horizontalAxis =  0;
  }



 

}
