using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HST : MonoBehaviour
{

  public Transform entryContainer;
  public Transform entryTemplate;
  private Highscores highscores;

  private void Start()
  {
    highscores = GameManager.Instance.highscores;
    entryTemplate.gameObject.SetActive(false);

    for (int i = 0; i <= highscores.highScoreEntries.Count - 1; i++)
    {
      float templateHeight = 31f;
      Transform entryTransform = Instantiate(entryTemplate, entryContainer);
      RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
      entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

      string rank;
      switch (i + 1)
      {
        case 1:
          entryTransform.Find("background").GetComponent<Image>().color = Color.green;
          rank = "1ST";
          break;
        case 2:
          entryTransform.Find("background").GetComponent<Image>().color = Color.cyan;
          rank = "2ND";
          break;
        case 3:
          entryTransform.Find("background").GetComponent<Image>().color = Color.yellow;
          rank = "3RD";
          break;
        default:
          entryTransform.Find("background").GetComponent<Image>().color = Color.gray;
          rank = (i + 1) + "TH";
          break;
      }

      entryTransform.Find("rank").GetComponent<Text>().text = rank;
      entryTransform.Find("score").GetComponent<Text>().text = highscores.highScoreEntries[i].score.ToString();
      entryTransform.Find("name").GetComponent<Text>().text = highscores.highScoreEntries[i].name;
      entryTransform.gameObject.SetActive(true);

    }
    for (int i = 0; i <= highscores.highScoreEntries.Count - 1; i++)
    {
      float templateHeight = 31f;
      Transform entryTransform = Instantiate(entryTemplate, entryContainer);
      RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
      entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);

      string rank;
      switch (i + 1)
      {
        case 1:
          entryTransform.Find("background").GetComponent<Image>().color = Color.green;
          rank = "1ST";
          break;
        case 2:
          entryTransform.Find("background").GetComponent<Image>().color = Color.cyan;
          rank = "2ND";
          break;
        case 3:
          entryTransform.Find("background").GetComponent<Image>().color = Color.yellow;
          rank = "3RD";
          break;
        default:
          entryTransform.Find("background").GetComponent<Image>().color = Color.gray;
          rank = (i + 1) + "TH";
          break;
      }

      entryTransform.Find("rank").GetComponent<Text>().text = rank;
      entryTransform.Find("score").GetComponent<Text>().text = highscores.highScoreEntries[i].score.ToString();
      entryTransform.Find("name").GetComponent<Text>().text = highscores.highScoreEntries[i].name;
      entryTransform.gameObject.SetActive(true);
    }
  }
}
