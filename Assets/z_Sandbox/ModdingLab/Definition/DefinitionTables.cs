using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using ModdingLab.Instance.Visual;

namespace ModdingLab.Definition
{
    public class DefinitionTables
    {
        static Dictionary<string, EntityDefine> entityDefineTable;
        static Dictionary<string, SpriteSheetDefine> spriteSheetDefineTable;
        static Dictionary<string, Texture> textureTable;
        static Dictionary<string, string> scriptTable;

        public static void LoadDefinitionDatas(StreamingItem[] defineDatas)
        {
            entityDefineTable = new Dictionary<string, EntityDefine>();
            spriteSheetDefineTable = new Dictionary<string, SpriteSheetDefine>();
            textureTable = new Dictionary<string, Texture>();
            scriptTable = new Dictionary<string, string>();

            for (int i = 0; i < defineDatas.Length; i++)
            {
                LoadDefineData(defineDatas[i]);
                LoadDefineAsset(defineDatas[i]);
            }
        }
        static void LoadDefineData(StreamingItem source)
        {
            LoadDefineData(source, "Entity", "Entity", entityDefineTable);
            LoadDefineData(source, "Visual", "SpriteSheet", spriteSheetDefineTable);
        }
        static void LoadDefineAsset(StreamingItem source)
        {
            LoadDefineAsset(source, "Texture", textureTable, (file) => file.ReadImage(FilterMode.Point));
            LoadDefineAsset(source, "Script", scriptTable, (file) => file.ReadString());
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
                }
            }
        }
        static void LoadDefineAsset<T>(StreamingItem source, string directoryName, Dictionary<string, T> assetTable, Func<StreamingFile, T> loadHandler)
        {
            StreamingDirectory directory = source.root.GetDirectory(directoryName);

            StreamingFile[] files = directory.files;
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                T asset = loadHandler.Invoke(file);

                assetTable.Add(file.name, asset);
                Debug.Log(file.name);
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
        public static Texture[] AccessTextures()
        {
            return textureTable.Values.ToArray();
        }
        public static string[] AccessScripts()
        {
            return scriptTable.Values.ToArray();
        }

        //TODO Moveout
        public static EntityDefine GetEntityDefine(string entityID)
        {
            return TryGetObject(entityID, entityDefineTable, (entity) => entity);
        }

        public static SpriteSheet GetSpriteSheet(string sheetID)
        {
            return TryGetObject(sheetID, spriteSheetDefineTable, (sheet) =>
            {
                Texture texture = GetTexture(sheet.source);
                return new SpriteSheet(sheet.id, sheet.width, sheet.height, texture);
            });
        }
        public static SpriteSheetAnimation[] GetSpriteSheetAnimations(string sheetID, out string defaultAnimation)
        {
            defaultAnimation = "";
            string defaultAnim = "";

            SpriteSheetAnimation[] animations = TryGetObject(sheetID, spriteSheetDefineTable, (define) =>
            {
                defaultAnim = define.animationDatas.defaultAnimation;

                SpriteSheetAnimation[] animations = Array.ConvertAll(define.animationDatas.animations, n =>
                {
                    return new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration);
                });
                return animations;
            });

            defaultAnimation = defaultAnim;
            return animations;
        }

        public static Texture GetTexture(string name)
        {
            return TryGetObject(name, textureTable, (texture) => texture);
        }

        //
        static To TryGetObject<From,To>(string id, Dictionary<string, From> table, Func<From, To> convertHandler)
        {
            if(table.TryGetValue(id, out From obj))
            {
                return convertHandler.Invoke(obj);
            }
            else
            {
                Logger.Log($"Undefine Data, type: {typeof(From)}, id: {id}", LogType.Warning);
                return default;
            }
        }
    }

}
