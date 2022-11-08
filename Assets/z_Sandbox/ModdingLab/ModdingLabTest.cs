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
        public List<EntityVisuals> visuals = new List<EntityVisuals>();
        public List<EntityProperties> properties = new List<EntityProperties>();
        public List<EntityComponents> components = new List<EntityComponents>();
        public List<EntityBehaviors> behaviors = new List<EntityBehaviors>();

        [Space]
        public List<SpriteSheetDefine> spriteSheets = new List<SpriteSheetDefine>();
        public List<Texture> textures = new List<Texture>();
        public List<string> scripts = new List<string>();

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


            DefinitionTables.AccessEntities(out entities, out tags, out visuals, out properties, out components, out behaviors);
            spriteSheets.AddRange(DefinitionTables.AccessSpriteSheetDefines());
            textures.AddRange(DefinitionTables.AccessTextures());
            scripts.AddRange(DefinitionTables.AccessScripts());

            //Rigidbody2D r;
            //r.velocity
            //r.AddForce()
        }


    }
}
