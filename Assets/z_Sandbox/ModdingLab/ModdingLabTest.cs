using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using DataDriven.Lua;
using ModdingLaboratory.Definition;
using ModdingLaboratory.Instance;
using ModdingLaboratory.Management;
using ModdingLaboratory.Definition.Componentized;
using MoonSharp.Interpreter;
using ModdingLaboratory.Instance.Visual;

namespace ModdingLaboratory
{
    public class ModdingLabTest : MonoBehaviour
    {
        [SerializeField] string summonID = "Inhert_Enemy_A";
        [SerializeField] bool breakOnSpawn;

        [Header("Defines")]
        public List<EntityDefine> entities = new List<EntityDefine>();
        public List<EntityTags> tags = new List<EntityTags>();
        public List<VisualModule> visuals = new List<VisualModule>();
        public List<ProperityModule> properties = new List<ProperityModule>();
        public List<ComponentModule> components = new List<ComponentModule>();
        public List<BehaviorModule> behaviors = new List<BehaviorModule>();
        public List<string> scripts = new List<string>();

        [Space]
        public List<VisualDataDefine> spriteSheetDefines = new List<VisualDataDefine>();
        public List<Texture> textures = new List<Texture>();

        [Header("Datas")]
        public List<SpriteSheet> spriteSheets = new List<SpriteSheet>();

        void Start()
        {
            Application.targetFrameRate = 120;
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Initial(); 
            }
            if(Input.GetKeyDown(KeyCode.F6))
            {
                GameEntity entity = EntityInstantiator.CreateEntity(summonID);
                entity.transform.position += (Vector3) Random.insideUnitCircle;
                if (breakOnSpawn) Debug.Break();
            }
            if (Input.GetKey(KeyCode.F7))
            {
                GameEntity entity = EntityInstantiator.CreateEntity(summonID);
                entity.transform.position += (Vector3)Random.insideUnitCircle;
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                EntityDatabase.CheckEntity(summonID);
            }
        }

        void Initial()
        {
            entities.Clear();
            spriteSheetDefines.Clear();
            textures.Clear();
            scripts.Clear();

            spriteSheets.Clear();

            LuaInitializer.Initialize(Debug.Log, typeof(GameEntity));

            ModManager.Initial();
            ModManager.LoadModFiles();
            ModManager.SaveModOptions();
            ModManager.ParseModFiles();
            //

            EntityDatabase.GetAllDatas(out entities, out tags, out visuals, out properties, out components, out behaviors);
            //EntityDatabase.GetAllScripts(out scripts);
            VisualDatabase.GetAllSpriteSheetDefines(out spriteSheetDefines);
            VisualDatabase.GetAllTextures(out textures);

            VisualDatabase.GetAllSpriteSheets(out spriteSheets);
            //Rigidbody2D r;
            //r.velocity
            //r.AddForce()
        }
    }
}
