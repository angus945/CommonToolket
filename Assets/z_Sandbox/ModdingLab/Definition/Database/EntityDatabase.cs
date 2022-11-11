using System.Collections.Generic;
using System.Xml;
using DataDriven;

namespace ModdingLab.Definition
{
    public class EntityDatabase
    {
        static EntityDatabase instance;
        public static bool TryGetEntity(string id, out EntityDefine entity, out EntityModule[] modules)
        {
            return instance.GetEntity(id, out entity, out modules);
        }
        public static string GetBehaviorScript(string name)
        {
            return instance.GetScript(name);
        }
        public static void GetAllDatas(out List<EntityDefine> entity, out List<EntityTags> tag, out List<VisualModule> visual, out List<ProperityModule> properties, out List<ComponentModule> components, out List<BehaviorModule> behaviors)
        {
            instance.ListDatas(out entity, out tag, out visual, out properties, out components, out behaviors);
        }
        public static void GetAllScripts(out List<string> scripts)
        {
            scripts = new List<string>(instance.scriptTable.Values);
        }

        //Entity
        DefinitionTable<EntityDefine> entityTable;

        //Module
        DefinitionTable<EntityTags> tagTable;
        DefinitionTable<VisualModule> visualTable;
        //public DefinitionTable<EntityAudio> visualTable;
        DefinitionTable<ProperityModule> properityTable;
        DefinitionTable<ComponentModule> componentTable;
        DefinitionTable<BehaviorModule> behaviorTable;

        //Script
        Dictionary<string, string> scriptTable;

        public EntityDatabase()
        {
            if (instance != null)
            {
                Logger.Log("Warning, try instance mulpitle time");
                return;
            }

            instance = this;

            tagTable = new DefinitionTable<EntityTags>();
            visualTable = new DefinitionTable<VisualModule>();
            properityTable = new DefinitionTable<ProperityModule>();
            componentTable = new DefinitionTable<ComponentModule>();
            behaviorTable = new DefinitionTable<BehaviorModule>();

            entityTable = new DefinitionTable<EntityDefine>();
            scriptTable = new Dictionary<string, string>();
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
            if(entityDirectory.TryGetDirectory("Script", out StreamingDirectory scriptDirectory))
            {
                StreamingFile[] scriptFiles = scriptDirectory.files;
                LoadScrips(scriptFiles);

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
                    AddDefineData(define);
                }
            }
        }
        void LoadScrips(StreamingFile[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                StreamingFile file = files[i];
                string code = file.ReadString();

                scriptTable.Add(file.name, code);
            }
        }
        void AddDefineData(XmlNode define)
        {
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

        bool GetEntity(string id, out EntityDefine entity)
        {
            return entityTable.TryGetDefine(id, out entity);
        }
        bool GetEntity(string id, out EntityDefine entity, out EntityModule[] modules)
        {
            if (entityTable.TryGetDefine(id, out entity))
            {
                List<EntityModule> entityModules = new List<EntityModule>();
                entityModules.AddRange(tagTable.GetDefines(entity.tags.includes));
                entityModules.AddRange(visualTable.GetDefines(entity.spriteSheets.includes));
                entityModules.AddRange(properityTable.GetDefines(entity.properties.includes));
                entityModules.AddRange(componentTable.GetDefines(entity.components.includes));
                entityModules.AddRange(behaviorTable.GetDefines(entity.behaviors.includes));

                modules = entityModules.ToArray();
                return true;
            }
            else
            {
                modules = null;
                return false;
            }
        }
        string GetScript(string name)
        {
            if (scriptTable.ContainsKey(name))
            {
                return scriptTable[name];
            }
            else return "";
            //TODO errorlog
            //return TryGetObject(name, scriptTable, (code) => code);
        }

        void ListDatas(out List<EntityDefine> entity, out List<EntityTags> tag, out List<VisualModule> visual, out List<ProperityModule> properties, out List<ComponentModule> components, out List<BehaviorModule> behaviors)
        {
            entity = new List<EntityDefine>(entityTable.Values);
            tag = new List<EntityTags>(tagTable.Values);
            visual = new List<VisualModule>(visualTable.Values);
            properties = new List<ProperityModule>(properityTable.Values);
            components = new List<ComponentModule>(componentTable.Values);
            behaviors = new List<BehaviorModule>(behaviorTable.Values);
        }
    }

}
