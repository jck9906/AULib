using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace AULib
{

    /// <summary>
    /// Attibute »Æ¿Â - ReadOnly
    /// </summary>

    [AttributeUsage(AttributeTargets.Field)]
    public class ReadOnlyAttribute : PropertyAttribute
    {
        public readonly bool runtimeOnly;

        public ReadOnlyAttribute(bool runtimeOnly = false)
        {
            this.runtimeOnly = runtimeOnly;
        }
    }

}