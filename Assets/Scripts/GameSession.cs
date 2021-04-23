using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
  public ScoreManager scoreManager {
    get {
      return GameSessionSingleton.Instance.scoreManager;
    }
  }
}
public class GameSessionSingleton {
  public ScoreManager scoreManager = new ScoreManager();

  private GameSessionSingleton() { }

  // ------------------------------------
  private static GameSessionSingleton _instance;
  public static GameSessionSingleton Instance {
    get {
      if (_instance == null)
        _instance = new GameSessionSingleton();
      return _instance;
    }
  }
}
