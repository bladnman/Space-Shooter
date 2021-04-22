using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [Header("Player")]
  [SerializeField] [Range(0.1f, 100f)] float moveSpeed = 10f;
  [SerializeField] [Range(0f, 1.0f)] float yMaxRatio = 0.5f;

  [Header("Projectile")]
  [SerializeField] GameObject laserPrefab;
  [SerializeField] float laserSpeed = 15f;
  [SerializeField] float laserFirePeriod = 0.1f;
  [SerializeField] AudioClip shootSFX;
  [SerializeField] [Range(0, 1)] float shootSFXVolume = 1.0f;

  Coroutine firingCoroutine;

  float xMin, xMax;
  float yMin, yMax;

  void Start() {
    SetUpMoveBoundaries();
  }
  void Update() {
    Move();
    Shoot();
  }
  void Shoot() {
    if (firingCoroutine == null && Input.GetButtonDown("Fire1")) {
      firingCoroutine = StartCoroutine(FireContinuously());
    } else if (firingCoroutine != null && Input.GetButtonUp("Fire1")) {
      StopCoroutine(firingCoroutine);
      firingCoroutine = null;
    }
  }
  IEnumerator FireContinuously() {
    while (true) {
      var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);

      if (shootSFX) {
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
      }

      yield return new WaitForSeconds(laserFirePeriod);
    }
  }
  void SetUpMoveBoundaries() {

    var spriteWidth = GetComponent<Renderer>().bounds.size.x;
    var spriteHeight = GetComponent<Renderer>().bounds.size.y;

    Camera gameCamera = Camera.main;
    xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + (spriteWidth / 2);
    xMax = gameCamera.ViewportToWorldPoint(new Vector3(1.0f, 0, 0)).x - (spriteWidth / 2);

    yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + (spriteHeight / 2);
    yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, yMaxRatio, 0)).y - (spriteHeight / 2);
  }
  void Move() {
    var xDelta = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
    var yDelta = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

    if (xDelta == 0 && yDelta == 0) return;

    var xNew = transform.position.x + xDelta;
    var yNew = transform.position.y + yDelta;
    transform.position = new Vector2(Mathf.Clamp(xNew, xMin, xMax), Mathf.Clamp(yNew, yMin, yMax));
  }
}
