using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataStorage;
using DataDriven;
using System.Linq;
using ModdingLaboratory.Definition;

namespace ModdingLaboratory.Management
{

    [System.Serializable]
    public class ModData
    {
        public StreamingItem modItem;
        public string name { get => modItem.itemName; }

        public ModOption option;
        public bool active { get => option.modActive; set => option.modActive = value; }

        public ModData(StreamingItem item, ModOption option)
        {
            modItem = item;
            this.option = option;
        }
    }

    public class ModManager
    {
        static Dictionary<string, ModData> modDatas = new Dictionary<string, ModData>();
        static ModOptionStorage optionStorage;

        public static void Initial()
        {
            optionStorage = StorageTokenLoader.LoadStorageToken<ModOptionStorage>();

            StreamingLoader.LoadStreamingItems();
            DefinitionManager.Initial();
        }
        public static void LoadModFiles()
        {
            modDatas.Clear();

            Dictionary<string, ModOption> options = optionStorage.GetModOptions();

            StreamingItem[] items = StreamingLoader.GetItemsWithType("Define");

            for (int i = 0; i < items.Length; i++)
            {
                StreamingItem item = items[i];

                ModOption option;

                if (!options.TryGetValue(item.itemName, out option))
                {
                    option = new ModOption(item.itemName, false);
                }

                modDatas.Add(item.itemName, new ModData(item, option));
            }

            System.GC.Collect();
        }
        public static void ParseModFiles()
        {
            Debugger.Tick();

            foreach (var mod in modDatas)
            {
                ModData modData = mod.Value;

                if(modData.active)
                {
                    StreamingItem modItem = modData.modItem;
                    DefinitionManager.LoadDefinition(modItem);

                    Debugger.Tick($"Data Parsed, Mod: {modData.name}");
                }
            }

            DefinitionManager.ProcessingDefinition();

            Debugger.Tick($"Definition Processed");

            Debugger.PrintLog();
        }
        public static void SaveModOptions()
        {
            Dictionary<string, ModOption> options = new Dictionary<string, ModOption>();
            foreach (var mod in modDatas)
            {
                ModOption optoin = mod.Value.option;
                options.Add(optoin.modName, optoin);
            }

            optionStorage.SetModOptions(options);
            optionStorage.ApplyToStorge();
        }
    }

}
