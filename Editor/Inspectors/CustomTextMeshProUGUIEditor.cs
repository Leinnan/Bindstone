using System;
using Bindstone.Binding;
using TMPro;
using UnityEditor;

namespace BindstoneEditor.Inspectors
{
    [CustomEditor(typeof(TextMeshProUGUI))]
    public class CustomTextMeshProUGUIEditor : BaseBuiltinClassExtensionEditor<TextMeshProUGUI>
    {
        public override Type BinderType => typeof(OneWayPropertyBinding);
        public override string BinderText => "Create one way binding";
        public override string DefaultViewValue => "TMPro.TextMeshProUGUI.text";
    }
}