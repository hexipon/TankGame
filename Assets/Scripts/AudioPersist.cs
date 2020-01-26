using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPersist : MonoBehaviour
{
  static bool AudioBegin = false;
  void Awake()
  {
    if (!AudioBegin)
    {
      GetComponent<AudioSource>().Play();
      DontDestroyOnLoad(gameObject);
      AudioBegin = true;
    }
  }
  void Update()
  {
    if (SceneManager.GetActiveScene().name == "Playing")
    {
      GetComponent<AudioSource>().Stop();
      AudioBegin = false;
    }
    else if (!GetComponent<AudioSource>().isPlaying)
    {
      GetComponent<AudioSource>().Play();
    }
  }
}
