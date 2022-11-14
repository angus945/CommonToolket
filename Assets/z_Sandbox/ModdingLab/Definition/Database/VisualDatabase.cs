using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DataDriven;
using ModdingLaboratory.Instance.Visual;
using System.Linq;

namespace ModdingLaboratory.Definition
{
    public class VisualDatabase
    {
        static VisualDatabase instance;
        public static bool TryGetSpriteSheet(string sheetID, out SpriteSheet spriteSheet)
        {
            return instance.GetSpriteSheet(sheetID, out spriteSheet);
        }
        public static void GetAllSpriteSheets(out List<VisualDataDefine> spriteSheets)
        {
            spriteSheets = new List<VisualDataDefine>(instance.defineTable.Values);
        }
        public static void GetAllTextures(out List<Texture> textures)
        {
            textures = new List<Texture>(instance.textureTable.Values);
        }

        //
        DefinitionTable<VisualDataDefine> defineTable;
        Dictionary<string, Texture> textureTable;

        public VisualDatabase()
        {
            if (instance != null) return;

            instance = this;
            defineTable = new DefinitionTable<VisualDataDefine>();
            textureTable = new Dictionary<string, Texture>();
        }

        public void LoadDefine(string prefix, StreamingDirectory visualDirectory)
        {
            StreamingFile[] entities = visualDirectory.files;
            LoadDefines(prefix, entities);

            if (visualDirectory.TryGetDirectory("Texture", out StreamingDirectory moduleDirectory))
            {
                StreamingFile[] modulesFiles = moduleDirectory.files;
                LoadAssets(prefix, modulesFiles);
            }
        }
        void LoadDefines(string group, StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];

                XmlDocument xml = file.ReadXML();
                XmlElement root = xml.DocumentElement;

                XmlNodeList datas = root.ChildNodes;
                foreach (XmlNode define in datas)
                {
                    defineTable.Add(group, define);
                }
            }
        }
        void LoadAssets(string prefix, StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                Texture asset = file.ReadImage(FilterMode.Point);

                textureTable.Add($"{prefix}.{file.name}", asset);
            }
        }

        bool GetSpriteSheet(string sheetID, out SpriteSheet sheet)
        {
            if (defineTable.TryGetDefine(sheetID, out VisualDataDefine define))
            {
                Texture texture = GetTexture(define.source);
                SpriteSheetAnimation[] animations = Array.ConvertAll(define.animationDatas.animations, n =>
                {
                    return new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration);
                });
                sheet = new SpriteSheet(define.width, define.height, texture, define.animationDatas.defaultAnimation, animations);
                return true;
            }
            else
            {
                Debugger.Print($"SpriteSheet not found, id: {sheetID}");

                sheet = default;
                return false;
            }
        }

        Texture GetTexture(string name)
        {
            if (textureTable.ContainsKey(name))
            {
                return textureTable[name];
            }
            else
            {
                //TODO return error texture
                return null;
            }
        }
    }

}
