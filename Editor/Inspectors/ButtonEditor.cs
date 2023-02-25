using System;
using Bindstone.Binding;
using UnityEditor;
using UnityEngine.UI;

namespace BindstoneEditor.Inspectors
{
    [CustomEditor(typeof(Button), true)]
    public class ButtonEditor : BaseBuiltinClassExtensionEditor<Button>
    {
        public override Type BinderType => typeof(EventBinding);
        public override string BinderText => "Bind to click";
        public override string DefaultViewValue => $"{TypeName()}.onClick"; // todo fix
    }
}