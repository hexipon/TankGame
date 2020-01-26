using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
  public int speed = 1000;
  private Rigidbody _rb;
  void Awake()
  {
    _rb = GetComponent<Rigidbody>();

  }
  private void Update()
  {
    if (!GameManager.Instance.CheckGameOver())
      _rb.velocity = transform.forward * speed * Time.deltaTime;

  }

  void OnCollisionEnter(Collision other)
  {
    if ("Player" == other.gameObject.tag)
      return;
    Destroy(gameObject);
      if ("Enemy" == other.gameObject.tag)
      {
        other.gameObject.GetComponent<EnemyMovement>().HitByPlayerBullet();
      }
  }
}
