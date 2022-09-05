using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven
{
    [CreateAssetMenu]
    public class DataParsingList : ScriptableObject
    {
        [SerializeField] DataParsingNode[] parsingNodes;

        public IEnumerator ParsingRoutine(Action<ParsingDatas[]> onFinishedCallback)
        {
            ParsingDatas[] parsingDatas = new ParsingDatas[0];

            for (int i = 0; i < parsingNodes.Length; i++)
            {
                yield return parsingNodes[i].ParsingRoutine(parsingDatas, (datas) =>
                {
                    parsingDatas = datas;
                });
            }

            onFinishedCallback?.Invoke(parsingDatas);
        }
    }
}
