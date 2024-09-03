using HarmonyLib;
using UnityEngine;

namespace ModMenu.UI.Console.Patches
{
    [HarmonyPatch(typeof(Debug), "LogError", typeof(object))]
    public class DebugLogErrorPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(object message)
        {
            Console.Log(ConsoleTypes.Error, $"[UNITY LOG] {message}");
            return true;
        }
    }
}