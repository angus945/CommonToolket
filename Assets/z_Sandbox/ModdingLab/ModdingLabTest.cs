using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using DataDriven.Lua;
using ModdingLab.Definition;
using ModdingLab.Instance;
using ModdingLab.Management;
using ModdingLab.Definition.Componentized;

namespace ModdingLab
{
    public class ModdingLabTest : MonoBehaviour
    {
        [SerializeField] string summonID = "Inhert_Enemy_A";

        [Space]
        public List<EntityDefine> entities = new List<EntityDefine>();
        public List<EntityTags> tags = new List<EntityTags>();
        public List<VisualModule> visuals = new List<VisualModule>();
        public List<ProperityModule> properties = new List<ProperityModule>();
        public List<ComponentModule> components = new List<ComponentModule>();
        public List<BehaviorModule> behaviors = new List<BehaviorModule>();
        public List<string> scripts = new List<string>();

        [Space]
        public List<SpriteSheetDefine> spriteSheets = new List<SpriteSheetDefine>();
        public List<Texture> textures = new List<Texture>();

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
                EntityInstantiator.CreateEntity(summonID);
            }
            if (Input.GetKey(KeyCode.F7))
            {
                EntityInstantiator.CreateEntity(summonID);
            }
        }

        void Initial()
        {
            entities.Clear();
            spriteSheets.Clear();
            textures.Clear();
            scripts.Clear();

            ModManager.Initial();
            ModManager.LoadModFiles();
            ModManager.SaveModOptions();
            ModManager.ParseModFiles();
            //

            //
            LuaInitializer.Initialize(Debug.Log, typeof(GameEntity));


            EntityDatabase.GetAllDatas(out entities, out tags, out visuals, out properties, out components, out behaviors);
            EntityDatabase.GetAllScripts(out scripts);
            VisualDatabase.GetAllSpriteSheets(out spriteSheets);
            VisualDatabase.GetAllTextures(out textures);

            //Rigidbody2D r;
            //r.velocity
            //r.AddForce()
        }


    }
}
