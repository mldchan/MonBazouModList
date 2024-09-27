using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace ModMenu.UpdateChecker
{
    public class UpdateCheck
    {
        public static async Task<VersionInfo> GetLatestVersion()
        {
            UnityWebRequest req = UnityWebRequest.Get("https://mldkyt.nekoweb.org/config/MonBazouModList/version.json");
            req.SendWebRequest();
            while (!req.isDone)
            {
                await Task.Delay(100);
            }

            if (req.result == UnityWebRequest.Result.ConnectionError ||
                req.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("[ModMenu]Failed to check for updates: " + req.error);
                return new VersionInfo
                {
                    error = true,
                    errorString = req.error
                };
            }
            else if (req.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.Log("[ModMenu]Failed to process update data: " + req.error);
                return new VersionInfo
                {
                    error = true,
                    errorString = "Data processing error: " + req.error
                };
            }

            if (!string.IsNullOrEmpty(req.error))
            {
                Debug.Log("[ModMenu]Failed to check for updates: " + req.error);
                return new VersionInfo
                {
                    error = true,
                    errorString = req.error
                };
            }

            var info = JsonUtility.FromJson<VersionInfo>(req.downloadHandler.text);
            return info;
        }
    }
}