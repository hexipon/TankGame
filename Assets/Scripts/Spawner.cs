using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering;
using Random = System.Random;

public class Spawner : MonoBehaviour
{

  [SerializeField] private float minTime;
  [SerializeField] private float maxTime;

  public GameObject player;
  public AudioSource myFx;
  private float speedMultiplyer = 1f;
  [SerializeField] private GameObject[] enemyPrefab;
  [SerializeField] private Transform[] spawnPoints;

  private float timeDelta = 0;

  void Start()
  {
    Random rnd = new Random();
    timeDelta = rnd.Next(Convert.ToInt32(minTime), Convert.ToInt32(maxTime));
  }

  void Update()
  {
    if ((timeDelta > 0) && (!GameManager.Instance.CheckGameOver()))
    {
      timeDelta -= Time.deltaTime;
      if (timeDelta <= 0)
      {
        Random rnd = new Random();
        int randomPoint = rnd.Next(0, spawnPoints.Length);
        int randEnemy = rnd.Next(0, CheckDifficulty()+1);
        Vector3 pos = new Vector3(spawnPoints[randomPoint].position.x + rnd.Next(0,10), spawnPoints[randomPoint].position.y, spawnPoints[randomPoint].position.z + rnd.Next(0, 10));
        GameObject enemy = Spawn(pos, randEnemy);


        if (NavMesh.SamplePosition(pos, out NavMeshHit closestHit, 500, 1))
        {
          enemy.transform.position = closestHit.position;
          enemy.GetComponent<EnemyMovement>().target = player;
          enemy.GetComponent<EnemyMovement>().myFx = myFx;

          enemy.AddComponent<NavMeshAgent>();
          if (randEnemy != 2)
            enemy.GetComponent<NavMeshAgent>().autoBraking = false;
          enemy.GetComponent<NavMeshAgent>().autoRepath = true;
          enemy.GetComponent<NavMeshAgent>().height = 1;
          enemy.GetComponent<NavMeshAgent>().acceleration = 10;
          enemy.GetComponent<NavMeshAgent>().speed = ((randEnemy == 1)? 10f*speedMultiplyer:5f);
          Debug.Log(enemy.GetComponent<NavMeshAgent>().speed);
          enemy.GetComponent<NavMeshAgent>().angularSpeed = 1000f;
          enemy.GetComponent<NavMeshAgent>().destination = player.transform.position;
          enemy.GetComponent<EnemyMovement>().reload *= speedMultiplyer;
        }
        speedMultiplyer += Time.deltaTime/2;
        timeDelta = (rnd.Next(Convert.ToInt32(minTime), Convert.ToInt32(maxTime))) - ((speedMultiplyer - 1)/1.5f);
      }
    }
  }

  private int CheckDifficulty()
  {
    int score = player.GetComponent<PlayerControl>().GetScore();
    Debug.Log("score: " + score);
    int difficulty=0;
    if (score > 10)
      difficulty = 2;
    else if ((score <= 10) && (score > 5))
      difficulty = 1;
    return difficulty;
  }

  public GameObject Spawn(Vector3 pos, int enemyNum)
  {
    return (GameObject)Instantiate(enemyPrefab[enemyNum], pos, Quaternion.identity);
  }



}
