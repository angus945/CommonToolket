using DataDriven;
using DataDriven.XML;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace ModdingLab
{
    public class ModdingLabTest : MonoBehaviour
    {
        public static Dictionary<string, EntityData> EntityDataTable = new Dictionary<string, EntityData>();
        public static Dictionary<string, SpriteSheet> SpriteSheetTable = new Dictionary<string, SpriteSheet>();

        public Texture2D tex;

        // Start is called before the first frame update
        void Start()
        {
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
            }

            //---------
            StreamingDirectory visualDirectory = item.root.GetDirectory("Visual");
            StreamingFile spriteSheetFiles = visualDirectory.GetFileWithName("SpriteSheets");
            XmlDocument spriteSheetXML = spriteSheetFiles.ReadXML();
            XmlNodeList spriteSheetsList = spriteSheetXML.GetFirstElementByTagName("SpriteSheets").ChildNodes;

            for (int i = 0; i < spriteSheetsList.Count; i++)
            {
                SpriteSheet spriteSheetData = XMLConverter.ConvertNode<SpriteSheet>(spriteSheetsList[i]);
                var texture = visualDirectory.GetFileWithName(spriteSheetData.source);
                spriteSheetData.texture = texture.ReadImage();

                SpriteSheetTable.Add(spriteSheetData.id, spriteSheetData);
            }
            //

            GameEntity entity = GameEntity.CreateEntity(EntityDataTable.First().Value);
        }

    }
}
