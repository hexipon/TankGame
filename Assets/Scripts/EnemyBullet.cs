using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
  public int speed = 500;
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
    if ("Enemy" == other.gameObject.tag)
      return;
      Destroy(gameObject);
    if ("Player" == other.gameObject.tag)
    {
      other.gameObject.GetComponent<PlayerControl>().PlayerHit();
      other.gameObject.GetComponent<PlayerControl>().HitByBullet();
    }
  }
}
