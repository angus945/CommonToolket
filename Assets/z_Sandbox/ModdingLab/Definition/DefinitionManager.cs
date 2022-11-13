using System;
using System.Collections.Generic;
using System.Linq;
using DataDriven;
using DataDriven.XML;
using ModdingLaboratory.Definition.Componentized;

namespace ModdingLaboratory.Definition
{

    public class DefinitionManager
    {
        static EntityDatabase entityDefinition;
        static VisualDatabase visualDefinition;

        public static void Initial()
        {
            entityDefinition = new EntityDatabase();
            visualDefinition = new VisualDatabase();
        }
        public static void LoadDefinitionDatas(StreamingItem[] defineDatas)
        {
            for (int i = 0; i < defineDatas.Length; i++)
            {
                LoadDefineData(defineDatas[i]);
            }
        }
        public static void LoadDefinitionData(StreamingItem defineData)
        {
            LoadDefineData(defineData);
        }
        static void LoadDefineData(StreamingItem source)
        {
            if(source.root.TryGetDirectory("Entity", out StreamingDirectory entityDirectory))
            {
                entityDefinition.LoadDefine(entityDirectory);
            }
            if (source.root.TryGetDirectory("Visual", out StreamingDirectory visualDirectory))
            {
                visualDefinition.LoadDefine(visualDirectory);
            }
        }
    }

}
