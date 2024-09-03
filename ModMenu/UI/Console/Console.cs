using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModMenu.UI.Console
{
    public class Console : MonoBehaviour
    {
        public static Console Instance { get; set; }
        public bool IsOpen { get; set; }

        private List<string> _entries = new List<string>();

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown("`"))
            {
                IsOpen = !IsOpen;
                Debug.Log("[ModList]Console: " + IsOpen);
                Log(ConsoleTypes.Log, $"[INTERNAL] Console: {IsOpen}");
            }
        }

        private void OnGUI()
        {
            if (!IsOpen) return;
            
            GUI.Box(new Rect(10, 10, 1000, 400), "Console");
            
            for (var i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Contains("[WARNING]")) GUI.color = Color.yellow;
                if (_entries[i].Contains("[ERROR]")) GUI.color = Color.red;
                
                GUI.Label(new Rect(20, 30 + i * 20, 1000, 20), _entries[i]);
                
                GUI.color = Color.white;
            }
        }
        
        public static void Log(ConsoleTypes type, string message)
        {
            if (Instance == null) return;
            if (Instance._entries.Count > 16)
                Instance._entries.RemoveAt(0);
            Instance._entries.Add($"[{DateTime.Now:yyyy/MM/dd (dddd) HH:mm:ss}] [{type.ToString().ToUpper()}] {message}");
        }
    }
}