using System;
using System.Collections;
using ModMenu.UpdateChecker;
using UnityEngine;

namespace ModMenu.UI.UpdateAvailable
{
    public class UpdateAvailableDialog : MonoBehaviour
    {
        bool newVersionAvailable;
        string newVersionString;

        private IEnumerator Start()
        {
            var updateProgress = UpdateCheck.GetLatestVersion();

            while (!updateProgress.IsCompleted)
            {
                yield return null;
            }

            if (updateProgress.Result.error)
            {
                Debug.LogError("[ModMenu]Failed to check for updates: " + updateProgress.Result.errorString);
                yield break;
            }

            if (updateProgress.Result.version > ModMenu.ModList.VERSION)
            {
                newVersionAvailable = true;
                newVersionString = updateProgress.Result.versionString;
            }
        }

        private void OnGUI()
        {
            if (!newVersionAvailable) return;

            var windowRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            GUI.Box(windowRect, "Update Available");

            GUILayout.BeginArea(new Rect(windowRect.x + 10, windowRect.y + 30, windowRect.width - 20,
                windowRect.height - 40));

            GUILayout.Label("A new version of Mod List is available!");
            GUILayout.Label("Current version: " + ModMenu.ModList.VERSION_STRING);
            GUILayout.Label("New version: " + newVersionString);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Download"))
            {
                Application.OpenURL("https://mldkyt.nekoweb.org/config/MonBazouModList/MonBazouModList.dll");
            }

            if (GUILayout.Button("Close"))
            {
                Destroy(gameObject);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }
    }
}