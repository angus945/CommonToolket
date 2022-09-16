namespace DataDriven
{
    [System.Serializable]
    public class StreamingItem
    {
        public const string FILE_NAME = "_streamingItem";

        public string itemName;
        public string itemType;

        [System.NonSerialized] public readonly string folderPath;

        public StreamingItem(string itemName, string itemType)
        {
            this.itemName = itemName;
            this.itemType = itemType;
        }
        public StreamingItem(string folderPath)
        {
            this.folderPath = folderPath;
        }

        public override string ToString()
        {
            return $"name: {itemName}, type: {itemType}, path: {folderPath}";
        }
    }
}
