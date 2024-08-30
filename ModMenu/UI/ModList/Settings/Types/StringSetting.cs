using System;
using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class StringSetting : Setting
    {
        public override void Initialize(string name, object value)
        {
            Name = name;
            Value = value;
            currentValue = value;
        }
        
        private object currentValue;

        public override void RenderSetting(ref int y)
        {
            GUI.Label(new Rect(110, y, 200, 30), Name);
            Value = GUI.TextField(new Rect(320, y, 200, 30), (string) Value);
            if (Value != currentValue)
            {
                currentValue = Value;
                InvokeValueUpdated();
            }
            y += 20;
        }
    }
}