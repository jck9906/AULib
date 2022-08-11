using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AULib
{
    public abstract class MonoSingletonBase<T> : BaseBehaviour where T : MonoSingletonBase<T>
    {
        protected static T s_instance = null;

        public static bool IsValid()
        {
            return s_instance ? true : false;
        }

        public static T i { get { return s_instance; } }


        protected override void Awake()
        {
            base.Awake();
            if ( s_instance == null )
            {
                s_instance = this as T;
                s_instance.Init();
            }
            else
            {
                Destroy( this.gameObject );
            }
        }

        public virtual void Init() { }

    }
}

