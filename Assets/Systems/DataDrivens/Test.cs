using DataDriven;
using DataDriven.Localization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image image;

    void Start()
    {
        //text.text = Application.streamingAssetsPath;
        //string path = Application.streamingAssetsPath;


        //string[] folders = Directory.GetDirectories(Application.streamingAssetsPath + "\\English");
        //string[] folders = Directory.GetFiles(Application.streamingAssetsPath + "\\English");
        //Debug.Log(folders.PrintOut());

        //StreamingItem[] contents = StreamingLoader.GetItemsWithType(KeywordDefine.LOCALIZATION);
        //for (int i = 0; i < contents.Length; i++)
        //{
        //    StreamingFolder folder = contents[i].rootFolder;

        //    Texture2D tex = folder.childFiles.Where(n => n.format == "jpg").ToArray().First().ReadImage();
        //    image.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        //}

        //StreamingItem[] items = StreamingLoader.GetItemsWithType("Localization");
        //Debug.Log(items.PrintOut());

        LocalizationTables.LoadText();
        string[] texts = LocalizationTables.GetTexts("Chinese (Simplified)", "Tips");
        Debug.Log(texts.PrintOut());
        //GetComponent<Text>().text = Application.dataPath;
    }
}
