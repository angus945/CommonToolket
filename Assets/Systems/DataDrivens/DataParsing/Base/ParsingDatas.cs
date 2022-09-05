namespace DataDriven
{
    public struct ParsingDatas
    {
        public string dataName;

        public string[] contents;
        public string[] contentFlags;

        public ParsingDatas(string tag, string[] contents, string[] contentFlags) : this()
        {
            this.dataName = tag;
            this.contents = contents;
            this.contentFlags = contentFlags;
        }
        public ParsingDatas(string tag, string content, string contentFlag) : this()
        {
            this.dataName = tag;
            this.contents = new string[] { content };
            this.contentFlags = new string[] { contentFlag };
        }

        public override string ToString()
        {
            return $"tag: {dataName}, names: {contentFlags.PrintOut()}, datas: {contents.PrintOut()}";
        }
    }
}
