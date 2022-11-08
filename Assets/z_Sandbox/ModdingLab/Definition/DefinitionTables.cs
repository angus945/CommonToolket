using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using ModdingLab.Instance.Visual;
using ModdingLab.Definition.Componentized;

namespace ModdingLab.Definition
{
    public class EntityDefinition
    {
        public DefinitionTable<EntityDefine> entityTable;

        public DefinitionTable<EntityTags> tagTable;
        public DefinitionTable<EntityVisuals> visualTable;
        //public DefinitionTable<EntityAudio> visualTable;
        public DefinitionTable<EntityProperties> properityTable;
        public DefinitionTable<EntityComponents> componentTable;
        public DefinitionTable<EntityBehaviors> behaviorTable;

        public EntityDefinition()
        {
            tagTable = new DefinitionTable<EntityTags>();
            visualTable = new DefinitionTable<EntityVisuals>();
            properityTable = new DefinitionTable<EntityProperties>();
            componentTable = new DefinitionTable<EntityComponents>();
            behaviorTable = new DefinitionTable<EntityBehaviors>();

            entityTable = new DefinitionTable<EntityDefine>();
        }
        public void LoadDefine(StreamingDirectory entityDirectory)
        {
            StreamingFile[] entities = entityDirectory.files;
            LoadDefines(entities);
            
            if(entityDirectory.TryGetDirectory("Module", out StreamingDirectory moduleDirectory))
            {
                StreamingFile[] modulesFiles = moduleDirectory.files;
                LoadDefines(modulesFiles);
            }
        }

