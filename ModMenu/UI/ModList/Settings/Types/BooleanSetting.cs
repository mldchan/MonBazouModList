using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class BooleanSetting : Setting
    {
        public override void Initialize(string name, object value)
        {
            Name = name;
            Value = value;
            currentValue = value;
        }
        
        public object currentValue;

        public override void RenderSetting(ref int y)
        {
            var val = (bool) Value;
            
            Value = GUI.Toggle(new Rect(110, y, 400, 30), val, Name);

            if (currentValue != Value)
            {
                currentValue = Value;
                InvokeValueUpdated();
            }
            
            y += 20;
        }
    }
}