using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Service : MonoBehaviour
{
    protected virtual void Awake()
    {
        ServiceLocator.RegisterService(this);
    }

    protected virtual void OnDestroy()
    {
        ServiceLocator.UnregisterService(this);
    }
}
