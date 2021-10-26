using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace UnityLogCleaner
{
    [InitializeOnLoad]
    public class UnityLogCleaner
    {
        const string PerfName = "UnityLogCleaner.LastDay";

        static UnityLogCleaner()
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;
            
            var now = DateTime.Now;

            // Only once a day
            var lastDay = PlayerPrefs.GetInt(PerfName);
            if (lastDay == now.DayOfYear)
                return;

            PlayerPrefs.SetInt(PerfName, now.DayOfYear);

            // Remove log files older than 1 month
            foreach (var logFile in Directory.GetFiles("Logs", "*.*", SearchOption.AllDirectories)
                .Where(f => (now - File.GetLastWriteTime(f)).Days > 30))
            {
                try
                {
                    File.Delete(logFile);
                }
                catch (Exception e)
                {
                    Debug.Log($"Failed to delete log file: {logFile}. Error: {e.Message}");
                }
            }
        }
    }
}
