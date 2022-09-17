namespace DataDriven.Localization
{
    [System.Serializable]
    class LocalizationItem
    {
        public string key;
        public string value;

        public LocalizationItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public override string ToString()
        {
            return $"key: {key}, value: {value}";
        }
    }

}
