using UnityEngine;

namespace ModMenu.UI.ModList.Settings.Types
{
    public class IntegerSetting : Setting
    {
        public bool error;
        public string indeterminateSetting;

        public override void Initialize(string name, object value)
        {
            Name = name;
            Value = value;
            indeterminateSetting = Value.ToString();
            currentValue = value;
        }
        
        public object currentValue;

        public override void RenderSetting(ref int y)
        {
            GUI.Label(new Rect(110, y, 200, 30), Name);
            indeterminateSetting = GUI.TextField(new Rect(320, y, 200, 30), indeterminateSetting);
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
        }
    }
}