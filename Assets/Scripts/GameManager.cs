using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
  enum GameState
  {
    START,
    IN_GAME,
    GAME_OVER,
    INSTRUCTIONS,
    LEADERBOARD,
    PAUSE,
    UNPAUSE
  };

  private GameState gameState;

  public bool CheckGameOver()
  {
    return (gameState == GameState.GAME_OVER);
  }
  public static GameManager Instance { get; private set; } = null;

  public Highscores highscores;

  private void Awake()
  {

    if (Instance != null)
      Destroy(gameObject);

    Instance = this;

    DontDestroyOnLoad(gameObject);

    //PlayerPrefs.DeleteAll(); //uncomment this reset the high score list, recomment after one run
    string jsonString = PlayerPrefs.GetString("highscoreT");
    highscores = JsonUtility.FromJson<Highscores>(jsonString);
    if (highscores == null)
    {
      for (int i = 0; i < 22; i++)
        AddHighscoreEntry(0, "N/A");
      jsonString = PlayerPrefs.GetString("highscoreT");
      highscores = JsonUtility.FromJson<Highscores>(jsonString);
    }
  }

  private void SortList(Highscores highscores)
  {
    for (int a = 0; a < highscores.highScoreEntries.Count; a++)
    {
      for (int b = a + 1; b < highscores.highScoreEntries.Count; b++)
      {
        if (highscores.highScoreEntries[b].score > highscores.highScoreEntries[a].score)
        {
          HighscoreEntry tmp = highscores.highScoreEntries[a];
          highscores.highScoreEntries[a] = highscores.highScoreEntries[b];
          highscores.highScoreEntries[b] = tmp;
        }
      }
    }
    for (int c = highscores.highScoreEntries.Count-1; c > 9; c--)
    {
      highscores.highScoreEntries.RemoveAt(c);
    }
  }

  public void AddHighscoreEntry(int score, string name)
  {

    Debug.Log("Stuff:" + score + name);
    HighscoreEntry highscoreEntry = new HighscoreEntry(score, name);

    if (highscores == null)
    {
      highscores = new Highscores(new List<HighscoreEntry>());
    }

    highscores.highScoreEntries.Add(highscoreEntry);
    SortList(highscores);
    string json = JsonUtility.ToJson(highscores);
    PlayerPrefs.SetString("highscoreT", json);
    PlayerPrefs.Save();
  }

  public bool CheckIfBetterThanLast(int score)
  {

    string jsonString = PlayerPrefs.GetString("highscoreT");
    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

    return (score > highscores.highScoreEntries[9].score);
  }




  void OnChangeState(GameState newState)
  {
    gameState = newState;
    switch (newState)
    {
      case GameState.START:
        Cursor.visible = true;
        SceneManager.LoadScene(0);
        break;
      case GameState.IN_GAME:
        SceneManager.LoadScene(1);

        Cursor.visible = false;

        break;
      case GameState.GAME_OVER:
        Cursor.visible = true;
        //Time.timeScale = 0; 
        break;
      case GameState.INSTRUCTIONS:
        Cursor.visible = true;
        SceneManager.LoadScene(2);

        break;
      case GameState.LEADERBOARD:
        Cursor.visible = true;
        SceneManager.LoadScene(3);

        break;
      case GameState.PAUSE:
        Time.timeScale = 0;
        break;
      case GameState.UNPAUSE:
        Time.timeScale = 1;
        break;
    }
  }


  private void Start()
  {
    Screen.SetResolution(1024, 768, true);
  }

  public void MainMenu() => OnChangeState(GameState.START);
  public void Play() => OnChangeState(GameState.IN_GAME);
  public void Instructions() => OnChangeState(GameState.INSTRUCTIONS);
  public void Leaderboard() => OnChangeState(GameState.LEADERBOARD);
  public void GameOver() => OnChangeState(GameState.GAME_OVER);
  public void Pause() => OnChangeState(GameState.PAUSE);
  public void Unpause() => OnChangeState(GameState.UNPAUSE);

  public void Quit()
  {
#if UNITY_STANDALONE    
    Application.Quit();
#endif

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
  }
}
public class Highscores
{
  public List<HighscoreEntry> highScoreEntries;

  public Highscores(List<HighscoreEntry> highScoreEntries)
  {
    this.highScoreEntries = highScoreEntries;
  }
}

[System.Serializable]
public struct HighscoreEntry
{
  public int score;
  public string name;

  public HighscoreEntry(int score, string name)
  {
    this.score = score;
    this.name = name;
  }
}