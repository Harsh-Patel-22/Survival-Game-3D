using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeHandler : MonoBehaviour
{
    [SerializeField] private PlayerInteractions playerInteractions;
    [SerializeField] private Camera scopeViewCamera;

    private bool scopeEnabled = false;

    private void Start()
    {
        scopeViewCamera.enabled = false;
        playerInteractions.OnScopeClickEvent += HandleScope;
    }

    private void HandleScope()
    {
        if (scopeEnabled)
        {
            scopeViewCamera.enabled = true;

        }
        else
        {
            scopeViewCamera.enabled = false;

        }
        scopeEnabled = !scopeEnabled;
    }

}
