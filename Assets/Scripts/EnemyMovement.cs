using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
//using Vuforia;


public class EnemyMovement : MonoBehaviour
{
  public GameObject target;
  private NavMeshAgent agent;
  public GameObject explodeprefab;
  public Material[] Materials;
  public GameObject[] matObjs;
  public GameObject bullet;
  public GameObject bulletOrigin;
  private bool dead = false;
  private float timer = 1f;
  public float reload = 3f;
  private RaycastHit hit;
  private Rigidbody _rigidbody;
  public AudioClip explodeSound;
  public AudioSource myFx;
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    _rigidbody = GetComponent<Rigidbody>();
  }
  
  void Update()
  {

    if ((!GameManager.Instance.CheckGameOver()) && (agent != null))
    {
      if (target != null)
        {
          agent.SetDestination(target.transform.position);
          if (matObjs[0].GetComponent<MeshRenderer>().material.color == Materials[2].color)
          {
            Physics.Raycast(bulletOrigin.transform.position, transform.TransformDirection(Vector3.forward), out hit,
              Mathf.Infinity);

            if ((Vector3.Distance(target.transform.position, transform.position) < 20f) && (hit.collider.gameObject.tag == "Player"))
            {
              agent.isStopped = true;
              if (timer >= reload)
              {
                GameObject shot;
                shot = Instantiate(bullet, bulletOrigin.transform.position, _rigidbody.transform.localRotation);
              shot.transform.TransformDirection(transform.forward);
              timer = 0f;
              }
            }
            else
            {
              agent.isStopped = false;
            }
        }
          else
          {
            agent.isStopped = false;
          }

        if ((Vector3.Distance(target.transform.position, transform.position) < 2.2f) || (dead))
            {
              Death();
              if (!dead)
                target.GetComponent<PlayerControl>().PlayerHit();
          }
        }
    }
    else
    {
      agent.isStopped=true;
    }
    timer += Time.deltaTime;
  }
  public void HitByPlayerBullet()
  {
    if (matObjs[0].GetComponent<MeshRenderer>().material.color == Materials[2].color)
    {
      matObjs[0].GetComponent<MeshRenderer>().material = Materials[1];
      matObjs[1].GetComponent<MeshRenderer>().material = Materials[1];
    }
    else if (matObjs[0].GetComponent<MeshRenderer>().material.color == Materials[1].color)
    {
      matObjs[0].GetComponent<MeshRenderer>().material = Materials[0];
      matObjs[1].GetComponent<MeshRenderer>().material = Materials[0];
    }
    else if (matObjs[0].GetComponent<MeshRenderer>().material.color == Materials[0].color)
    {
      dead = true;
      target.GetComponent<PlayerControl>().IncreaseScore();
    }

  }
  private void Death()
  {
    ParticleSystem explode = Instantiate(explodeprefab).GetComponent<ParticleSystem>();
    explode.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    explode.Play();
    myFx.PlayOneShot(explodeSound);
    Destroy(gameObject);
    Destroy(explode, explode.main.duration + explode.main.startLifetimeMultiplier);
  }
}