        //
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
                    AddDefine(define);
                }
            }
        }
        void AddDefine(XmlNode define)
        {
            Debug.Log(define.Name);

            switch (define.Name)
            {
                case "Entity":
                    entityTable.Add(define);
                    break;

                case "Tags":
                    tagTable.Add(define);
                    break;

                case "Visual":
                    visualTable.Add(define);
                    break;

                case "Audio":
                    break;

                case "Properties":
                    properityTable.Add(define);
                    break;

                case "Components":
                    componentTable.Add(define);
                    break;

                case "Behavior":
                    behaviorTable.Add(define);
                    break;

                default:
                    break;
            }
        }

        public void ListDatas(out List<EntityDefine> entity, out List<EntityTags> tag, out List<EntityVisuals> visual, out List<EntityProperties> properties, out List<EntityComponents> components, out List<EntityBehaviors> behaviors)
        {
            entity = new List<EntityDefine>(entityTable.Values);
            tag = new List<EntityTags>(tagTable.Values);
            visual = new List<EntityVisuals>(visualTable.Values);
            properties = new List<EntityProperties>(properityTable.Values);
            components = new List<EntityComponents>(componentTable.Values);
            behaviors = new List<EntityBehaviors>(behaviorTable.Values);
        }
    }

    public class DefinitionTables
    {
        //
        static DefinitionTable<EntityDefine> entityDefineTable;
        static DefinitionTable<SpriteSheetDefine> spriteSheetDefineTable;
        static Dictionary<string, Texture> textureTable;
        static Dictionary<string, string> scriptTable;

        static EntityDefinition entityDefinition;

        public static void Initial()
        {
            entityDefinition = new EntityDefinition();

            entityDefineTable = new DefinitionTable< EntityDefine>();
            spriteSheetDefineTable = new DefinitionTable< SpriteSheetDefine>();
            textureTable = new Dictionary<string, Texture>();
            scriptTable = new Dictionary<string, string>();
        }
        public static void LoadDefinitionDatas(StreamingItem[] defineDatas)
        {
            for (int i = 0; i < defineDatas.Length; i++)
            {
                LoadDefineData(defineDatas[i]);
                LoadDefineAsset(defineDatas[i]);
            }
        }
        public static void LoadDefinitionData(StreamingItem defineData)
        {
            LoadDefineData(defineData);
            LoadDefineAsset(defineData);
        }
        static void LoadDefineData(StreamingItem source)
        {
            if(source.root.TryGetDirectory("Entity", out StreamingDirectory entityDirectory))
            {
                entityDefinition.LoadDefine(entityDirectory);
            }

            //LoadDefineData(source, "Entity", "Entity", entityDefineTable);
            LoadDefineData(source, "Visual", "SpriteSheet", spriteSheetDefineTable);
        }
        static void LoadDefineAsset(StreamingItem source)
        {
            LoadDefineAsset(source, "Texture", textureTable, (file) => file.ReadImage(FilterMode.Point));
            LoadDefineAsset(source, "Script", scriptTable, (file) => file.ReadString());
        }
        static void LoadDefineData<T>(StreamingItem source, string directoryName, string tagName, DefinitionTable<T> defineTable) where T : class, IDefinition
        {
            if (!source.root.TryGetDirectory(directoryName, out StreamingDirectory directory)) return;

            StreamingFile[] files = directory.GetFilesWithFormat("xml");

            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];

                XmlDocument xml = file.ReadXML();

                XmlNodeList datas = xml.GetElementsByTagName(tagName);
                foreach (XmlNode entityDefine in datas)
                {
                    T defineData = XMLConverter.ConvertNode<T>(entityDefine);
                    defineTable.Add(defineData);
                }
            }
        }
        static void LoadDefineAsset<T>(StreamingItem source, string directoryName, Dictionary<string, T> assetTable, Func<StreamingFile, T> loadHandler)
        {
            if (!source.root.TryGetDirectory(directoryName, out StreamingDirectory directory)) return;

            StreamingFile[] files = directory.files;
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                T asset = loadHandler.Invoke(file);

                assetTable.Add(file.name, asset);
                //Debug.Log(file.name);
            }
        }

        //TODO Fix
        public static void AccessEntities(out List<EntityDefine> entity, out List<EntityTags> tag, out List<EntityVisuals> visual, out List<EntityProperties> properties, out List<EntityComponents> components, out List<EntityBehaviors> behaviors)
        {
            entityDefinition.ListDatas(out entity, out tag, out visual, out properties, out components, out behaviors);
        }
        public static SpriteSheetDefine[] AccessSpriteSheetDefines()
        {
            return spriteSheetDefineTable.definitionTable.Values.ToArray();
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
        public static bool GetEntityDefine(string entityID, out EntityDefine entity)
        {
            entity = entityDefineTable.GetDefine(entityID);

            return entity != null;
        }

        public static SpriteSheet GetSpriteSheet(string sheetID)
        {
            //TODO Fix
            SpriteSheetDefine spriteSheet = spriteSheetDefineTable.GetDefine(sheetID);

            Texture texture = GetTexture(spriteSheet.source);
            return new SpriteSheet(spriteSheet.id, spriteSheet.width, spriteSheet.height, texture);
        }
        public static SpriteSheetAnimation[] GetSpriteSheetAnimations(string sheetID, out string defaultAnimation)
        {
            defaultAnimation = "";
            string defaultAnim = "";

            SpriteSheetDefine define = spriteSheetDefineTable.definitionTable[sheetID];

            defaultAnim = define.animationDatas.defaultAnimation;

            SpriteSheetAnimation[] animations = Array.ConvertAll(define.animationDatas.animations, n =>
            {
                return new SpriteSheetAnimation(n.name, n.index, n.length, n.loop, n.duration);
            });
            defaultAnimation = defaultAnim;

            return animations;

            //return animations;
        }

        public static Texture GetTexture(string name)
        {
            return TryGetObject(name, textureTable, (texture) => texture);
        }
        public static string GetScriptCode(string name)
        {
            return TryGetObject(name, scriptTable, (code) => code);
        }

        //
        static To TryGetObject<From, To>(string id, Dictionary<string, From> table, Func<From, To> convertHandler)
        {
            if (table.TryGetValue(id, out From obj))
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
