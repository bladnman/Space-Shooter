using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthLabeler : MonoBehaviour {

  HealthManager healthManager;
  TMP_Text label;

  void Start() {
    var player = FindObjectOfType<Player>();
    if (player) {
      healthManager = player.healthManager;
      if (healthManager) {
        healthManager.onHealthChanged += HandleHealthChange;
      }
    }
    label = GetComponent<TMP_Text>();
    UpdateDisplay();
  }

  private void HandleHealthChange(object sender, EventArgs e) {
    UpdateDisplay();
  }

  void UpdateDisplay() {
    if (healthManager) {
      label.text = $"{healthManager.GetHealth()}";

      var healthPerc = healthManager.GetHealthPercent();
      if (healthPerc < .25f) {
        label.color = new Color32(255, 0, 0, 255);
      } else if (healthPerc < .50f) {
        label.color = new Color32(255, 150, 0, 255);
      } else if (healthPerc < .75f) {
        label.color = new Color32(150, 255, 0, 255);
      } else {
        label.color = new Color32(0, 255, 0, 255);
      }
    }
  }
  void OnEnable() {
    if (healthManager) {
      healthManager.onHealthChanged += HandleHealthChange;
    }
  }
  void OnDisable() {
    if (healthManager) {
      healthManager.onHealthChanged -= HandleHealthChange;
    }
  }
}
