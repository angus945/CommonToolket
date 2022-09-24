using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "new FontsNode", menuName = TextProcessNode.MENU_BASE + "Fonts")]
    public class FontsNode : TextProcessNode
    {
        enum Mode
        {
            Single,
            WithFlag,
        }

        [System.Serializable]
        struct FontTable
        {
            public string language;
            public string fontName;
            public string fontSize;
        }

        [SerializeField] FontTable defaultFont;

        [Space]
        [SerializeField] FontTable[] fontTables;

        public override IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback)
        {
            throw new NotImplementedException();
        }
    }

}