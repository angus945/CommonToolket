using DataDriven;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Text text;
    void Start()
    {
        //string path = Application.streamingAssetsPath;

        //text.text = path.ToString();

        //string[] folders = Directory.GetDirectories(path);
        //Debug.Log(folders.PrintOut());

        StreamingItem[] items = StreamingLoader.GetItemsWithType("Localization");
        Debug.Log(items.PrintOut());

        //GetComponent<Text>().text = Application.dataPath;
    }
}
