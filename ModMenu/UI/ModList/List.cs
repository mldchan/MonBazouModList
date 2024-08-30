using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Bootstrap;
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
        public InfoTab selectedTab;

        public InfoTabStruct InfoTabStruct = new InfoTabStruct();

        public List<PluginInfo> mods;

        public int page;
        public int windowHeight;
        public int windowWidth;

        private void Awake()
        {
            mods = Chainloader.PluginInfos.Values.ToList();
        }

        private void OnGUI()
        {
            if (!showList) return;

            windowHeight = Screen.height - 200;
            windowWidth = Screen.width - 500; // Leave space for the buttons on the right
            if (selectedMod != null)
            {
                switch (selectedTab)
                {
                    case InfoTab.Info:
                        RenderAdvModInfo();
                        break;
                }

                return;
            }

            RenderModList();
        }

        private void RenderAdvModInfo()
        {
            var metadata = selectedMod.Info.Metadata;
            GUI.Box(new Rect(100, 100, windowWidth, windowHeight),
                $"Mod Information - {metadata.Name} {metadata.Version}");

            GUI.Label(new Rect(110, 110, windowWidth - 20, 20), $"{metadata.Name}");
            GUI.Label(new Rect(110, 130, windowWidth - 20, 20), $"{metadata.GUID}");
            GUI.Label(new Rect(110, 150, windowWidth - 20, 20), $"Version {metadata.Version}");

            if (InfoTabStruct.showAdvancedInformation)
            {
                int y = 170;
                GUI.Label(new Rect(110, y, windowWidth - 20, 20), "Dependencies:");
                y += 20;
                AdvancedInfoDependencies(ref y);

                if (GUI.Button(new Rect(110, y, windowWidth - 20, 20), "Hide Advanced"))
                {
                    InfoTabStruct.showAdvancedInformation = false;
                }
            }
            else
            {
                if (GUI.Button(new Rect(110, 170, windowWidth - 20, 20), "Show Advanced"))
                {
                    InfoTabStruct.showAdvancedInformation = true;
                }
            }
        }

        private void AdvancedInfoDependencies(ref int y)
        {
            if (InfoTabStruct.expandDependencies)
            {
                foreach (var dependency in selectedMod.Info.Dependencies)
                {
                    var required = (dependency.Flags & BepInDependency.DependencyFlags.HardDependency) != 0
                        ? "(required)"
                        : "(optional)";

                    GUI.Label(new Rect(120, y, windowWidth - 20, 20),
                        $"Dependency: {dependency.DependencyGUID} {required}");
                    y += 20;
                    GUI.Label(new Rect(120, y, windowWidth - 20, 20), $"Minimum version: {dependency.MinimumVersion}");
                    y += 20;
                }

                if (GUI.Button(new Rect(120, y, windowWidth - 20, 20), "(collapse)"))
                {
                    InfoTabStruct.expandDependencies = false;
                }

                y += 20;
            }
            else
            {
                if (GUI.Button(new Rect(120, y, windowWidth - 20, 20), "(expand)"))
                {
                    InfoTabStruct.expandDependencies = true;
                }

                y += 20;
            }
        }

        private void RenderModList()
        {
            GUI.Box(new Rect(100, 100, windowWidth, windowHeight), "Mod List");

            int y = 100;

            foreach (var mod in mods)
            {
                RenderBasicModInfo(mod, y);
                y += 50;
            }
        }

        private void RenderBasicModInfo(PluginInfo mod, int y)
        {
            GUI.Label(new Rect(110, y + 10, windowWidth - 20, 20),
                $"{mod.Metadata.Name} <color=gray>({mod.Metadata.GUID}) {mod.Metadata.Version}</color>");
            if (GUI.Button(new Rect(110, y + 30, 100, 20), "Information"))
            {
                selectedMod = mod.Instance;
                selectedTab = InfoTab.Info;
            }

            if (GUI.Button(new Rect(210, y + 30, 100, 20), "Settings"))
            {
                selectedMod = mod.Instance;
                selectedTab = InfoTab.Settings;
            }
        }
    }
}