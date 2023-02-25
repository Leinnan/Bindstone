using System;
using System.Collections.Generic;
using System.Linq;
using Bindstone;
using Bindstone.Binding;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BindstoneEditor.Inspectors
{
    public class Connection
    {
        public bool alreadyHas;
        public GameObject connectedObject;
        public Type type;

        public string GetConnectionDescription()
        {
            if (!alreadyHas) return string.Empty;

            var c = connectedObject.GetComponent<AbstractMemberBinding>();
            // if(c.) 
            return string.Empty;
        }
    }

    [CustomEditor(typeof(BaseViewModel), true)]
    public class BaseViewModelnspector : Editor
    {
        private static Vector2 offset = new Vector2(0, 2);

        private static readonly Type[] typesToLookFor = new[]
            { typeof(TextMeshProUGUI), typeof(UnityEngine.UI.Button), typeof(TMP_InputField) };

        List<Connection> connectedList = new List<Connection>();

        public void OnEnable()
        {
            Refresh();
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
        }

        private void OnDisable()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= HierarchyWindowItemOnGUI;
        }

        private void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            Color fontColor = Color.blue;
            Color backgroundColor = EditorGUIUtility.isProSkin ? new Color(.1f, .1f, .1f) : new Color(.76f, .76f, .76f);

            var obj = EditorUtility.InstanceIDToObject(instanceID);
            if (obj != null)
            {
                var index = connectedList.FindIndex(c => c.connectedObject.GetInstanceID() == instanceID);
                if (index >= 0)
                {
                    fontColor = connectedList[index].alreadyHas
                        ? Color.yellow
                        : Color.green; ///new Color(0.24f, 0.48f, 0.90f);

                    Rect offsetRect = new Rect(selectionRect.position + offset, selectionRect.size);
                    EditorGUI.DrawRect(selectionRect, backgroundColor);
                    EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle()
                        {
                            normal = new GUIStyleState() { textColor = fontColor },
                            fontStyle = FontStyle.Bold
                        }
                    );
                }
            }
        }

        private void Refresh()
        {
            if (target is Component component)
            {
                foreach (var c in component.GetComponentsInChildren(typeof(AbstractMemberBinding)))
                {
                    if (connectedList.All(cc => cc.connectedObject != c.gameObject))
                    {
                        connectedList.Add(new Connection()
                        {
                            connectedObject = c.gameObject,
                            alreadyHas = true,
                            type = c.GetType()
                        });
                    }
                }

                foreach (var t in typesToLookFor)
                {
                    foreach (var c in component.GetComponentsInChildren(t))
                    {
                        if (connectedList.All(cc => cc.connectedObject != c.gameObject))
                            connectedList.Add(new Connection()
                            {
                                connectedObject = c.gameObject,
                                alreadyHas = c.gameObject.GetComponent(typeof(AbstractMemberBinding)) != null,
                                type = t
                            });
                    }
                }
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            VisualElement rootObject = new VisualElement();
            var sheetAsset =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/com.leinnan.bindstone/Editor/Inspectors/style.uss");
            if (sheetAsset != null)
            {
                rootObject.styleSheets.Add(sheetAsset);
            }

            var box = new VisualElement();
            box.AddToClassList("infoContainer");
            VisualElement defaultContainer = new VisualElement();
            Foldout couldConnectContainer = new Foldout { text = "<b>Can connect list</b>" };
            Foldout connectedContainer = new Foldout { text = "<b>Connected list</b>" };
            foreach (var connection in connectedList)
            {
                if (!connection.alreadyHas)
                {
                    var button = new Button
                        { text = $"<b>{connection.connectedObject.gameObject.name}</b>: {connection.type.Name}" };
                    button.clickable.clicked += () =>
                    {
                        //TODO create matching component in connectedObject
                        Debug.Log("TODO");
                    };
                    couldConnectContainer.contentContainer.Add(button);
                }
                else
                {
                    var connected = new VisualElement();
                    connected.AddToClassList("connected");
                    

                    var button = new Button { text = $"<b>{connection.connectedObject.gameObject.name}</b>" };
                    button.AddToClassList("gameObjectName");
                    button.clickable.clicked +=
                        () => Selection.activeGameObject = connection.connectedObject.gameObject;
                    var label = new Label { text = $"<b>{connection.type.Name}</b>" };
                    
                    
                    connected.Add(button);
                    connected.Add(label);
                    connectedContainer.contentContainer.Add(connected);
                }
            }

            box.Add(couldConnectContainer);
            box.Add(connectedContainer);
            rootObject.Add(box);
            rootObject.Add(defaultContainer);
            InspectorElement.FillDefaultInspector(defaultContainer, serializedObject, this);

            return rootObject;
        }
    }
}