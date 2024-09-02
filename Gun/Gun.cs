using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private Text currentBulletsText;
    [SerializeField] private Text totalBulletsText;
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private Animator animator;
    [SerializeField] private FireBullet fireBullet;


    private Quaternion tempRotation;

    private int magazineSize = 6;
    // private string gunType = "revolver";
    private int maxAmmo = 999, currentAmmo = 6;

    private void Start()
    {
        currentBulletsText.text = currentAmmo.ToString();
        totalBulletsText.text = maxAmmo.ToString();
        playerInteractions.OnShootEvent += shoot;
        fireBullet.OnReloadAnimationCompletion += reload;
    }

    private void Update()
    {
        tempRotation = Quaternion.Slerp(transform.rotation, orientation.rotation, Time.deltaTime);
        transform.rotation = tempRotation;
    }

    private void shoot()
    {
        if (!animator.GetBool("IsShooting") && !animator.GetBool("IsReloading"))
        {
            animator.SetBool("IsShooting", true);
            

            currentAmmo -= 1;
            if (currentAmmo <= 0)
            {
                animator.SetBool("IsReloading", true);
            }
            UpdateAmmoText();
        }
    }

    private void reload(object sender, EventArgs e)
    {
        animator.SetBool("IsReloading", false);
        Debug.Log("Reloaded!");
        maxAmmo -= (magazineSize - currentAmmo);
        currentAmmo = 6;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        currentBulletsText.text = currentAmmo.ToString();
        totalBulletsText.text = maxAmmo.ToString();
    }
}
