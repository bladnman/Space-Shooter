using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  [SerializeField] List<WaveConfig> waveConfigs;
  [SerializeField] int startingWave = 0;
  [SerializeField] bool looping = false;

  IEnumerator Start() {
    do {
      yield return StartCoroutine(SpawnAllWaves());
    } while (looping);
  }

  private IEnumerator SpawnAllWaves() {
    foreach (var waveConfig in waveConfigs) {
      yield return StartCoroutine(SpawnAllEnemiesInWave(waveConfig));
    }
  }
  private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig) {
    for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {

      var enemy = Instantiate(
        waveConfig.GetEnemyPrefab(),
        waveConfig.GetWaypoints()[0].transform.position,
        Quaternion.identity
      );

      enemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

      var rnd = UnityEngine.Random.Range(0f, waveConfig.GetSpawnRandomFactor());
      yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns() + rnd);
    }
  }
}
