namespace DataDriven.TextProcess
{
    public enum ProcessingType
    {
        Single,
        Multiple,
    }

    public struct ProcessingData
    {
        public ProcessingType type;

        public string dataName;
        public string dataFlag;

        public string[] contents;
        public string[] contentFlags;

        public ProcessingData(string name, string flag, string[] contents, string[] contentFlags) : this()
        {
            this.dataName = name;
            this.dataFlag = flag;
            this.contents = contents;
            this.contentFlags = contentFlags;

            type = ProcessingType.Multiple;
        }
        public ProcessingData(string name, string flag, string content, string contentFlag) : this()
        {
            this.dataName = name;
            this.dataFlag = flag;
            this.contents = new string[] { content };
            this.contentFlags = new string[] { contentFlag };

            type = ProcessingType.Single;
        }

        public override string ToString()
        {
            return $"tag: {dataName}, flags: {contentFlags.PrintOut()}, datas: {contents.PrintOut()}";
        }
    }
}
