using HarmonyLib;
using UnityEngine;

namespace ModMenu.UI.Console.Patches
{
    [HarmonyPatch(typeof(Debug), "Log", typeof(object))]
    public class DebugLogPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(object message)
        {
            Console.Log(ConsoleTypes.Log, $"[UNITY LOG] {message}");
            return true;
        }
    }
}