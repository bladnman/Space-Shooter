using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLabeler : MonoBehaviour {

  ScoreManager scoreManager;
  TMP_Text label;

  void Start() {


    var gsCount = FindObjectsOfType<GameSession>().Length;

    var gameSession = FindObjectOfType<GameSession>();
    scoreManager = gameSession.scoreManager;
    if (scoreManager != null) {
      scoreManager.onScoreChange += HandleScoreChange;
    }
    label = GetComponent<TMP_Text>();

    UpdateScore();
  }
  void UpdateScore() {
    if (scoreManager != null) {
      label.text = $"{scoreManager.Score}";
    }
  }
  void OnEnable() {
    if (scoreManager != null) {
      scoreManager.onScoreChange += HandleScoreChange;
    }
  }
  void OnDisable() {
    if (scoreManager != null) {
      scoreManager.onScoreChange -= HandleScoreChange;
    }
  }

  private void HandleScoreChange(object sender, EventArgs e) {
    UpdateScore();
  }
}
