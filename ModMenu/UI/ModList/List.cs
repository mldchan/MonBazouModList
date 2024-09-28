using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using ModList.UI.ModList.Settings.Types;
using ModMenu.UI.About;
using ModMenu.UI.ModList.Settings;
using ModMenu.UI.ModList.Settings.Types;
using ModMenu.UI.ModList.Structs;
using UnityEngine;
using UnityEngine.Serialization;

namespace ModMenu.UI.ModList
{
    public class List : MonoBehaviour
    {
        [FormerlySerializedAs("showModLoader")]
        public bool showList;

        public BaseUnityPlugin selectedMod;
        public Tab selectedTab;

        public InfoTabStruct InfoTabStruct;
        public SettingsTabStruct SettingsTabStruct;

        public List<PluginInfo> mods;

        public int page;
        public int windowHeight;
        public int windowWidth;
        public int windowX;
        public int windowY;

        private void Awake()
        {
            mods = Chainloader.PluginInfos.Values.ToList();
        }

        private void OnGUI()
        {
            if (!showList) return;

            windowHeight = Mathf.Clamp(Screen.height - 200, 200, Screen.height);
            windowWidth = Mathf.Clamp(Screen.width - 1000, 200, Screen.width);
            windowX = Mathf.Clamp((Screen.width - windowWidth) / 2, 0, Screen.width - windowWidth);
            windowY = Mathf.Clamp(100, 0, Screen.height - windowHeight);
            if (selectedMod != null)
            {
                switch (selectedTab)
                {
                    case Tab.Info:
                        RenderAdvModInfo();
                        break;
                    case Tab.Settings:
                        RenderModSettings();
                        break;
                }

                return;
            }
            else if (SettingsTabStruct.hasInitialized)
            {
                SettingsTabStruct.hasInitialized = false;
                SettingsTabStruct.settings.Clear();
            }

            RenderModList();
        }

        private void RenderModSettings()
        {
            // Support Boolean, String, Integer, Float
            // More types of settings may be added in the future

            if (!SettingsTabStruct.hasInitialized)
            {
                InitializeSettingsTab();
            }

            GUILayout.BeginArea(new Rect(windowX, windowY, windowWidth, windowHeight), "Mod Settings", GUI.skin.box);

            if (GUILayout.Button("Back", GUILayout.Width(100)))
            {
                selectedMod = null;
                selectedTab = Tab.Info;
                GUILayout.EndArea();
                return;
            }

            var category = "";
            foreach (var setting in SettingsTabStruct.settings)
            {
                if (category != setting.Category)
                {
                    GUILayout.Label($"[{setting.Category}]", GUILayout.Width(windowWidth - 20));
                    category = setting.Category;
                }

                setting.RenderSetting();
            }

            GUILayout.EndArea();
        }

        private void InitializeSettingsTab()
        {
            SettingsTabStruct.settings = new List<Setting>();
            foreach (var config in selectedMod.Config)
            {
                // I wanna cry but I couldn't write a switch statement for this
                if (config.Value.SettingType == typeof(Boolean))
                {
                    if (config.Key.Key == "Enabled" && config.Key.Section == "General")
                    {
                        continue;
                    }
                    
                    var boolSetting = new BooleanSetting();
                    boolSetting.Initialize(config.Key.Key, config.Value.BoxedValue,
                        config.Value.Description.Description, config.Key.Section);
                    boolSetting.ValueUpdated += (sender, o) => { config.Value.BoxedValue = o; };
                    SettingsTabStruct.settings.Add(boolSetting);
                }

                else if (config.Value.SettingType == typeof(String))
                {
                    var stringSetting = new StringSetting();
                    stringSetting.Initialize(config.Key.Key, config.Value.BoxedValue,
                        config.Value.Description.Description, config.Key.Section);
                    stringSetting.ValueUpdated += (sender, o) => { config.Value.BoxedValue = o; };
                    SettingsTabStruct.settings.Add(stringSetting);
                }

                else if (config.Value.SettingType == typeof(Int32))
                {
                    var intSetting = new IntegerSetting();
                    intSetting.Initialize(config.Key.Key, config.Value.BoxedValue, config.Value.Description.Description,
                        config.Key.Section);
                    intSetting.ValueUpdated += (sender, o) => { config.Value.BoxedValue = o; };
                    SettingsTabStruct.settings.Add(intSetting);
                }

                else if (config.Value.SettingType == typeof(Single))
                {
                    var floatSetting = new FloatSetting();
                    floatSetting.Initialize(config.Key.Key, config.Value.BoxedValue,
                        config.Value.Description.Description, config.Key.Section);
                    floatSetting.ValueUpdated += (sender, o) => { config.Value.BoxedValue = o; };
                    SettingsTabStruct.settings.Add(floatSetting);
                }
            }

            SettingsTabStruct.hasInitialized = true;
            SettingsTabStruct.settings = SettingsTabStruct.settings.OrderBy(setting => setting.Category).ToList();
        }

