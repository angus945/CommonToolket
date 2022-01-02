using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace TokenizableAnimation.Graph
{
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        EditorWindow window;
        AnimationGraphView graphView;

        public void Configure(EditorWindow window, AnimationGraphView graphView)
        {
            this.window = window;
            this.graphView = graphView;
        }

        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> tree = new List<SearchTreeEntry>()
            {
                TreeGroup("Tree Root", 0),
                TreeEntry("Base Node", 1, new BaseNode()),

                TreeGroup("Siblings", 1),
                TreeEntry("Base Node", 2, new BaseNode()),
                //new SearchTreeGroupEntry(new GUIContent("Tree Nodes"), 0),

                //AddNodeSearch("Root Node", new RootNode(Vector2.zero, window, graphView)),
                //AddNodeSearch("Leaf Node", new LeafNode(Vector2.zero, window, graphView))
            };

            return tree;
        }
        SearchTreeGroupEntry TreeGroup(string name, int level)
        {
            return new SearchTreeGroupEntry(new GUIContent(name), level);
        }
        SearchTreeEntry TreeEntry(string name, int level, BaseNode _baseNode)
        {
            SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(name))
            {
                level = level,
                userData = _baseNode
            };
            return entry;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            VisualElement dest = window.rootVisualElement.parent;
            Vector3 point = context.screenMousePosition - window.position.position;
            Vector2 mousePosition = window.rootVisualElement.ChangeCoordinatesTo(dest, point);

            Vector2 grphviewMousePosition = graphView.contentViewContainer.WorldToLocal(mousePosition);

            return CheckForNodeType(searchTreeEntry, grphviewMousePosition);
        }
        bool CheckForNodeType(SearchTreeEntry _searchTreeEntry, Vector2 createPosition)
        {
            switch (_searchTreeEntry.userData)
            {
                case BaseNode _:
                    BaseNode node = new BaseNode();
                    node.SetPosition(new Rect(createPosition, Vector2.one));
                    graphView.AddElement(node);
                    return true;
            }

            return false;
        }
    }

}

