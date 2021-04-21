using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

  public void LoadStartMenu() {
    SceneManager.LoadScene("Menu");
  }
  public void LoadGameScene() {
    SceneManager.LoadScene("Game");
  }
  public void LoadGameOver() {
    SceneManager.LoadScene("Game Over");
  }
  public void QuitGame() {
    Application.Quit();
  }
}
