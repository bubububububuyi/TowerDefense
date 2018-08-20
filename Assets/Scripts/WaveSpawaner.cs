using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawaner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;

    private float _TimeBetweenWaves = 5f;
    private float _Countdown = 2f;
    private int _WaveNumber = 1;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_Countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _Countdown = _TimeBetweenWaves;
        }
        _Countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < _WaveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        _WaveNumber++;
        if (_WaveNumber > 9)
        {
            _WaveNumber = 1;
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, new Vector3(5f, 3f, 5f), Quaternion.identity);
    }

}
