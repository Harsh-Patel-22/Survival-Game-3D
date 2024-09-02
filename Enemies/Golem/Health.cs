using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private GameObject g;
    private BulletIteractions bullet;

    // private Image healthBar;
    // private RectTransform healthBarRectTransform;

    private float health;
    private float bulletDamage;

    private void Awake()
    {
        g = GameObject.FindGameObjectWithTag("BulletInteractions");
        bullet = g.GetComponent<BulletIteractions>();

        // g = GameObject.FindGameObjectWithTag("GolemHealth");
        // healthBar = g.GetComponent<Image>();
    }

    private void Start()
    {
        // healthBar.enabled = false;
        // healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        health = 100f;
        bulletDamage = 10f;
        bullet.OnGolemHit += HandleHit;
    }


    private void HandleHit(Transform _)
    {
        health -= bulletDamage;
        Debug.Log(health);
        // healthBarRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health);
        // healthBarRectTransform.position = transform.up * 7f;
        // healthBar.enabled = true;

        if (health <= 0)
        {
            Debug.Log("Killed");
            bullet.OnGolemHit -= HandleHit;
            Destroy(gameObject);
        }
    }
}
