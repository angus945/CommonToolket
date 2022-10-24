﻿using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;
using UnityEngine;
using MoonSharp.Interpreter;

namespace DataDriven
{
    [System.Serializable]
    public class StreamingFile
    {
        public readonly string name;
        public readonly string format;

        public readonly byte[] data;
        public StreamingFile(string path)
        {
            string[] fileName = Regex.Split(path, "\\/").Last().Split('.');

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
        public Texture2D ReadImage(FilterMode filter)
        {
            Texture2D image = new Texture2D(2, 2);
            image.LoadImage(data);
            image.filterMode = filter;

            return image;
        }
        public Script ReadLua()
        {
            Script script = new Script();
            string code = System.Text.Encoding.UTF8.GetString(this.data);

            script.DoString(code);

            return script;
        }
        public XmlDocument ReadXML()
        {
            string xml = System.Text.Encoding.UTF8.GetString(this.data);
            XmlDocument docs = new XmlDocument();

            docs.LoadXml(xml);
            return docs;
        }
        //public Object ReadObject()
        //{
        //    return ByteComverter.FromByteArray<Object>(data);
        //}

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
}
