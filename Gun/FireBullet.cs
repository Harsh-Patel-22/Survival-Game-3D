using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private AudioSource shootAudioSource;
    [SerializeField] private AudioSource reloadAudioSource;

    GameObject bullet;
    Rigidbody rb;
    private float bulletFireForce = 50f;
    private Animator animator;

    public event EventHandler OnReloadAnimationCompletion;
    public event Action <GameObject> OnBulletInstantiation;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BulletPrefabInit()
    {
        bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        bullet.transform.rotation = transform.parent.rotation;

        OnBulletInstantiation?.Invoke(bullet);

        PlaySound(shootAudioSource);
        
        rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(-transform.right * bulletFireForce, ForceMode.Impulse);
    }

    private void PlaySound(AudioSource audioSource)
    {
        audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        audioSource.Play();

    }

    public void OnShootFinish()
    {
        animator.SetBool("IsShooting", false);
    }

    public void OnReloadFinish()
    {
        OnReloadAnimationCompletion?.Invoke(this, EventArgs.Empty);
        PlaySound(reloadAudioSource);
    }
}