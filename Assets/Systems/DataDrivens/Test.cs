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
        LocalizationTables.LoadText();

        string input = "减少 50 %来自任何地方的<eq_intro_3>。<eq_intro_2><eq_intro_1>";
        string result = LocalizationTables.ReplaceTag("Chinese (Taiwan)", input, "UI_EquipIntro", out string[] tags);

        Debug.Log(input);
        Debug.Log(result);
        Debug.Log(tags.PrintOut());

        text.text = result;
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

        //LocalizationTables.LoadText();
        //string[] texts = LocalizationTables.GetTexts("Chinese (Simplified)", "Tips");
        //Debug.Log(texts.PrintOut());
        //GetComponent<Text>().text = Application.dataPath;

        //string[] osFonts = Font.GetOSInstalledFontNames();
        //string rndFont = osFonts[Random.Range(0, osFonts.Length)];
        //Font font = Font.CreateDynamicFontFromOSFont("abc", 16);

        //text.font = font;
        //text.text = "Hello";
        //Debug.Log(Font.GetOSInstalledFontNames().PrintOut());
    }
}
