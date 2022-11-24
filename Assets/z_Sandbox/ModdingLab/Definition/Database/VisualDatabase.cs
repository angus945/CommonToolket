using DataDriven;
using ModdingLaboratory.Instance.Visual;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace ModdingLaboratory.Definition
{
    public class VisualDatabase
    {
        static VisualDatabase instance;
        static Dictionary<string, SpriteSheet> spriteSheetTable;
        public static bool TryGetSpriteSheet(string sheetID, out SpriteSheet spriteSheet)
        {
            return spriteSheetTable.TryGetValue(sheetID, out spriteSheet);
        }
        public static void GetAllSpriteSheets(out List<SpriteSheet> spriteSheets)
        {
            spriteSheets = new List<SpriteSheet>(spriteSheetTable.Values);
        }
        public static void GetAllSpriteSheetDefines(out List<VisualDataDefine> spriteSheets)
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
            //if (instance != null) return;

            instance = this;
            defineTable = new DefinitionTable<VisualDataDefine>();
            textureTable = new Dictionary<string, Texture>();

            spriteSheetTable = new Dictionary<string, SpriteSheet>();
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

        public void ProcessingDefine()
        {
            foreach (VisualDataDefine define in defineTable.Values)
            {
                if (!textureTable.TryGetValue(define.source, out Texture texture))
                {
                    //TODO
                    continue;
                }

                string defaultImage = "";
                string defaultAnim = "";

                Dictionary<string, SpriteSheetImage> images = new Dictionary<string, SpriteSheetImage>();
                Dictionary<string, SpriteSheetAnimation> animations = new Dictionary<string, SpriteSheetAnimation>();

                if (define.haveSprite)
                {
                    defaultImage = define.spriteDatas.defaultSprite;
                    images = define.spriteDatas.sprites.ToDictionary(n => n.name, n => new SpriteSheetImage(n.name, n.x, n.y));
                }
                if (define.haveAnimation)
                {
                    defaultAnim = define.animationDatas.defaultAnimation;
                    animations = define.animationDatas.animations.ToDictionary(n => n.name, n => new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration));
                }
                //TODO ProcessError

                SpriteSheet sheet = new SpriteSheet(define.width, define.height, texture, defaultImage, defaultAnim, images, animations);
                spriteSheetTable.Add(define.globalID, sheet);
            }
        }

    }

}
