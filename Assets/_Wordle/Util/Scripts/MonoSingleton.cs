using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {

    private static T instance;

    public static T Instance {
        get {
            return instance;
        }
    }

    public static bool IsInitialised {
        get {
            return instance != null;
        }
    }

    protected virtual void Awake() {
        if (instance != null) {
            Debug.LogError("[Singleton] trying to instantiate a second instance if a singleton class");
        } else {
            Debug.Log("Instantiating this" + ((T)this).name);
            instance = (T)this;
        }
    }

    protected virtual void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }
}

