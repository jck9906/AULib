using UnityEngine;
using System;
using IFDEF = System.Diagnostics.ConditionalAttribute;


public class Debug
{

    static string TAG
    {
        get
        {
            return "[" + DateTime.Now + "]";
        }
    }
    

    [IFDEF("ENABLE_LOG")]
    public static void Log(Type type, object text, string color = "black") => UnityEngine.Debug.Log($"{TAG} <color={color}><b>{type.Name}</b>  {text}</color>");


    [IFDEF("ENABLE_LOG")]
    public static void Logf(string format, params object[] args) => UnityEngine.Debug.Log(string.Format($"{TAG} {format}", args));


    [IFDEF("ENABLE_LOG")]
    public static void Log(string text) => UnityEngine.Debug.Log($"{TAG} {text}");


    [IFDEF("ENABLE_LOG")]
    public static void Log(string text, UnityEngine.Object context)
    {
        Debug.Log(TAG + text, context);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogError(string text)
    {
        UnityEngine.Debug.LogError(TAG + text);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogError(string text, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogError(TAG + text, context);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogErrorFormat(string text, params object[] args)
    {
        UnityEngine.Debug.LogErrorFormat(TAG + text, args);      
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogWarning(string text)
    {       
        UnityEngine.Debug.LogWarning(TAG + text);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogWarning(string text, UnityEngine.Object context)
    {      
        UnityEngine.Debug.LogWarning(TAG + text, context);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogWarningFormat(string text, params object[] args)
    {
        UnityEngine.Debug.LogWarningFormat(TAG + text, args);        
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogException(System.Exception exception)
    {
        UnityEngine.Debug.LogException(/*TAG + */exception);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogException(System.Exception exception, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogException(/*TAG + */exception, context);
    }

    [IFDEF("ENABLE_LOG")]
    public static void Logf(object format, params object[] args)
    {
        UnityEngine.Debug.Log(string.Format(TAG + format.ToString(), args));
    }

    [IFDEF("ENABLE_LOG")]
    public static void Log(object text)
    {
        UnityEngine.Debug.Log(TAG + text.ToString());
    }

    [IFDEF("ENABLE_LOG")]
    public static void Log(object text, UnityEngine.Object context)
    {
        UnityEngine.Debug.Log(TAG + text.ToString(), context);
    }


    [IFDEF("ENABLE_LOG")]
    public static void LogError(object text)
    {
        UnityEngine.Debug.LogError(TAG + text.ToString());
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogError(object text, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogError(TAG + text.ToString(), context);
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogWarning(object text)
    {
        UnityEngine.Debug.LogWarning(TAG + text.ToString());
    }

    [IFDEF("ENABLE_LOG")]
    public static void LogWarning(object text, UnityEngine.Object context)
    {
        UnityEngine.Debug.LogWarning(TAG + text.ToString(), context);
    }



    [IFDEF("ENABLE_LOG")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration) => UnityEngine.Debug.DrawRay(start, dir, color, duration);
}