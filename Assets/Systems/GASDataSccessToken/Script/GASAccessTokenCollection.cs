using UnityEngine;

namespace DataDriven.GoogleAppScript
{
    [CreateAssetMenu(menuName = "DataDriven/GoogleAppScript/GASAccessCollection", fileName = "new Collection")]
    public class GASAccessTokenCollection : ScriptableObject
    {
        [SerializeField] string _excelID;
        [SerializeField] string _writeLocation;
        public string excelID { get => _excelID; }
        public string writeLocation { get => _writeLocation; }

        [SerializeField] string[] _sheetNames;

        public string[] sheetNames { get => _sheetNames; }
        public string[] pathes
        {
            get
            {
                string[] pathes = new string[_sheetNames.Length];
                for (int i = 0; i < pathes.Length; i++)
                {
                    pathes[i] = $"{Application.dataPath}/{_writeLocation}/{_sheetNames[i]}.json";

                }
                return pathes;
            }
        }
    }
}