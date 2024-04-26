using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoPoker
{
    //-//////////////////////////////////////////////////////////////////////
    ///
    /// Parent class to make sure all scripts are started and updated in proper sequence
    /// 
    public class Branch : MonoBehaviour
    {
        protected bool initialized = false;
        protected virtual void Initialize()
        {
            initialized = true;
        }


        public virtual void Tick(float delta)
        {
            if (!initialized)
            {
                Initialize();
            }
        }

    }
}
