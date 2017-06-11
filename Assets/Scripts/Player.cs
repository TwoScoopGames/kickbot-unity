using UnityEngine;

public class Player : MonoBehaviour {

  private Animator animator;
  private Collider2D collider;
  private Movement2D movement;
  private SpriteRenderer renderer;
  private ParticleSystem particlesDust;

  private bool onWall;
  private bool isDead;
  private float leftJumpTime = -1;
  private float rightJumpTime = -1;

  public AudioClip[] jumpSounds;

  // Use this for initialization
  void Start () {
    animator = GetComponent<Animator>();
    collider = GetComponent<Collider2D>();
    movement = GetComponent<Movement2D>();
    renderer = GetComponent<SpriteRenderer>();
    particlesDust = GameObject.Find("particles-dust").GetComponent<ParticleSystem>();
  }

  private float Oscillate(float current, float period) {
    return Mathf.Sin(current / period * Mathf.PI);
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (onWall) {
      particlesDust.Play();
      return;
    }
    if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Window") {
      movement.velocity.x = 0;
      movement.velocity.y = 0;
      onWall = true;
      leftJumpTime = -1;
      rightJumpTime = -1;

      animator.SetTrigger("Land");

      var wallIsOnLeft = transform.position.x < 0;
      if (wallIsOnLeft) {
        var v = transform.position;
        v.x = other.transform.position.x +
          (other.bounds.size.x / 2f) +
          (collider.bounds.size.x / 2f);
        transform.position = v;

        renderer.flipX = false;
      } else {
        var v = transform.position;
        v.x = other.transform.position.x -
          (other.bounds.size.x / 2f) -
          (collider.bounds.size.x / 2f);
        transform.position = v;

        renderer.flipX = true;
      }
    } else if (other.gameObject.tag == "Hazard") {
      isDead = true;
      animator.SetTrigger("Explode");
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "MainCamera") {
      isDead = true;
      GameManager.instance.PlayerDied();
    }
  }

  // Update is called once per frame
  void Update () {
    if (!onWall) {
      particlesDust.Stop();
      if (leftJumpTime >= 0 && leftJumpTime <= 0.2f) {
        leftJumpTime += Time.deltaTime;
        movement.velocity.x = Oscillate(leftJumpTime + 0.1f, 0.2f) * 1000f / 3f;
      }
      if (rightJumpTime >= 0 && rightJumpTime <= 0.2f) {
        rightJumpTime += Time.deltaTime;
        movement.velocity.x = -Oscillate(rightJumpTime + 0.1f, 0.2f) * 1000f / 3f;
      }
      return;
    }

    if (isDead) {
      return;
    }

    var axis = Input.GetAxisRaw("Horizontal");
    var left = axis < 0;
    var right = axis > 0;

    var wallIsOnLeft = transform.position.x < 0;

    if (left || right) {
      GameManager.instance.StartGame();
      // FIXME: should be 500f, because of math, but it's too fast, so number is fudged for now
      movement.velocity.y = 333.333f;
      onWall = false;

      SoundManager.instance.Play(jumpSounds);

      animator.SetTrigger("Jump");

    }
    if (left) {
      if (wallIsOnLeft) {
        // sin wave bullshit
        leftJumpTime = 0;
      } else {
        movement.velocity.x = -333.333f;
      }
    } else if (right) {
      if (wallIsOnLeft) {
        movement.velocity.x = 333.333f;
      } else {
        // sine wave bullshit
        rightJumpTime = 0;
      }
    }
  }
}
