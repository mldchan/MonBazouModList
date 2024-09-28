using System;
using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class StringSetting : Setting
    {
        public override void Initialize(string name, object value, string comment, string category)
        {
            Name = name;
            Value = value;
            Comment = comment;
            Category = category;
            currentValue = value;
        }

        private object currentValue;

        public override void RenderSetting()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Space(30);
            GUILayout.Label(Name, GUILayout.Width(150));
            Value = GUILayout.TextField((string)Value, GUILayout.Width(250));
            if (Value != currentValue)
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