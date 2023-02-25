using System;
using System.Linq;
using Bindstone;
using Bindstone.Binding;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BindstoneEditor.Inspectors
{
    public abstract class BaseBuiltinClassExtensionEditor<T> : Editor where T : Component
    {

        public abstract string DefaultViewValue { get; }
        public abstract Type BinderType { get; }
        public abstract string BinderText { get; }
        public bool HasModelInParents { get; private set; }

        public BaseViewModel ViewModel { get; private set; }
        public string TypeName() => target.GetType().Name;
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement rootObject = new VisualElement();
            VisualElement defaultContainer = new VisualElement();
            if (target is Component component)
            {
                ViewModel = component.GetComponentInParent<BaseViewModel>();
                HasModelInParents = ViewModel != null;
            }
            if (HasModelInParents)
            {
                var popupLabel = new Label { text = "TEST" };
                VisualElement description =
                    new Label($"You can add connection to the model in {ViewModel.gameObject.name}");
                description.AddToClassList("description");
                var button = new Button
                {
                    text = BinderText
                };
                button.clickable.clicked += () =>
                {
                    var component = target as Component;
                    var components = component.gameObject.GetComponents(typeof(Component));
                    var index = components.ToList().FindIndex(component1 => component.Equals(component1));
                    var componentsAmount = components.Length - index;

                    var binding = (AbstractMemberBinding)component.gameObject.AddComponent(BinderType);
                    if (!string.IsNullOrWhiteSpace(DefaultViewValue))
                    {
                        if (binding is OneWayPropertyBinding oneWay)
                        {
                            oneWay.ViewPropertyName = DefaultViewValue;
                        }
                        else if (binding is EventBinding eventBinding)
                        {
                            eventBinding.ViewEventName = DefaultViewValue;
                        }
                    }

                    for (int i = 0; i < componentsAmount; i++)
                    {
                        UnityEditorInternal.ComponentUtility.MoveComponentUp(binding);
                    }
                };
                rootObject.Add(popupLabel);
                rootObject.Add(description);
                rootObject.Add(button);
            }
			
            rootObject.Add(defaultContainer);
            InspectorElement.FillDefaultInspector(defaultContainer, serializedObject, this);

            return rootObject;
        }
    }
}