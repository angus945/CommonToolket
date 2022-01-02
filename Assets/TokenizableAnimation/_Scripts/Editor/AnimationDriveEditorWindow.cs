using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.UIElements;

namespace TokenizableAnimation.Graph
{
    public class AnimationDriveEditorWindow : EditorWindow
    {

        //static AnimationDriveEditorWindow window;
        AnimationGraphView graphView;
        StyleSheet styleSheet;

        [MenuItem("test/Graph")]
        public static void Open()
        {
            GetWindow(typeof(AnimationDriveEditorWindow));
        }

        public void OnEnable()
        {
            SetWindowStyles();
            ConstructGraphView();

            CreateToolBar();
        }

        void SetWindowStyles()
        {
            styleSheet = Resources.Load<StyleSheet>("GraphViewStyleSheet");
            rootVisualElement.styleSheets.Add(styleSheet);
        }
        void ConstructGraphView()
        {
            graphView = new AnimationGraphView(this, styleSheet);
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }

        void CreateToolBar()
        {
            Toolbar toolbar = new Toolbar();

            Button testButton = new Button()
            {
                text = "test button",
            };
            testButton.clicked += () => Debug.Log("toolbar button is clicked");
            toolbar.Add(testButton);
            //注意註冊委派是對 clicked 註冊而非 OnClick

            Label testLabel = new Label("toolbar label");
            testLabel.AddToClassList("test_label");

            toolbar.Add(testLabel);

            rootVisualElement.Add(toolbar);
        }
    }

}
