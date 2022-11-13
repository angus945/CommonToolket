using DataStorage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModdingLaboratory.Management
{
    [System.Serializable]
    public class ModOption
    {
        public string modName;
        public bool modActive;

        public ModOption(string modName, bool modActive)
        {
            this.modName = modName;
            this.modActive = modActive;
        }
    }

    [CreateAssetMenu(fileName = "Mod Option Storage", menuName = StorageTokenBase.MenuBase + "ModOption")]
    public class ModOptionStorage : StorageTokenBase
    {
        public override string storgeKey { get => "ModOptions"; }

        public ModOption[] modOptions;

        public Dictionary<string, ModOption> GetModOptions()
        {
            Dictionary<string, ModOption> optoinTable = new Dictionary<string, ModOption>();

            if (modOptions == null) return optoinTable;

            for (int i = 0; i < modOptions.Length; i++)
            {
                ModOption option = modOptions[i];
                optoinTable.Add(option.modName, option);
            }

            return optoinTable;
        }
        public void SetModOptions(Dictionary<string, ModOption> optoins)
        {
            modOptions = optoins.Values.ToArray();
        }

        protected override void OnClear()
        {
            modOptions = new ModOption[0];
        }
    }
}
