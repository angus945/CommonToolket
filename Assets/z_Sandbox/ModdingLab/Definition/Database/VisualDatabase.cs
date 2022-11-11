using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using DataDriven;
using ModdingLab.Instance.Visual;
using System.Linq;

namespace ModdingLab.Definition
{
    public class VisualDatabase
    {
        static VisualDatabase instance;
        public static bool TryGetSpriteSheet(string sheetID, out SpriteSheet spriteSheet)
        {
            return instance.GetSpriteSheet(sheetID, out spriteSheet);
        }
        public static SpriteSheetAnimation[] TryGetAnimations(string sheetID, out string defaultAnimation)
        {
            return instance.GetSpriteSheetAnimations(sheetID, out defaultAnimation);
        }
        public static void GetAllSpriteSheets(out List<SpriteSheetDefine> spriteSheets)
        {
            spriteSheets = new List<SpriteSheetDefine>(instance.defineTable.Values);
        }
        public static void GetAllTextures(out List<Texture> textures)
        {
            textures = new List<Texture>(instance.textureTable.Values);
        }

        //
        DefinitionTable<SpriteSheetDefine> defineTable;
        Dictionary<string, Texture> textureTable;

        public VisualDatabase()
        {
            if (instance != null) return;

            instance = this;
            defineTable = new DefinitionTable<SpriteSheetDefine>();
            textureTable = new Dictionary<string, Texture>();
        }

        public void LoadDefine(StreamingDirectory visualDirectory)
        {
            StreamingFile[] entities = visualDirectory.files;
            LoadDefines(entities);

            if (visualDirectory.TryGetDirectory("Texture", out StreamingDirectory moduleDirectory))
            {
                StreamingFile[] modulesFiles = moduleDirectory.files;
                LoadAssets(modulesFiles);
            }
        }
        void LoadDefines(StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];

                XmlDocument xml = file.ReadXML();
                XmlElement root = xml.DocumentElement;

                XmlNodeList datas = root.ChildNodes;
                foreach (XmlNode define in datas)
                {
                    defineTable.Add(define);
                }
            }
        }
        void LoadAssets(StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                Texture asset = file.ReadImage(FilterMode.Point);

                textureTable.Add(file.name, asset);
            }
        }

        bool GetSpriteSheet(string sheetID, out SpriteSheet sheet)
        {
            if (defineTable.TryGetDefine(sheetID, out SpriteSheetDefine spriteSheet))
            {
                Texture texture = GetTexture(spriteSheet.source);
                sheet = new SpriteSheet(spriteSheet.id, spriteSheet.width, spriteSheet.height, texture);
                return true;
            }
            else
            {
                //TODO logerror

                sheet = null;
                return false;
            }
        }
        SpriteSheetAnimation[] GetSpriteSheetAnimations(string sheetID, out string defaultAnimation)
        {
            defaultAnimation = "";
            string defaultAnim = "";

            if (defineTable.TryGetDefine(sheetID, out SpriteSheetDefine define))
            {
                defaultAnim = define.animationDatas.defaultAnimation;

                SpriteSheetAnimation[] animations = Array.ConvertAll(define.animationDatas.animations, n =>
                {
                    return new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration);
                });
                defaultAnimation = defaultAnim;

                return animations;
            }

            return null;
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
