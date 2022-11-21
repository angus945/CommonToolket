using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DataDriven
{
    public class StreamingLoader : MonoBehaviour
    {
        static bool loaded;
        static Dictionary<string, List<StreamingItem>> streamingItemsTable;

        public static void LoadStreamingItems()
        {
            string[] streamingFolders = Directory.GetDirectories(Application.streamingAssetsPath);
            streamingItemsTable = new Dictionary<string, List<StreamingItem>>();

            for (int i = 0; i < streamingFolders.Length; i++)
            {
                string path = streamingFolders[i];
                string itemPath = ($"{path}/{StreamingItem.FILE_NAME}.json").Replace('\\', '/');

                if (!File.Exists(itemPath))
                {
                    Debugger.RecordLog($"streaming item lost, path: {itemPath}");
                    continue;
                }

                string json = File.ReadAllText(itemPath);

                StreamingItem item = new StreamingItem(path);
                JsonUtility.FromJsonOverwrite(json, item);

                if(!streamingItemsTable.ContainsKey(item.itemType))
                {
                    streamingItemsTable.Add(item.itemType, new List<StreamingItem>());
                }

                streamingItemsTable[item.itemType].Add(item);
            }

            loaded = true;
        }
        public static StreamingItem[] GetItemsWithType(string type)
        {
            if (!loaded) LoadStreamingItems();

            return streamingItemsTable[type].ToArray();
        }
    }
}
