using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

namespace TokenizableAnimation.Graph
{
    public class AnimationGraphView : GraphView
    {
        EditorWindow window;
        NodeSearchWindow searchWindow;

        //
        public AnimationGraphView(EditorWindow window, StyleSheet style)
        {
            SetParent(window);
            SetControl();
            SetStyle(style);

            DrawGrid();

            AddSearchWindow();
        }

        void SetParent(EditorWindow window)
        {
            this.window = window;
            this.StretchToParentSize();
        }
        void SetControl()
        {
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new FreehandSelector());
        }
        void SetStyle(StyleSheet style)
        {
            styleSheets.Add(style);
        }

        void DrawGrid()
        {
            GridBackground grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();
        }

        void AddSearchWindow()
        {
            searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            searchWindow.Configure(window, this);

            nodeCreationRequest = n => SearchWindow.Open(new SearchWindowContext(n.screenMousePosition), searchWindow);
        } 

        //public GraphElement CreateRootNode(Vector2 pos)
        //{
        //    return new RootNode(pos, window, this);
        //}
        //public GraphElement CreateLeafNode(Vector2 pos)
        //{
        //    return new LeafNode(pos, window, this);
        //}
    }

}
