using UnityEngine;
using UnityEditor;
using Bindstone.Binding;
using Bindstone.Binding.Internal;
using System.Linq;
using System.Reflection;

namespace BindstoneEditor
{
    /// <summary>
    /// Inspector window for SubViewModelBinding
    /// </summary>
    [CustomEditor(typeof(SubViewModelBinding))]
    public class SubViewModelBindingEditor : BaseBindingEditor
    {
        private SubViewModelBinding targetScript;

        /// <summary>
        /// Whether or not the value on our target matches its prefab.
        /// </summary>
        private bool propertyPrefabModified;

        private void OnEnable()
        {
            targetScript = (SubViewModelBinding)target;
        }

        public override void OnInspectorGUI()
        {
            if (CannotModifyInPlayMode())
            {
                GUI.enabled = false;
            }

            UpdatePrefabModifiedProperties();

            var bindableProperties = FindBindableProperties();

            var defaultLabelStyle = EditorStyles.label.fontStyle;
            EditorStyles.label.fontStyle = propertyPrefabModified 
                ? FontStyle.Bold 
                : defaultLabelStyle;

            ShowViewModelPropertyMenu(
                new GUIContent(
                    "Sub view-model property", 
                    "The property on the top level view model containing the sub view-model"
                ),
                bindableProperties,
                updatedValue => 
                {
                    targetScript.ViewModelPropertyName = updatedValue;

                    targetScript.ViewModelTypeName = bindableProperties
                        .Single(prop => prop.ToString() == updatedValue)
                        .Member.PropertyType.ToString();
                },
                targetScript.ViewModelPropertyName,
                p => true
            );

            EditorStyles.label.fontStyle = defaultLabelStyle;
        }

        private BindableMember<PropertyInfo>[] FindBindableProperties()
        {
            return TypeResolver.FindBindableProperties(targetScript)
                .Where(prop => prop.Member.PropertyType
                    .GetCustomAttributes(typeof(BindingAttribute), false)
                    .Any()
                )
                .ToArray();
        }

        /// <summary>
        /// Check whether each of the properties on the object have been changed 
        /// from the value in the prefab.
        /// </summary>
        private void UpdatePrefabModifiedProperties()
        {
            var property = serializedObject.GetIterator();
            // Need to call Next(true) to get the first child. Once we have it, Next(false)
            // will iterate through the properties.

            propertyPrefabModified = false;
            property.Next(true);
            do
            {
                switch (property.name)
                {
                    case "viewModelPropertyName": 
                    case "viewModelTypeName":
                        propertyPrefabModified = property.prefabOverride 
                            || propertyPrefabModified;
                        break;
                }
            }
            while (property.Next(false));
        }
    }
}