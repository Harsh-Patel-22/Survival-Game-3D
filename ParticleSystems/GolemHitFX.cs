using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemHitFX : MonoBehaviour
{
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private BulletIteractions _bulletIteractions;
    private void Start()
    {
        _bulletIteractions.OnGolemHit += PlayParticleEffect;
    }

    private void PlayParticleEffect(Transform obj)
    {
        Debug.Log("Partticles!!!");
        Instantiate(particlePrefab, obj.position, obj.rotation);
    }
}
