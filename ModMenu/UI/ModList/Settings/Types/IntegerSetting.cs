using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class IntegerSetting : Setting
    {
        public bool error;
        public string indeterminateSetting;

        public override void Initialize(string name, object value, string comment, string category)
        {
            Name = name;
            Value = value;
            Comment = comment;
            Category = category;
            indeterminateSetting = Value.ToString();
            currentValue = value;
        }
        
        public object currentValue;

        public override void RenderSetting()
        {
            GUILayout.BeginHorizontal();
            
            GUILayout.Space(30);
            GUILayout.Label(Name, GUILayout.Width(150));
            indeterminateSetting = GUILayout.TextField(indeterminateSetting, GUILayout.Width(250));
            if (error) GUI.color = Color.red;

            if (int.TryParse(indeterminateSetting, out var result))
            {
                Value = result;
                error = false;

                if (Value != currentValue)
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