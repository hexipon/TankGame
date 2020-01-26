using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerControl : MonoBehaviour
{
  private Rigidbody _rigidbody;
  public float speed = 12f;
  public float rotSpeed = 200f;
  private float movementInput;
  private float turnInput;
  private float movementInputValue;
  private float turnInputValue;
  private int health = 20;
  private int score = 0;
  public GameObject healthDisplay;
  public GameObject scoreDisplay;
  public GameObject bullet;
  public GameObject bulletOrigin;
  private float timer = 1f;
  private float reload = 0.2f;
  public GameObject gameOverScreen;
  public GameObject submit;
  public GameObject submitText;
  public GameObject[] tank;
  public GameObject explodePrefab;
  public AudioClip explodeSound;
  public AudioSource myFx;
  public AudioClip bulletImpact;

  private void Start()
  {
    _rigidbody = GetComponent<Rigidbody>();
    _rigidbody.centerOfMass = new Vector3(0f, -0.8f, 0f);
    healthDisplay.GetComponent<TextMeshProUGUI>().text = "Health: " + health.ToString();
  }
  private void Update()
  {
    if (!GameManager.Instance.CheckGameOver())
    {
      movementInput = Input.GetAxis("Vertical");
      turnInput = Input.GetAxis("Horizontal");
      movementInputValue = movementInput;
      turnInputValue = turnInput;
      Move();
      Rotate();
      if (Input.GetButton("Fire") && (timer >= reload))
      {
        GameObject shot;
        shot = Instantiate(bullet, bulletOrigin.transform.position, _rigidbody.rotation);
        shot.transform.TransformDirection(transform.forward);
        timer = 0f;
      }

      timer += Time.deltaTime;
    }
  }

  private void Move()
  {
    Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
    _rigidbody.MovePosition(_rigidbody.position + movement);
  }

  private void Rotate()
  {
    float turn = turnInputValue * rotSpeed * Time.deltaTime;
    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
    _rigidbody.MoveRotation(_rigidbody.rotation * turnRotation);
  }

  public void PlayerHit()
  {
    health--;
    healthDisplay.GetComponent<TextMeshProUGUI>().text = "Health: " + health.ToString();
    if (health <= 0)
    {
      gameOverScreen.SetActive(true);
      if (GameManager.Instance.CheckIfBetterThanLast(score))
        submit.SetActive(true);
      ParticleSystem explode = Instantiate(explodePrefab).GetComponent<ParticleSystem>();
      explode.transform.position = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
      explode.Play();
      myFx.PlayOneShot(explodeSound);
      for (int i = tank.Length-1; i>=0;i--)
        tank[i].SetActive(false);
      GameManager.Instance.GameOver();
    }
  }

  public void UpdateScoreBoard()
  {
    GameManager.Instance.AddHighscoreEntry(score, submitText.GetComponent<TextMeshProUGUI>().text);
    submit.SetActive(false);
  }

  public void HitByBullet()
  {
    myFx.PlayOneShot(bulletImpact);
  }

  public int GetScore() => score;
  public void IncreaseScore()
  {
    score++;
    scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
  }
}