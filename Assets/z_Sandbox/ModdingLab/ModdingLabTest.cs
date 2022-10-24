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

namespace ModdingLab
{
    public class ModdingLabTest : MonoBehaviour
    {
        public List<EntityDefine> entities = new List<EntityDefine>();
        public List<SpriteSheetDefine> spriteSheets = new List<SpriteSheetDefine>();
        public List<Texture> textures = new List<Texture>();
        public List<string> scripts = new List<string>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Initial();
            }
            if(Input.GetKeyDown(KeyCode.F6))
            {
                EntityInstantiator.CreateEntity("Entity_A");
            }
        }

        void Initial()
        {
            entities.Clear();
            spriteSheets.Clear();
            textures.Clear();
            scripts.Clear();

            //
            LuaInitializer.Initialize(Debug.Log, typeof(GameEntity));

            //
            float start = Time.realtimeSinceStartup;
            StreamingItem[] items = StreamingLoader.GetItemsWithType("Define");
            float duration = Time.realtimeSinceStartup - start;
            Debug.Log(duration);


            start = Time.realtimeSinceStartup;
            DefinitionTables.LoadDefinitionDatas(items);
            duration = Time.realtimeSinceStartup - start;
            Debug.Log(duration);

            entities.AddRange(DefinitionTables.AccessEntityDefines());
            spriteSheets.AddRange(DefinitionTables.AccessSpriteSheetDefines());
            textures.AddRange(DefinitionTables.AccessTextures());
            scripts.AddRange(DefinitionTables.AccessScripts());

            //
            //GameEntity entity = EntityInstantiator.CreateEntity("Entity_A");
        }


    }
}
