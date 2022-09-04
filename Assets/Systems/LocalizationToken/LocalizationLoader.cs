using System.Collections.Generic;
using UnityEngine;

namespace Localization
{
    class LocalizationContent
    {
        string languageCode;

        Dictionary<string, string> contents;

        public LocalizationContent(string sourceContents)
        {


        }
    }
    public class LocalizationLoader : MonoBehaviour
    {
        const string localizationPath = "";
        static Dictionary<string, LocalizationContent> contentTable;
        //public static Localization

        public static void LoadTextAssets()
        {
            contentTable = new Dictionary<string, LocalizationContent>();

            TextAsset[] files = Resources.LoadAll<TextAsset>(localizationPath);
            for (int i = 0; i < files.Length; i++)
            {
                TextAsset file = files[i];
                contentTable.Add(file.name, new LocalizationContent(file.text));
            }
        }
        //public static T LoadLocalization<T>()
        //{
        //    if(localizationTextFiles == null)
        //    return default;
        //}

    }
}
