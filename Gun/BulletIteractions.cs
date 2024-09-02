using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class BulletIteractions : MonoBehaviour
{

    [SerializeField] private FireBullet bulletInstance;
    // private GameObject[] bullets = null;
    private ArrayList bullets = null;

    private float hitDistace = 1f;
    private float groundCollideDistance = 0.2f;

    public event Action <Transform> OnGolemHit;

    private void Start()
    {
        bulletInstance.OnBulletInstantiation += AddBullet;
    }

    private void Update()
    {
        GameObject bullet;
        if (bullets != null)
        {
            for (global::System.Int32 i = 0; i < bullets.Count; i++)
            {
                bullet = (GameObject) bullets[i];
                HandleGolemHit(bullet);
                HandleGroundCollision(bullet);
            }
        } 
    }

    private void HandleGolemHit(GameObject bullet)
    {
        if (Physics.Raycast(bullet.transform.position, bullet.transform.forward, out RaycastHit raycastHit, hitDistace))
        {
            if (raycastHit.transform.TryGetComponent(out Health golemHealthScript))
            {
                OnGolemHit?.Invoke(raycastHit.transform);
                Destroy(bullet, 1f);
                bullet = null;
            }
        }
    }

    private void HandleGroundCollision(GameObject bullet)
    {
        
        if(Physics.Raycast(bullet.transform.position, -bullet.transform.up, out RaycastHit hitInfo, groundCollideDistance))
        {
            if(hitInfo.collider.tag == "Ground")
            {
                bullets.Remove(bullet);
                Destroy(bullet);
            }
        }
    }

    private void AddBullet(GameObject instance)
    {
        bullets.Add(instance);
    }
}
