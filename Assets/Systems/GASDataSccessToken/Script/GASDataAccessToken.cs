using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace DataDriven.GoogleAppScript
{
    [CreateAssetMenu(menuName = "DataDriven/GoogleAppScript/GASAccessToken", fileName = "new AccesToken")]
    public class GASDataAccessToken : ScriptableObject
    {

        [SerializeField] string _excelID = "";
        [SerializeField] string _sheetName = "";
        public string excelID { get => _excelID; }
        public string sheetName { get => _sheetName; }

        [Space]
        [SerializeField] string _writeFileName = "";
        [SerializeField] string _writeLocation = "";
        public string path { get => $"{Application.dataPath}/{_writeLocation}/{_writeFileName}.json"; }

        [HideInInspector] [SerializeField] string _sourceData = "";
        public string sourceData { get => _sourceData; }
    }
}
