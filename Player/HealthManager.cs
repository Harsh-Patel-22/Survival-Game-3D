using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Text healthLabel;
    [SerializeField] private Image healthBar;
    [SerializeField] private AnimationHandler golemAnimationHandler;

    private RectTransform healthBarRectTransform;
    private float health = 100f;
    private float golemDamage = 15f;
    private void Start()
    {
        golemAnimationHandler.OnHit += TakeDamage;
        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
    }

    private void TakeDamage()
    {
        health -= golemDamage;
        healthBarRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, health);
        healthLabel.text = health.ToString();
        if(health <= 0)
        {
            Debug.Log("GameOVER");
        }
    }
}
