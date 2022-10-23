using DataDriven;
using DataDriven.XML;
using ModdingLab.Instance.Visual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace ModdingLab.Definition
{
    public class DefinitionTables
    {
        static Dictionary<string, EntityDefine> entityDefineTable = new Dictionary<string, EntityDefine>();
        static Dictionary<string, SpriteSheetDefine> spriteSheetDefineTable = new Dictionary<string, SpriteSheetDefine>();

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

        //
        public static EntityDefine[] AccessEntityDefines()
        {
            return entityDefineTable.Values.ToArray();
        }
        public static SpriteSheetDefine[] AccessSpriteSheetDefines()
        {
            return spriteSheetDefineTable.Values.ToArray();
        }

        //
        public static EntityDefine GetEntityDefine(string entityID)
        {
            if (entityDefineTable.TryGetValue(entityID, out EntityDefine entity))
            {
                return entity;
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Undefine Entity: {entityID}");
                return null;
            }
        }

        public static SpriteSheet GetSpriteSheet(string sheetID)
        {
            if (DefinitionTables.spriteSheetDefineTable.TryGetValue(sheetID, out SpriteSheetDefine sheet))
            {
                return new SpriteSheet(sheet.id, sheet.width, sheet.height, sheet.texture);
            }
            else
            {
                UnityEngine.Debug.LogWarning($"Undefine SpriteSheet: {sheetID}");
                return null;
            }
        }
        public static SpriteSheetAnimation[] GetSpriteSheetAnimations(string sheetID, out string defaultAnimation)
        {
            if (spriteSheetDefineTable.TryGetValue(sheetID, out SpriteSheetDefine define))
            {
                SpriteSheetAnimation[] animations = Array.ConvertAll(define.animationDatas.animations, n =>
                {
                    return new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration);
                });
                defaultAnimation = define.animationDatas.defaultAnimation;
                return animations;
            }
            else
            {
                defaultAnimation = "";
                UnityEngine.Debug.LogWarning($"Undefine SpriteSheet: {sheetID}");
                return null;
            }
        }
    }

}
