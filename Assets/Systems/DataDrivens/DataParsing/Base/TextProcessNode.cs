using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    //[CreateAssetMenu(fileName = "New ProcessNode", menuName = "DataDriven/DataProcess/ProcessNode")]
    public abstract class TextProcessNode : ScriptableObject
    {
        public abstract IEnumerator ParsingRoutine(ProcessingData[] input, Action<ProcessingData[]> onFinishedCallback);
    }
}
