using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    public abstract class DataParsingNode : ScriptableObject
    {
        public abstract IEnumerator ParsingRoutine(ParsingDatas[] input, Action<ParsingDatas[]> onFinishedCallback);
    }
}
