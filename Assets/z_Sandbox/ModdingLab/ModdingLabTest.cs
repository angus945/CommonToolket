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
        //public static Dictionary<string, EntityData> EntityDataTable = new Dictionary<string, EntityData>();
        //public static Dictionary<string, SpriteSheet> SpriteSheetTable = new Dictionary<string, SpriteSheet>();
        public List<EntityDefine> entities = new List<EntityDefine>();
        public List<SpriteSheetDefine> spriteSheets = new List<SpriteSheetDefine>();

        public Texture2D tex;

        // Start is called before the first frame update
        void Start()
        {
            StreamingItem[] items = StreamingLoader.GetItemsWithType("Define");
            DefinitionTables.LoadDefinitionDatas(items);

            foreach (KeyValuePair<string, EntityDefine> item in DefinitionTables.entityDefineTable)
            {
                entities.Add(item.Value);
            }
            foreach (KeyValuePair<string, SpriteSheetDefine> item in DefinitionTables.spriteSheetDefineTable)
            {
                spriteSheets.Add(item.Value);
            }
            GameEntity entity = GameEntity.CreateEntity(DefinitionTables.entityDefineTable["Entity_A"]);
        }

    }
}
