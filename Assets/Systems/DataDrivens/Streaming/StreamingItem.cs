using System;

namespace DataDriven
{
    [System.Serializable]
    public class StreamingItem
    {
        public const string FILE_NAME = "_streamingItem";

        public string itemName;
        public string itemType;
        public StreamingItem(string itemName, string itemType)
        {
            this.itemName = itemName;
            this.itemType = itemType;
        }

        [System.NonSerialized] public readonly StreamingDirectory root;

        public StreamingItem(string rootPath)
        {
            root = new StreamingDirectory(rootPath);
        }
        public override string ToString()
        {
            return $"name: {itemName}, type: {itemType}, path: {root.path}";
        }
    }
}
