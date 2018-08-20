using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{

    private Transform _Target;

    private int _WavePointIndex = 0;

    private bool _IsChanage;

    private Enemy _Enemy;

    // Use this for initialization
    void Start()
    {
        _Enemy = GetComponent<Enemy>();
        _Target = Waypoints.Points[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_IsChanage)
        {
            Vector3 dir = _Target.position - transform.position;
            transform.Translate(dir.normalized * _Enemy.Speed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, _Target.position) < 0.2f && !_IsChanage)
        {
            _IsChanage = true;
            GetNextWaypoint();
        }
        _Enemy.Speed = _Enemy.StartSpeed;
    }

    private void GetNextWaypoint()
    {
        if (_WavePointIndex >= Waypoints.Points.Length - 1)
        {
            Debug.Log("the enemy is win");
            Destroy(this.gameObject);
            return;
        }
        _WavePointIndex++;
        _Target = Waypoints.Points[_WavePointIndex];
        _IsChanage = false;
    }

}
