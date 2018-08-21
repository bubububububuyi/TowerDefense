using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawaner : MonoBehaviour
{
    public Wave[] Waves;

    private float _TimeBetweenWaves = 10f;
    private float _Countdown = 10f;
    private int _WaveNumber;

    private bool IsNext;

    public Text TimeText;

    // Use this for initialization
    void Start()
    {
        IsNext = true;
        _WaveNumber = 0;
        _Countdown = _TimeBetweenWaves;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (IsNext)
        {
            if (_Countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                _Countdown = _TimeBetweenWaves;
            }
            _Countdown -= Time.deltaTime;
            _Countdown = Mathf.Clamp(_Countdown, 0f, Mathf.Infinity);
            TimeText.text = string.Format("{00:00.00}", _Countdown);
        }
    }

    IEnumerator SpawnWave()
    {
        IsNext = false;
        Wave wave = Waves[_WaveNumber];
        
        for (int i = 0; i < wave.Count; i++)
        {
            SpawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1f / wave.Rate);
            if (i == wave.Count - 1)
            {
                IsNext = true;
            }
        }

        _WaveNumber++;
        if (_WaveNumber >= Waves.Length)
        {
            _WaveNumber = 0;
        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, new Vector3(5f, 3f, 5f), Quaternion.identity);
    }

}
