using System;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using MoonSharp.Interpreter;

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
        public StreamingFile[] GetFilesWithType(string type)
        {
            return files.Where(n => n.name.EndsWith($".{type}")).ToArray();
        }
        public StreamingFile GetFileWithType(string type)
        {
            return files.Where(n => n.name.EndsWith($".{type}")).First();
        }

        public override string ToString()
        {
            return $"folder: {name}";
        }
    }

    [System.Serializable]
    public class StreamingFile
    {
        public readonly string name;
        public readonly string format;

        public readonly byte[] data;
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
        public Script ReadLua()
        {
            Script script = new Script();
            string code = System.Text.Encoding.UTF8.GetString(this.data);

            script.DoString(code);

            return script;
        }
        internal XmlDocument ReadXML()
        {
            string xml = System.Text.Encoding.UTF8.GetString(this.data);
            XmlDocument docs = new XmlDocument();

            docs.LoadXml(xml);
            return docs;
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
