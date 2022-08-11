using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AULib
{
    public class AUUtility
    {
        static public Color IntToColor(int v)
        {
            Color c;
            c.b = (byte)((v) & 0xFF);
            c.g = (byte)((v >> 8) & 0xFF);
            c.r = (byte)((v >> 16) & 0xFF);
            c.a = (byte)((v >> 24) & 0xFF);

            return c; // RGBA
        }

        static public int ColorToInt(byte r, byte g, byte b, byte a)
        {
            return b + (g << 8) + (r << 16) + (a << 24);
        }

        //static public int ColorToInt( float r, float g, float b, float a )
        //{
        //    return ( r * 255  << 24 ) + ( g * 255 << 16 ) + ( b * 255 << 8 ) + ( a * 255 << 24 ) ;
        //}


        // 각도를 -90 ~ 90 사이로 변환.
        public static float ClampAngle(float angle)
        {
            float result = angle - Mathf.CeilToInt(angle / 360f) * 360f;
            if (result < -90)
            {
                result += 360f;
            }
            return result;
        }

    }
}