using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
  [SerializeField] float health = 400;

  [Header("Death Effects")]
  [SerializeField] AudioClip deathSFX;
  [SerializeField] [Range(0, 1)] float deathSFXVolume = 1.0f;
  [SerializeField] GameObject deathVFX;
  [SerializeField] float deathVFXDuration = 1f;

  [Header("Hit Effects")]
  [SerializeField] AudioClip hitSFX;
  [SerializeField] [Range(0, 1)] float hitSFXVolume = 1.0f;
  [SerializeField] GameObject hitVFX;
  [SerializeField] float hitVFXDuration = 1f;


  public void ProcessHit(DamageDealer damageDealer) {
    health -= damageDealer.GetDamage();
    damageDealer.Hit();
    if (health <= 0) {
      DoDeath();
    } else {
      if (hitSFX) {
        AudioSource.PlayClipAtPoint(hitSFX, Camera.main.transform.position, hitSFXVolume);
      }
      if (hitVFX) {
        var fx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(fx, hitVFXDuration);
      }
    }
  }

  private void DoDeath() {
    if (deathVFX) {
      var fx = Instantiate(deathVFX, transform.position, Quaternion.identity);
      Destroy(fx, deathVFXDuration);
    }

    if (deathSFX) {
      AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    Destroy(gameObject);
  }
}
