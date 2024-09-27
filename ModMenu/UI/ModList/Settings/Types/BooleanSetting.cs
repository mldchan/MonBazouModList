using ModMenu.UI.ModList.Settings;
using UnityEngine;

namespace ModList.UI.ModList.Settings.Types
{
    public class BooleanSetting : Setting
    {
        public override void Initialize(string name, object value, string comment, string category)
        {
            Name = name;
            Value = value;
            Comment = comment;
            Category = category;
            currentValue = value;
        }

        public object currentValue;

        public override void RenderSetting()
        {
            var val = (bool)Value;

            GUILayout.BeginHorizontal();
            
            GUILayout.Space(30);
            Value = GUILayout.Toggle(val, Name, GUILayout.Width(400));

            if (currentValue != Value)
            {
                currentValue = Value;
                InvokeValueUpdated();
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                GUILayout.Space(5);
                GUI.color = Color.gray;
                GUILayout.Label(Comment);
                GUI.color = Color.white;
            }
            
            GUILayout.EndHorizontal();
        }
    }
}