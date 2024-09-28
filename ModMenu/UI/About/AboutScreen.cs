using System;
using UnityEngine;

namespace ModMenu.UI.About
{
    public class AboutScreen : MonoBehaviour
    {
        public static AboutScreen Instance { get; private set; }

        public bool show = false;

        private void Start()
        {
            if (Instance != null)
            {
                Debug.LogError("Another instance of AboutScreen already exists, destroying this one.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnGUI()
        {
            if (!show) return;

            GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300), "About",
                GUI.skin.box);

            GUILayout.Space(10);
            
            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                show = false;
            }

            GUILayout.Label("<color=white>Mod List by mldchan</color>",
                new GUIStyle() { fontSize = 20, fontStyle = FontStyle.Bold });
            GUILayout.Label($"<color=white>Version {ModMenu.ModList.VERSION_STRING}</color>",
                new GUIStyle() { fontSize = 15 });
            GUILayout.Label(
                "This program comes with ABSOLUTELY NO WARRANTY; for details, see the GNU General Public License.");
            GUILayout.Label("This is free software, and you are welcome to redistribute it under certain conditions.");

            if (GUILayout.Button("Source Code"))
            {
                Application.OpenURL("https://github.com/mldchan/MonBazouModList");
            }

            if (GUILayout.Button("About the Developer"))
            {
                Application.OpenURL("https://femboy.bio/mldchan");
            }

            GUILayout.EndArea();
        }
    }
}