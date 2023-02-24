using Bindstone;
using UnityEditor;
using UnityEngine;

namespace BindstoneEditor.Inspectors
{
    [CustomEditor(typeof(BaseViewModel), true)]
    public class BaseViewModelnspector : Editor
    {
        public void OnEnable()
        {
            Debug.Log("1");
        }
        public override void OnInspectorGUI()
        {
            // my code here
            base.OnInspectorGUI();
        }
    }
}