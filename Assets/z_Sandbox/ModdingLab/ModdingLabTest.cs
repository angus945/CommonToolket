using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using DataDriven;
using DataDriven.XML;
using ModdingLab.Define;

namespace ModdingLab
{
    public class ModdingLabTest : MonoBehaviour
    {
        public static Dictionary<string, EntityData> EntityDataTable = new Dictionary<string, EntityData>();
        public static Dictionary<string, SpriteSheet> SpriteSheetTable = new Dictionary<string, SpriteSheet>();
        public List<EntityData> entities = new List<EntityData>();
        public List<SpriteSheet> spriteSheets = new List<SpriteSheet>();

        public Texture2D tex;

        // Start is called before the first frame update
        void Start()
        {
            entities.Clear();
            spriteSheets.Clear();

            StreamingLoader.LoadStreamingItems();
            StreamingItem item = StreamingLoader.GetItemsWithType("Complex")[0];

            StreamingFile entitiesFile = item.root.GetDirectory("Entity").GetFileWithName("Entities");
            //StreamingFile behavioursFile = items[0].root.GetFileWithName("Behaviours");

            XmlDocument entitiesXML = entitiesFile.ReadXML();

            XmlNodeList infos = entitiesXML.GetFirstElementByTagName("Entities").ChildNodes;
            for (int i = 0; i < infos.Count; i++)
            {
                EntityData entityData = XMLConverter.ConvertNode<EntityData>(infos[i]);
                EntityDataTable.Add(entityData.id, entityData);
                entities.Add(entityData);
            }

            //---------
            StreamingDirectory visualDirectory = item.root.GetDirectory("Visual");
            StreamingFile spriteSheetFiles = visualDirectory.GetFileWithName("SpriteSheets");
            XmlDocument spriteSheetXML = spriteSheetFiles.ReadXML();
            XmlNodeList spriteSheetsList = spriteSheetXML.GetFirstElementByTagName("SpriteSheets").ChildNodes;

            for (int i = 0; i < spriteSheetsList.Count; i++)
            {
                SpriteSheet spriteSheetData = XMLConverter.ConvertNode<SpriteSheet>(spriteSheetsList[i]);
                StreamingFile texture = visualDirectory.GetFileWithName(spriteSheetData.source);
                spriteSheetData.texture = texture.ReadImage(spriteSheetData.filter);

                SpriteSheetTable.Add(spriteSheetData.id, spriteSheetData);
                spriteSheets.Add(spriteSheetData);
            }
            //

            GameEntity entity = GameEntity.CreateEntity(EntityDataTable.First().Value);
        }

    }
}
