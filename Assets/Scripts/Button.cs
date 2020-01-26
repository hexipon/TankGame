using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
  public AudioSource myFx;
  public AudioClip hover;

  public void HoverSound() => myFx.PlayOneShot(hover);
  public void Play() => GameManager.Instance.Play();
  public void Leaderboard() => GameManager.Instance.Leaderboard();
  public void Exit() => GameManager.Instance.Quit();
  public void MainMenu() => GameManager.Instance.MainMenu();
  public void Pause() => GameManager.Instance.Pause();
  public void Unpause() => GameManager.Instance.Unpause();
  public void Instructions() => GameManager.Instance.Instructions();
}
