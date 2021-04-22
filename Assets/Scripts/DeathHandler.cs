using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {
  [SerializeField] [Range(0f, 2.0f)] float deathDelay = 1.5f;
  public float GetDeathDelay() { return deathDelay; }

  public void Die() {
    StartCoroutine(DieAfterDelay());
  }
  IEnumerator DieAfterDelay() {
    yield return new WaitForSeconds(deathDelay);
    FindObjectOfType<Level>().LoadGameOver();
  }
}
