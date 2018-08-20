using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform _Target;
    public float Range = 15f;
    public string EnemyTag = "Enemy";

    /// <summary>
    /// 旋转对象
    /// </summary>
    public Transform PartToRotate;
    private float _TurndSpeed = 10f;
   
    public float _FireRate = 1f;
    private float _FireCountdown = 0f;

    //子弹
    public GameObject BulletPrefab;
    public Transform FirePoint;

    [Header("use laser")]
    public bool UseLaser = false;
    public LineRenderer LineRenderer;
    public ParticleSystem LaserEffect;
    public int DamageOverTime = 30;
    /// <summary>
    /// 减速speed
    /// </summary>
    public float SlowPct = 0.5f;

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

//        DrawCircle(transform, transform.position, Range);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
//        if (_Target == null)
        {
            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortesDistance)
                {
                    shortesDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy != null && shortesDistance <= Range)
        {
            _Target = nearestEnemy.transform;
        }
        else
        {
            _Target = null;
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_Target == null)
        {
            if (UseLaser)
            {
                if (LineRenderer.enabled)
                {
                    LineRenderer.enabled = false;
                    LaserEffect.Stop();
                }
            }
            return;
        }
        
        LookOnTarget();

        if (UseLaser)
        {
            if (_Target != null)
            {
                Laser();
            }
        }
        else
        {
            if (_FireCountdown <= 0f)
            {
                Shoot();
                _FireCountdown = 1f / _FireRate;
            }
            _FireCountdown -= Time.deltaTime;
        }


    }

    private void Laser()
    {
        _Target.GetComponent<Enemy>().TakeDamage(DamageOverTime * Time.deltaTime);
        _Target.GetComponent<Enemy>().Slow(SlowPct);
        if (!LineRenderer.enabled)
        {
            LineRenderer.enabled = true;
            LaserEffect.Play();
        }
        var pos = _Target.position + new Vector3(0f, 0f, 0f);
        LineRenderer.SetPosition(0, FirePoint.position);
        LineRenderer.SetPosition(1, pos);

        Vector3 dir = FirePoint.position - pos;

        LaserEffect.transform.position = pos + dir.normalized * 0.5f;
        LaserEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LookOnTarget()
    {
        var pos = _Target.position + new Vector3(0f, 0f, 0f);
        Vector3 dir = pos - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(PartToRotate.rotation, lookRotation, Time.deltaTime * _TurndSpeed).eulerAngles;
        PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    private void Shoot()
    {
        GameObject go = (GameObject)Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = go.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(_Target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

}
