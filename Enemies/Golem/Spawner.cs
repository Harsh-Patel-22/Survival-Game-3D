using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform[] transforms;
    [SerializeField] float spawnTimeDifference;

    private void Spawn()
    {
        int randomNumber = Random.Range(0, transforms.Length);
        Transform randomTransform = transforms[randomNumber];
        Instantiate(prefab, randomTransform.position, randomTransform.rotation);
    }

    private void Start()
    {
        InvokeRepeating("Spawn", 2f, spawnTimeDifference);
    }
}
