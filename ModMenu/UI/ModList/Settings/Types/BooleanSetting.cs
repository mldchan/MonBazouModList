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

        public override void RenderSetting(ref int y)
        {
            var val = (bool) Value;
            
            Value = GUI.Toggle(new Rect(140, y, 350, 20), val, Name);

            if (currentValue != Value)
            {
                currentValue = Value;
                InvokeValueUpdated();
            }

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