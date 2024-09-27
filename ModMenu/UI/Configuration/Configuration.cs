using System;
using BepInEx;
using BepInEx.Configuration;
using ModMenu.UI.Configuration.Structs;

namespace ModMenu.UI.Configuration
{
    public class Configuration
    {
        public static Configuration Instance { get; private set; }

        private ConfigEntry<bool> _showOnStartup;
        private ConfigEntry<string> _showConsoleWhen;

        private ConfigEntry<bool> _checkForUpdates;
        private ConfigEntry<bool> _skipLoadingScreen;

        public static Structs.Console Console
        {
            get
            {
                if (Instance == null)
                    throw new InvalidProgramException("Config file not initialized, stopping...");
                return new Structs.Console
                {
                     showConsoleOnStartup = Instance._showOnStartup.Value,
                     showConsoleWhen =  Instance._showConsoleWhen.Value
                };
            }
            set
            {
                if (Instance == null)
                    throw new InvalidProgramException("Config file not initalized, stopping...");
                
                Instance._showOnStartup.Value = value.showConsoleOnStartup;
                Instance._showConsoleWhen.Value = value.showConsoleWhen;
            }
        }
        
        public static Startup Startup
        {
            get
            {
                if (Instance == null)
                    throw new InvalidProgramException("Config file not initialized, stopping...");
                return new Startup
                {
                    checkForUpdates = Instance._checkForUpdates.Value,
                    skipLoadingScreen = Instance._skipLoadingScreen.Value
                };
            }
            set
            {
                if (Instance == null)
                    throw new InvalidProgramException("Config file not initalized, stopping...");
                
                Instance._checkForUpdates.Value = value.checkForUpdates;
                Instance._skipLoadingScreen.Value = value.skipLoadingScreen;
            }
        }

        public static void Initialize(ConfigFile config)
        {
            Instance = new Configuration();
            
            Instance._showOnStartup = config.Bind("Console", "Show Console on Startup", false, "Show the console window on startup");
            Instance._showConsoleWhen = config.Bind("Console", "Show Console When", "Error", "When to show the console window. Options: Never, Error, Warning, Log");
            
            Instance._checkForUpdates = config.Bind("Update Checker", "Check for Updates", true, "Check for updates on startup");
            Instance._skipLoadingScreen = config.Bind("Startup", "Skip Loading Screen", false, "Skip the loading screen on startup");
        }
    }
}