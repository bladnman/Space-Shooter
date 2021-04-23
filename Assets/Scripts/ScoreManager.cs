using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {

  public event EventHandler onScoreChange;

  private int _score = 0;
  public int Score {
    get { return _score; }
    private set {
      if (value == _score) return;
      _score = value;
      SignalScoreChange();
    }
  }
  void SignalScoreChange() {
    if (onScoreChange != null) onScoreChange(this, EventArgs.Empty);
  }
  public void Increase(int amount) {
    Score += Mathf.Abs(amount);
  }
  public void Decrease(int amount) {
    Score -= Mathf.Abs(amount);
  }
  public void Reset() {
    Score = 0;
  }
}
