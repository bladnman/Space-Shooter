using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

  WaveConfig waveConfig;

  List<Transform> waypoints;
  int waypointIndex = 0;

  void Start() {
    if (waveConfig) {
      waypoints = waveConfig.GetWaypoints();
      transform.position = waypoints[waypointIndex].position;
    }
  }
  void Update() {
    Move();
  }
  public void SetWaveConfig(WaveConfig waveConfig) {
    this.waveConfig = waveConfig;
  }

  void Move() {
    if (!waveConfig) return;
    if (waypoints == null || waypoints.Count < 1) return;
    // move through points
    if (waypointIndex <= waypoints.Count - 1) {
      var targetPos = waypoints[waypointIndex].position;
      var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
      transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);
      if (transform.position.x == targetPos.x && transform.position.y == targetPos.y) {
        waypointIndex++;
      }
    }

    // no more - destroy
    else {
      Destroy(gameObject);
    }
  }
}
