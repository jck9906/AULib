using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    /// <summary>
    /// Rigidbody 확장 메서드
    /// https://monoflauta.com/2021/07/27/11-useful-unity-c-extension-methods/
    /// </summary>
    public static class RigidbodyExtension
    {

        /// <summary>
        /// 방향 전환
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="direction"></param>
        public static void ChangeDirection(this Rigidbody rb, Vector3 direction)
        {
            rb.velocity = direction.normalized * rb.velocity.magnitude;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rb"></param>
        /// <param name="magnitude"></param>
        public static void NormalizeVelocity(this Rigidbody rb, float magnitude = 1)
        {
            rb.velocity = rb.velocity.normalized * magnitude;
        }
    }
}