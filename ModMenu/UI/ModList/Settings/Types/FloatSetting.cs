using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class FloatSetting : Setting
    {
        public string intermediateValue;
        public bool error;


        public override void Initialize(string name, object value, string comment, string category)
        {
            Name = name;
            Value = value;
            Comment = comment;
            Category = category;
            intermediateValue = Value.ToString();
            currentValue = value;
        }

        public object currentValue;

        public override void RenderSetting()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Space(30);
            GUILayout.Label(Name, GUILayout.Width(150));
            intermediateValue = GUILayout.TextField(intermediateValue, GUILayout.Width(250));
            if (error) GUI.color = Color.red;

            if (float.TryParse(intermediateValue, out var result))
            {
                Value = result;
                error = false;

                if (currentValue != Value)
                {
                    currentValue = Value;
                    InvokeValueUpdated();
                }
            }
            else error = true;

            GUI.color = Color.white;

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