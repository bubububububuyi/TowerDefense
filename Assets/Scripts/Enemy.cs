using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float StartSpeed = 10f;
    [HideInInspector]
    public float Speed;

    /// <summary>
    /// 生命值
    /// </summary>
    public float StartHealth = 1000f;
    private float _CurrHealth;
    /// <summary>
    /// 死亡所得金钱
    /// </summary>
    public int Worth = 50;

    public GameObject DeathEffect;

    [Header("UI")]
    public Slider HealthSlider;
    public Transform CanvasTrans;
    public Image HeadImage;
    public Sprite[] Sprites;
    public Text HPText;

    void Start()
    {
        Speed = StartSpeed;
        _CurrHealth = StartHealth;
        HealthSlider.value = _CurrHealth / StartHealth;
        HPText.text = string.Format("{0}/{1}", (int)_CurrHealth, (int)StartHealth);
        HeadImage.sprite = Sprites[Random.Range(0, Sprites.Length)];
    }

    void Update()
    {
//        CanvasTrans.LookAt(Camera.main.transform);
        CanvasTrans.rotation = Camera.main.transform.rotation * Quaternion.FromToRotation(new Vector3(0, 1, 0), new Vector3(0, 1, 0));
    }

    /// <summary>
    /// 改变当前血量值
    /// </summary>
    /// <param name="amount">Amount.</param>
    public void TakeDamage(float amount)
    {
        _CurrHealth -= amount;
        HealthSlider.value = _CurrHealth / StartHealth;
        HPText.text = string.Format("{0}/{1}", (int)_CurrHealth, (int)StartHealth);
        if (_CurrHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);
        
        ClientPlayer.Money += Worth;
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 减速效果
    /// </summary>
    /// <param name="slowAmount">Slow amount.</param>
    public void Slow(float slowAmount)
    {
        Speed = StartSpeed * (1f - slowAmount);
    }



}
