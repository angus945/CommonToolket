using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    //[CreateAssetMenu(fileName = "New ProcessNode", menuName = "DataDriven/DataProcess/ProcessNode")]
    public abstract class TextProcessNode : ScriptableObject
    {
        public const string MENU_BASE = "DataDriven/DataProcess/";
        public abstract IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback);
    }
}
