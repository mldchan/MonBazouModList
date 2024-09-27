using UnityEngine.Serialization;

namespace ModMenu.UpdateChecker
{
    public class VersionInfo
    {
        public int version;
        public string versionString;

        public bool error;
        public string errorString;
    }
}