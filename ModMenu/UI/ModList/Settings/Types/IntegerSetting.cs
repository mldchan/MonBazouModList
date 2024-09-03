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

        public override void RenderSetting(ref int y)
        {
            GUI.Label(new Rect(140, y, 150, 20), Name);
            indeterminateSetting = GUI.TextField(new Rect(320, y, 200, 20), indeterminateSetting);
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

            y += 20;
            if (!string.IsNullOrEmpty(Comment))
            {
                var windowWidth = Screen.width - 800;
                GUI.color = Color.gray;
                GUI.Label(new Rect(200, y, windowWidth - 100, 20), Comment);
                GUI.color = Color.white;
                
                y += 20;
            }
        }
    }
}