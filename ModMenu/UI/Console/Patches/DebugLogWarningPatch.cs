using HarmonyLib;
using UnityEngine;

namespace ModMenu.UI.Console.Patches
{
    [HarmonyPatch(typeof(Debug), "LogWarning", typeof(object))]
    public class DebugLogWarningPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(object message)
        {
            Console.Log(ConsoleTypes.Warning, "[UNITY LOG] " + message);
            return true;
        }
    }
}