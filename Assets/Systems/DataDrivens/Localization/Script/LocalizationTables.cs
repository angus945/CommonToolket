using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataDriven.Localization
{
    public class LocalizationTable
    {
        string language;
        string tableName;
        Dictionary<string, string> table;
    }
    public class LocalizationTables : MonoBehaviour
    {
        //static bool isInitialized;

        public static void LoadText()
        {
            string path = Application.streamingAssetsPath;

            Directory.GetDirectories(path);

        }
    }

}