using TMPro;
using UnityEditor;

namespace BindstoneEditor.BuiltInExtensions
{
    [CustomEditor(typeof(TextMeshProUGUI))]
    public class CustomTextMeshProUGUIEditor : BaseBuiltinClassExtensionEditor<TextMeshProUGUI>
    {
        public override bool OneWayBinding => true;
        public override string DefaultViewPropertyName => "TMPro.TextMeshProUGUI.text";
    }
}