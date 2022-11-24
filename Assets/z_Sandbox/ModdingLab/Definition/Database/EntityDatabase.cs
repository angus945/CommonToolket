using DataDriven;
using DataDriven.Lua;
using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ModdingLaboratory.Definition
{
    public class EntityDatabase
    {
        static EntityDatabase instance;
        static DefinitionTable<EntityDefine> defineTable;
        public static bool CheckEntity(string id)
        {
            Debugger.PrintLog();

            bool success = TryGetEntity(id, out _);

            Debugger.PrintLog();

            return success;
        }
        public static bool TryGetEntity(string id, out EntityDefine entity)
        {
            return defineTable.TryGetDefine(id, out entity);
        }
        public static bool TryGetBehaviorScript(string name, out Script script)
        {
            return instance.GetScript(name, out script);
        }
        public static void GetAllDatas(out List<EntityDefine> entity, out List<EntityTags> tag, out List<VisualModule> visual, out List<ProperityModule> properties, out List<ComponentModule> components, out List<BehaviorModule> behaviors)
        {
            instance.ListDatas(out entity, out tag, out visual, out properties, out components, out behaviors);
        }

        //Module
        DefinitionTable<EntityTags> tagTable;
        DefinitionTable<VisualModule> visualTable;
        //public DefinitionTable<EntityAudio> audioTable;
        DefinitionTable<ProperityModule> properityTable;
        DefinitionTable<ComponentModule> componentTable;
        DefinitionTable<BehaviorModule> behaviorTable;

        //Script
        Dictionary<string, Script> scriptTable;

        public EntityDatabase()
        {
            //if (instance != null)
            //{
            //    Debugger.RecordLog("Warning, try instance mulpitle time");
            //    return;
            //}

            instance = this;

            defineTable = new DefinitionTable<EntityDefine>();

            tagTable = new DefinitionTable<EntityTags>();
            visualTable = new DefinitionTable<VisualModule>();
            properityTable = new DefinitionTable<ProperityModule>();
            componentTable = new DefinitionTable<ComponentModule>();
            behaviorTable = new DefinitionTable<BehaviorModule>();

            scriptTable = new Dictionary<string, Script>();
        }

        public void LoadDefine(string prefix, StreamingDirectory entityDirectory)
        {
            StreamingFile[] entities = entityDirectory.files;
            LoadDefines(prefix, entities);

            if (entityDirectory.TryGetDirectory("Module", out StreamingDirectory moduleDirectory))
            {
                StreamingFile[] modulesFiles = moduleDirectory.files;
                LoadDefines(prefix, modulesFiles);
            }
            if (entityDirectory.TryGetDirectory("Script", out StreamingDirectory scriptDirectory))
            {
                StreamingFile[] scriptFiles = scriptDirectory.files;
                LoadScrips(prefix, scriptFiles);

            }
        }
        void LoadDefines(string prefix, StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];

                XmlDocument xml = file.ReadXML();
                XmlElement root = xml.DocumentElement;

                XmlNodeList datas = root.ChildNodes;
                foreach (XmlNode define in datas)
                {
                    AddDefineData(prefix, define);
                }
            }
        }
        void LoadScrips(string prefix, StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                string code = file.ReadString();

                Script script = LuaInitializer.CreateScript(code);
                scriptTable.Add($"{prefix}.{file.name}", script);
            }
        }
        void AddDefineData(string group, XmlNode define)
        {
            switch (define.Name)
            {
                case "Entity":
                    defineTable.Add(group, define);
                    break;

                case "Tags":
                    tagTable.Add(group, define);
                    break;

                case "Visual":
                    visualTable.Add(group, define);
                    break;

                case "Audio":
                    break;

                case "Properties":
                    properityTable.Add(group, define);
                    break;

                case "Components":
                    componentTable.Add(group, define);
                    break;

                case "Behavior":
                    behaviorTable.Add(group, define);
                    break;

                default:
                    break;
            }
        }

        bool GetScript(string name, out Script script)
        {
            return scriptTable.TryGetValue(name, out script);
        }

        public void PorcessingDefine()
        {
            foreach (EntityDefine define in defineTable.Values)
            {
                define.tags.Include(tagTable.GetDefines(define.tags.includes));
                define.visuals.Include(visualTable.GetDefines(define.visuals.includes));
                define.properties.Include(properityTable.GetDefines(define.properties.includes));
                define.components.Include(componentTable.GetDefines(define.components.includes));
                define.behaviors.Include(behaviorTable.GetDefines(define.behaviors.includes));
            }
        }

        void ListDatas(out List<EntityDefine> entity, out List<EntityTags> tag, out List<VisualModule> visual, out List<ProperityModule> properties, out List<ComponentModule> components, out List<BehaviorModule> behaviors)
        {
            entity = new List<EntityDefine>(defineTable.Values);
            tag = new List<EntityTags>(tagTable.Values);
            visual = new List<VisualModule>(visualTable.Values);
            properties = new List<ProperityModule>(properityTable.Values);
            components = new List<ComponentModule>(componentTable.Values);
            behaviors = new List<BehaviorModule>(behaviorTable.Values);
        }
    }

}
