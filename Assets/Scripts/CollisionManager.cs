using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

  public event EventHandler onHit;
  public event EventHandler onDeath;

  HealthManager healthManager;

  void Start() {
    healthManager = GetComponent<HealthManager>();
  }
  void SignalHit() {
    if (onHit != null) onHit(this, EventArgs.Empty);
  }
  void SignalDeath() {
    if (onDeath != null) onDeath(this, EventArgs.Empty);
  }
  void Damage(int damage) {
    // UPDATE HEALTH - send signals
    if (healthManager != null) {
      healthManager.Damage(damage);
      if (healthManager.GetHealth() <= 0) {
        SignalDeath();
      } else {
        SignalHit();
      }
    }
  }
  void OnTriggerEnter2D(Collider2D other) {
    var damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (null == damageDealer) return;

    damageDealer.Hit();

    Damage(damageDealer.GetDamage());
  }
}
