using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    protected bool initialized = false;
    protected virtual void Initialize() {
        initialized = true;
    }
    public virtual void Tick(float delta) {
        if (!initialized) { 
            Initialize();
        }
    }


}
