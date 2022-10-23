using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using ModdingLab.Definition;
using ModdingLab.Instance;

namespace ModdingLab
{
    public class ModdingLabTest : MonoBehaviour
    {
        public List<EntityDefine> entities = new List<EntityDefine>();
        public List<SpriteSheetDefine> spriteSheets = new List<SpriteSheetDefine>();

        void Start()
        {
            StreamingItem[] items = StreamingLoader.GetItemsWithType("Define");
            DefinitionTables.LoadDefinitionDatas(items);

            entities.AddRange(DefinitionTables.AccessEntityDefines());
            spriteSheets.AddRange(DefinitionTables.AccessSpriteSheetDefines());

            GameEntity entity = EntityInstantiator.CreateEntity("Entity_A");
        }

    }
}
