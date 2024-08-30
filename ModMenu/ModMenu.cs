using System;
using BepInEx;
using Michsky.MUIP;
using ModMenu.UI.ModList;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

namespace ModMenu
{
    [BepInPlugin("me.mldkyt.monbazoumodmenu", "Mod Menu", "1.0")]
    public class ModMenu : BaseUnityPlugin
    {
        public List modList;
        
        void Awake()
        {
            Debug.Log("[ModMenu]Welcome!");
            SceneManager.sceneLoaded += (arg0, mode) =>
            {
                if (arg0.buildIndex == 1)
                {
                    Debug.Log("[ModMenu]Loading Main Menu stuff...");
                    OnMainMenuLoad();
                }
            };
        }

        private void OnMainMenuLoad()
        {
            var changelogsBtn = GameObject.Find("MainMenu_Manager/Canvas/Right Menu/Button_Changelogs");
            var changelogsBtnManager = changelogsBtn.GetComponent<ButtonManager>();

            Debug.Log("[ModMenu]Spawn duplicate of \"Load Game\" button");
            // Spawn duplicate of "Load Game" button
            var loadGameBtn = GameObject.Find("MainMenu_Manager/Canvas/Right Menu/Button_LoadGame");
            var modMenuBtn = GameObject.Instantiate(loadGameBtn);
            modMenuBtn.name = "Button_ModMenu";
            modMenuBtn.transform.parent = loadGameBtn.transform.parent;
            modMenuBtn.transform.localScale = Vector3.one;
            modMenuBtn.transform.SetSiblingIndex(4);
            
            Debug.Log("[ModMenu]Set the text and triggers");
            // Set the text
            var modMenuBtnManager = modMenuBtn.GetComponent<ButtonManager>();
            modMenuBtnManager.SetText("MOD MENU");
            modMenuBtnManager.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
            modMenuBtnManager.onClick.AddListener(OpenModMenu);
            
            // Set icon (So it doesn't look similar to the other button)
            modMenuBtnManager.SetIcon(changelogsBtnManager.buttonIcon);
            
            // Disable localization to prevent funny stuff
            modMenuBtn.GetComponent<LocalizeStringEvent>().enabled = false;

            // Init mod list

            if (modList == null)
            {
                var go = new GameObject("Mod List");
                modList = go.AddComponent<List>();
            }
            
            void OpenModMenu()
            {
                modList.showList = true;
            }
        }
    }
}