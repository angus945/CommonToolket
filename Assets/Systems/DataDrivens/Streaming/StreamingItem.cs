using System.IO;
using System.Linq;
using UnityEngine;

namespace DataDriven
{
    [System.Serializable]
    public class StreamingFolder
    {
        public readonly string folderName;
        public readonly string folderPath;

        public readonly StreamingFolder[] childFolders;
        public readonly StreamingFile[] childFiles;

        public StreamingFolder(string path)
        {
            this.folderName = path.Split('\\').Last();
            this.folderPath = path;

            childFolders = LoadFolders(path);
            childFiles = LoadFiles(path);

        }
        StreamingFolder[] LoadFolders(string path)
        {
            string[] pathes = Directory.GetDirectories(path);

            StreamingFolder[] folders = new StreamingFolder[pathes.Length];
            for (int i = 0; i < pathes.Length; i++)
            {
                //Debug.Log(pathes[i]);

                folders[i] = new StreamingFolder(pathes[i]);
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

        public override string ToString()
        {
            return $"folder: {folderName}";
        }
    }

    [System.Serializable]
    public class StreamingFile
    {
        public readonly string name;
        public readonly string format;

        public readonly byte[] data;
        public string ReadString()
        {
            return System.Text.Encoding.UTF8.GetString(this.data);
        }
        public Texture2D ReadImage()
        {
            Texture2D image = new Texture2D(2, 2);
            image.LoadImage(data);
            return image;
        }

        public StreamingFile(string path)
        {
            string[] fileName = path.Split('\\').Last().Split('.');

            name = fileName[0];
            format = fileName[1];

            data = File.ReadAllBytes(path);

            //switch (format.ToLower())
            //{
            //    case "json":
            //    case "xml":
            //        content = File.ReadAllText(path);
            //        break;

            //    case "jpg":
            //    case "png":
            //        content = File.ReadAllBytes(path);
            //        break;

            //    default:
            //        break;
            //}

            //Debug.Log(this);
        }
        public override string ToString()
        {
            string content = "";

            switch (format.ToLower())
            {
                case "json":
                case "xml":
                    content = System.Text.Encoding.UTF8.GetString(this.data);
                    break;

                case "jpg":
                case "png":
                    content = "(image data)";
                    break;

                case "ttf":

                    break;

                default:
                    break;
            }

            return $"name: {name}.{format}, content: {content}";
        }
    }

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

        [System.NonSerialized] public readonly StreamingFolder rootFolder;

        public StreamingItem(string rootPath)
        {
            rootFolder = new StreamingFolder(rootPath);
        }
        public override string ToString()
        {
            return $"name: {itemName}, type: {itemType}, path: {rootFolder.folderPath}";
        }
    }
}
