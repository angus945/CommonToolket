using System.IO;
using System.Linq;

namespace DataDriven
{
    [System.Serializable]
    public class StreamingDirectory
    {
        public readonly string name;
        public readonly string path;

        public readonly StreamingDirectory[] directory;
        public readonly StreamingFile[] files;

        public StreamingDirectory(string path)
        {
            this.name = path.Split('\\').Last();
            this.path = path;

            directory = LoadFolders(path);
            files = LoadFiles(path);

        }
        StreamingDirectory[] LoadFolders(string path)
        {
            string[] pathes = Directory.GetDirectories(path);

            StreamingDirectory[] folders = new StreamingDirectory[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                //Debug.Log(pathes[i]);

                folders[i] = new StreamingDirectory(pathes[i]);
            }

            return folders;
        }
        StreamingFile[] LoadFiles(string path)
        {
            string[] pathes = Directory.GetFiles(path).Where(file => !file.EndsWith(".meta") && !file.Contains(StreamingItem.FILE_NAME)).ToArray();

            StreamingFile[] files = new StreamingFile[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                files[i] = new StreamingFile(pathes[i]);
            }

            return files;
        }

        public StreamingDirectory GetDirectory(string name)
        {
            return directory.Where(n => n.name == name).First();
        }

        public StreamingFile[] GetFilesWithName(string name)
        {
            return files.Where(n => n.name == name).ToArray();
        }
        public StreamingFile GetFileWithName(string name)
        {
            return files.Where(n => n.name == name).First();
        }

        public StreamingFile[] GetFilesWithFormat(string format)
        {
            return files.Where(n => n.format == format).ToArray();
        }
        public StreamingFile GetFileWithFormat(string format)
        {
            return files.Where(n => n.format == format).First();
        }

        public override string ToString()
        {
            return $"folder: {name}";
        }
    }
}
