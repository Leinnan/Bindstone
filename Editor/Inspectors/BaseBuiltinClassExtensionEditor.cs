using System.Linq;
using Bindstone;
using Bindstone.Binding;
using UnityEditor;
using UnityEngine;

namespace BindstoneEditor.Inspectors
{
    public abstract class BaseBuiltinClassExtensionEditor<T> : UnityEditor.Editor where T : Component
    {
        public abstract bool OneWayBinding { get; }
        public bool HasModelInParents { get; private set; }

        public BaseViewModel ViewModel { get; private set; }

        public void OnEnable()
        {
            if (target is Component component)
            {
                ViewModel = component.GetComponentInParent<BaseViewModel>();
                HasModelInParents = ViewModel != null;
            }
        }


        public override void OnInspectorGUI()
        {
            if (HasModelInParents)
            {
                EditorGUILayout.HelpBox($"You can add connection to the model in {ViewModel.gameObject.name}",
                    MessageType.Info);
                if (OneWayBinding && EditorGUILayout.LinkButton("Create one way binding") && target is T component)
                {
                    var components = component.gameObject.GetComponents(typeof(Component));
                    var index = components.ToList().FindIndex(component1 => component.Equals(component1));
                    var componentsAmount = components.Length - index;

                    var binding = component.gameObject.AddComponent<OneWayPropertyBinding>();
                    if (!string.IsNullOrWhiteSpace(DefaultViewPropertyName))
                    {
                        binding.ViewPropertyName = DefaultViewPropertyName;
                    }

                    for (int i = 0; i < componentsAmount; i++)
                    {
                        UnityEditorInternal.ComponentUtility.MoveComponentUp(binding);
                    }
                }

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();
            }

            base.OnInspectorGUI();
        }

        public abstract string DefaultViewPropertyName { get; }
    }
}