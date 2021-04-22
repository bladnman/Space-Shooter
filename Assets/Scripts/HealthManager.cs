using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
  [SerializeField] int maxHealth = 100;
  int currentHealth;

  public event EventHandler onHealthChanged;

  private void Start() {
    currentHealth = maxHealth;
  }
  public int GetHealth() {
    return currentHealth;
  }
  public int GetMaxHealth() {
    return maxHealth;
  }
  public float GetHealthPercent() {
    return (float)maxHealth / currentHealth;
  }
  public void Damage(int amount) {
    currentHealth -= Mathf.Abs(amount);
    if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
  }
  public void Heal(int amount) {
    int beforeChange = currentHealth;
    currentHealth += Mathf.Abs(amount);

    // stop at max
    if (currentHealth > maxHealth) {
      currentHealth = maxHealth;
    }

    // no change - bail
    if (currentHealth == beforeChange) return;

    // change - signal
    if (onHealthChanged != null) onHealthChanged(this, EventArgs.Empty);
  }
}
