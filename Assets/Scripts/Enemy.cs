using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] float projectialSpeed = 8f;
  [SerializeField] GameObject projectilePrefab;
  [SerializeField] AudioClip shootSFX;
  [SerializeField] [Range(0, 1)] float shootSFXVolume = 1.0f;
  [SerializeField] [Range(0, 2000)] int scoreValue = 125;

  float shotCounter;
  CollisionManager collisionManager;
  bool hasDied = false;

  void Start() {
    shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    // listen for death
    collisionManager = GetComponent<CollisionManager>();
    collisionManager.onDeath += HandleDeath;
  }
  void OnEnable() {
    if (collisionManager) {
      collisionManager.onDeath += HandleDeath;
    }
  }
  void OnDisable() {
    collisionManager.onDeath -= HandleDeath;
  }
  void Update() {
    CountDownAndShoot();
  }


  /** DEATH */
  private void HandleDeath(object sender, EventArgs e) {

    if (hasDied) return;
    var gameSession = FindObjectOfType<GameSession>();
    var scoreManager = gameSession.scoreManager;
    if (scoreManager != null) {
      scoreManager.Increase(scoreValue);
    }
  }
  private void CountDownAndShoot() {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0) {
      DoFire();
    }
  }

  private void DoFire() {
    // reset shot counter
    shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    if (shootSFX) {
      AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }

    // fire!
    var shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
    shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectialSpeed * -1);
  }
}
