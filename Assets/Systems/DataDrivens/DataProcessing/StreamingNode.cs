using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataDriven.TextProcess
{
    [CreateAssetMenu(fileName = "new StreamingNode", menuName = TextProcessNode.MENU_BASE + "Streaming")]
    public class StreamingNode : TextProcessNode
    {
        [SerializeField] bool useFlag;
        [SerializeField] StreamingItem streamingItem;

        [Space]
        [SerializeField] bool prettyPrint;

        public override IEnumerator ProcessingRoutine(List<ProcessingData> input, Action<List<ProcessingData>> onFinishedCallback)
        {
            List<StreamingItem> streamingItems = GetStreamingItems(input);

            for (int i = 0; i < streamingItems.Count; i++)
            {
                StreamingItem streamingItem = streamingItems[i];
                ProcessingData data = new ProcessingData(StreamingItem.FILE_NAME, streamingItem.itemName, JsonUtility.ToJson(streamingItem, prettyPrint), "");

                input.Add(data);
            }

            onFinishedCallback.Invoke(input);

            yield return null;
        }

        List<StreamingItem> GetStreamingItems(List<ProcessingData> input)
        {
            List<StreamingItem> streamingItems = new List<StreamingItem>();

            if (useFlag)
            {
                for (int i = 0; i < input.Count; i++)
                {
                    ProcessingData data = input[i];

                    if (streamingItems.Find(n => n.itemName == data.dataFlag) == null)
                    {
                        streamingItems.Add(new StreamingItem(data.dataFlag, streamingItem.itemType));
                    }
                }
            }
            else streamingItems.Add(streamingItem);

            return streamingItems;
        }


    }
}
