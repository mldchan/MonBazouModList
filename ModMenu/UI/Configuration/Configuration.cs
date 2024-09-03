using System;
using BepInEx;
using BepInEx.Configuration;

namespace ModMenu.UI.Configuration
{
    public class Configuration
    {
        public static Configuration Instance { get; private set; }

        private ConfigEntry<bool> _showOnStartup;
        private ConfigEntry<string> _showConsoleWhen;

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

        public static void Initialize(ConfigFile config)
        {
            Instance = new Configuration();
            
            Instance._showOnStartup = config.Bind("Console", "Show Console on Startup", false, "Show the console window on startup");
            Instance._showConsoleWhen = config.Bind("Console", "Show Console When", "Error", "When to show the console window. Options: Never, Error, Warning, Log");
        }
    }
}