        private void RenderAdvModInfo()
        {
            var metadata = selectedMod.Info.Metadata;
            GUILayout.BeginArea(new Rect(windowX, windowY, windowWidth, windowHeight), "Mod Information", GUI.skin.box);

            if (GUILayout.Button("Back", GUILayout.Width(100)))
            {
                selectedMod = null;
                selectedTab = Tab.Info;
                return;
            }

            GUILayout.Label($"{metadata.Name}");
            GUILayout.Label($"{metadata.GUID}");
            GUILayout.Label($"Version {metadata.Version}");

            if (InfoTabStruct.showAdvancedInformation)
            {
                GUILayout.Label("Dependencies:");
                AdvancedInfoDependencies();

                if (GUILayout.Button("Hide Advanced"))
                {
                    InfoTabStruct.showAdvancedInformation = false;
                }
            }
            else
            {
                if (GUILayout.Button("Show Advanced"))
                {
                    InfoTabStruct.showAdvancedInformation = true;
                }
            }

            GUILayout.EndArea();
        }

        private void AdvancedInfoDependencies()
        {
            if (InfoTabStruct.expandDependencies)
            {
                foreach (var dependency in selectedMod.Info.Dependencies)
                {
                    GUILayout.BeginHorizontal();
                    var required = (dependency.Flags & BepInDependency.DependencyFlags.HardDependency) != 0
                        ? "(required)"
                        : "(optional)";

                    GUILayout.Label($"Dependency: {dependency.DependencyGUID} {required}", GUILayout.Width(200));
                    GUILayout.Label($"Minimum version: {dependency.MinimumVersion}", GUILayout.Width(200));
                    GUILayout.EndHorizontal();
                    GUILayout.Space(4);
                }

                if (GUILayout.Button("(collapse)"))
                {
                    InfoTabStruct.expandDependencies = false;
                }
            }
            else
            {
                if (GUILayout.Button("(expand)"))
                {
                    InfoTabStruct.expandDependencies = true;
                }
            }
        }

        private void RenderModList()
        {
            var w = Screen.width / 2 - 200;
            var h = Screen.height - 200;
            GUILayout.BeginArea(new Rect(windowX, windowY, windowWidth, windowHeight), GUI.skin.box);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("About Mod List", GUILayout.Width(100)))
            {
                AboutScreen.Instance.show = true;
            }

            GUILayout.Space(windowWidth - 150);

            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                showList = false;
            }

            GUILayout.EndHorizontal();
            GUILayout.Label("Mod List");

            foreach (var mod in mods)
            {
                RenderBasicModInfo(mod);
            }

            GUILayout.EndArea();
        }

        private void RenderBasicModInfo(PluginInfo mod)
        {
            GUILayout.Label($"{mod.Metadata.Name} <color=gray>({mod.Metadata.GUID}) {mod.Metadata.Version}</color>");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Information", GUILayout.Width(100)))
            {
                selectedMod = mod.Instance;
                selectedTab = Tab.Info;
            }

            if (GUILayout.Button("Settings", GUILayout.Width(100)))
            {
                selectedMod = mod.Instance;
                selectedTab = Tab.Settings;
            }

            // Enabled switch
            if (mod.Instance.Config.Any(x => x.Key.Key == "Enabled" && x.Value.SettingType == typeof(Boolean)))
            {
                mod.Instance.Config[new ConfigDefinition("General", "Enabled")].BoxedValue = GUILayout.Toggle(
                    (bool) mod.Instance.Config[new ConfigDefinition("General", "Enabled")].BoxedValue, "Enabled");
            }

            GUILayout.EndHorizontal();
        }
    }
}