using System.Runtime.CompilerServices;
using UnityEngine;

public static class Logger
{
#if UNITY_EDITOR 
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.DebuggerStepThrough]
    public static void Log(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "")
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.Log($"[{className}.{memberName}] {message}");
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    [System.Diagnostics.DebuggerStepThrough]
    public static void Warning(
        string message,
        [CallerFilePath] string filePath = "",
        [CallerMemberName] string memberName = "")
    {
        string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
        Debug.LogWarning($"[{className}.{memberName}] {message}");
    }
#endif
}