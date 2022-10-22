//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Xml;
//using System.Xml.Serialization;
//using UnityEngine;
//using UnityEngine.UI;
//using MoonSharp.Interpreter;
//using DataDriven;
//using DataDriven.Localization;
//using DataDriven.Lua;
//using DataDriven.XML;

//[System.Serializable][MoonSharpUserData]
//public class Properity
//{
//    public Vector3 position;

//    [field:SerializeField][XmlElement]
//    public float Speed { get; set; }

//    [field:SerializeField][XmlArray("Tags")][XmlArrayItem("Tag")]
//    public string[] Tags { get; set; }

//    [field:SerializeField][XmlArray("Components")][XmlArrayItem("Component")]
//    public List<string> Components { get; set; }
//}

//[System.Serializable]
//public class Behavior
//{
//    [field: SerializeField] [XmlElement]
//    public string Movement { get; set; }

//    DynValue updateMethod;

//    public void LoadBehavior(Script script)
//    {
//        updateMethod = script.Globals.Get(Movement);
//    }
//    public void Update(Properity properity, float delta)
//    {
//        //Debug.Log(delta);
//        updateMethod.Function.Call(properity, delta);
//    }
//}

//[System.Serializable]
//public class Entity
//{
//    [field: SerializeField][XmlElement]
//    public Properity Properity { get; set; }

//    [field: SerializeField][XmlElement]
//    public Behavior Behavior { get; set; }

//    public void Update(float delta)
//    {
//        Behavior.Update(Properity, delta);
//    }
//}

//public class Test : MonoBehaviour
//{
//    [SerializeField] Text text;
//    [SerializeField] Image image;
//    [SerializeField] string path;

//    public Entity[] entities;

//    void Start()
//    {
//        //XmlDocument xml = new XmlDocument();
//        //XmlDeclaration xmldecl = xml.CreateXmlDeclaration("1.0", "UTF-8", "");
//        //XmlElement root = xml.CreateElement("Data");
//        //XmlElement info = xml.CreateElement("Info");

//        //info.SetAttribute("Name", "Angus");
//        //info.SetAttribute("Age", "20");
//        //info.SetAttribute("Phone", "123456");

//        //root.AppendChild(info);
//        //xml.AppendChild(root);
//        //xml.Save($"{Application.streamingAssetsPath}/{path}/TestXML.xml");

//        //UserData.RegisterType<Properity>();
//        //LuaInitializer.Initialize(Debug.Log, typeof(Properity));
//        LuaInitializer.RegisterTypes(typeof(Properity));

//        StreamingLoader.LoadStreamingItems();
//        StreamingItem[] items = StreamingLoader.GetItemsWithType("Complex");


//        StreamingFile entitiesFile = items[0].root.GetFileWithName("Entities");
//        StreamingFile behavioursFile = items[0].root.GetFileWithName("Behaviours");

//        Script behaviorScript = behavioursFile.ReadLua();
//        LuaInitializer.IncludeLiberary(behaviorScript);

//        XmlDocument entitiesXML = entitiesFile.ReadXML();

//        XmlNodeList infos = entitiesXML.GetFirstElementByTagName("Entities").ChildNodes;
//        this.entities = new Entity[infos.Count];
//        for (int i = 0; i < infos.Count; i++)
//        {
//            Entity entity = XMLConverter.ConvertNode<Entity>(infos[i]);

//            Debug.Log(entity);
//            Debug.Log(entity.Behavior);
//            Debug.Log(behaviorScript);
//            entity.Behavior.LoadBehavior(behaviorScript);
//            this.entities[i] = entity;// new Entity(infos[i]);
//        }
//        //Debug.Log(xml.ToString());
//    }
//    void Update()
//    {
//        for (int i = 0; i < entities.Length; i++)
//        {
//            entities[i].Update(Time.deltaTime);
//        }
//    }
//}
