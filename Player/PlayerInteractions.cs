using System;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public event Action OnShootEvent;
    public event Action OnScopeClickEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            OnShootEvent?.Invoke();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("SCOPE BOY!");
            OnScopeClickEvent?.Invoke();
        }
    }
}
