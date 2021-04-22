using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour {

  [Header("Hit Effects")]
  [SerializeField] AudioClip hitSFX;
  [SerializeField] [Range(0, 1)] float hitSFXVolume = 1.0f;
  [SerializeField] GameObject hitVFX;
  [SerializeField] float hitVFXDuration = 1f;

  [Header("Death Effects")]
  [SerializeField] AudioClip deathSFX;
  [SerializeField] [Range(0, 1)] float deathSFXVolume = 1.0f;
  [SerializeField] GameObject deathVFX;
  [SerializeField] float deathVFXDuration = 1f;

  CollisionManager collisionManager;
  bool hasDied = false;

  void Start() {
    collisionManager = GetComponent<CollisionManager>();
    collisionManager.onHit += HandleHit;
    collisionManager.onDeath += HandleDeath;
  }
  void OnEnable() {
    if (collisionManager) {
      collisionManager.onHit += HandleHit;
      collisionManager.onDeath += HandleDeath;
    }
  }
  void OnDisable() {
    collisionManager.onHit -= HandleHit;
    collisionManager.onDeath -= HandleDeath;
  }

  /** HIT */
  private void HandleHit(object sender, EventArgs e) {
    if (hitSFX) {
      AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
    }
    if (hitVFX) {
      var fx = Instantiate(hitVFX, transform.position, Quaternion.identity);
      Destroy(fx, hitVFXDuration);
    }
  }

  /** DEATH */
  private void HandleDeath(object sender, EventArgs e) {

    if (hasDied) return;

    hasDied = true;
    if (deathVFX) {
      var fx = Instantiate(deathVFX, transform.position, Quaternion.identity);
      Destroy(fx, deathVFXDuration);
    }

    if (deathSFX) {
      AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    // USE DEATH HANDLER
    var deathHandler = gameObject.GetComponent<DeathHandler>();
    if (deathHandler) {
      deathHandler.Die();
    }

    // simply destroy
    else {
      Destroy(gameObject);
    }
  }
}
