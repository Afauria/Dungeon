using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T instance { get; protected set; }

    public static bool instanceExists
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (instanceExists)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
