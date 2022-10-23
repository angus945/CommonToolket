using System;
using System.Collections.Generic;
using System.Xml;
using DataDriven;
using DataDriven.XML;

namespace ModdingLab.Definition
{
    public class DefinitionTables
    {
        public static Dictionary<string, EntityDefine> entityDefineTable = new Dictionary<string, EntityDefine>();
        public static Dictionary<string, SpriteSheetDefine> spriteSheetDefineTable = new Dictionary<string, SpriteSheetDefine>();

        public static void LoadDefinitionDatas(StreamingItem[] defineDatas)
        {
            entityDefineTable = new Dictionary<string, EntityDefine>();
            spriteSheetDefineTable = new Dictionary<string, SpriteSheetDefine>();

            for (int i = 0; i < defineDatas.Length; i++)
            {
                LoadDefineData(defineDatas[i]);
            }
        }

        static void LoadDefineData(StreamingItem source)
        {
            LoadDefineData<EntityDefine>(source, "Entity", "Entity", entityDefineTable);

            LoadDefineData<SpriteSheetDefine>(source, "Visual", "SpriteSheet", spriteSheetDefineTable);
        }
        static void LoadDefineData<T>(StreamingItem source, string directoryName, string tagName, Dictionary<string, T> defineTable) where T : class, IDefinition
        {
            StreamingDirectory directory = source.root.GetDirectory(directoryName);
            StreamingFile[] files = directory.GetFilesWithFormat("xml");

            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];

                XmlDocument xml = file.ReadXML();

                XmlNodeList datas = xml.GetElementsByTagName(tagName);
                foreach (XmlNode entityDefine in datas)
                {
                    T defineData = XMLConverter.ConvertNode<T>(entityDefine);
                    defineTable.Add(defineData.id, defineData);

                    defineData.Initial(directory);
                }
            }
        }
    }

}
