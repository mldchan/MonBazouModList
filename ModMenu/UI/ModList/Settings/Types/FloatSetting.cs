using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class FloatSetting : Setting
    {
        public string intermediateValue;
        public bool error;


        public override void Initialize(string name, object value)
        {
            Name = name;
            Value = value;
            intermediateValue = Value.ToString();
            currentValue = value;
        }
        
        public object currentValue;

        public override void RenderSetting(ref int y)
        {
            GUI.Label(new Rect(110, y, 200, 30), Name);
            intermediateValue = GUI.TextField(new Rect(320, y, 200, 30), intermediateValue);
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
        }
    }
}