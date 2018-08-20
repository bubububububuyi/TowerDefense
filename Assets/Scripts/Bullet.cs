using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _Target;

    public float Speed = 50f;
    /// <summary>
    /// 炸弹爆炸范围
    /// </summary>
    public float ExplosionRadius = 0f;

    public GameObject ImpactEffect;

    /// <summary>
    /// 伤害值
    /// </summary>
    public int DamageNumber = 50;

    // Use this for initialization
    void Start()
    {
		
    }
	
    // Update is called once per frame
    void Update()
    {
        if (_Target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 dir = _Target.position - transform.position;
        float distanceThisrame = Speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisrame)
        {
            HitTraget();
            return;
        } 
        transform.Translate(dir.normalized * distanceThisrame, Space.World);
        transform.LookAt(_Target);

    }

    private void HitTraget()
    {
        GameObject effect = (GameObject)Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(effect, 1.2f);

        if (ExplosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(_Target);
        }

        Destroy(this.gameObject);
    }

    public void Seek(Transform trans)
    {
        _Target = trans;
    }

    /// <summary>
    /// 判断炸弹的爆炸范围检测
    /// </summary>
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void Damage(Transform enemy)
    {
        var e = enemy.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(DamageNumber);
        }
//        Destroy(enemy.gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }

}
