using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
  [SerializeField] float shotCounter;
  [SerializeField] float minTimeBetweenShots = 0.2f;
  [SerializeField] float maxTimeBetweenShots = 3f;
  [SerializeField] float projectialSpeed = 8f;
  [SerializeField] GameObject projectilePrefab;
  [SerializeField] AudioClip shootSFX;
  [SerializeField] [Range(0, 1)] float shootSFXVolume = 1.0f;

  Health health;

  void Start() {
    health = GetComponent<Health>();
    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
  }
  void Update() {
    CountDownAndShoot();
  }

  private void CountDownAndShoot() {
    shotCounter -= Time.deltaTime;
    if (shotCounter <= 0) {
      DoFire();
    }
  }

  private void DoFire() {
    // reset shot counter
    shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

    if (shootSFX) {
      AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }

    // fire!
    var shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
    shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectialSpeed * -1);
  }

  private void OnTriggerEnter2D(Collider2D other) {
    var damageDealer = other.gameObject.GetComponent<DamageDealer>();
    if (null == damageDealer) return;
    health.ProcessHit(damageDealer);
  }
}
