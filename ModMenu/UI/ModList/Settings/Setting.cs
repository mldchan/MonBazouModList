using System;

namespace ModMenu.UI.ModList.Settings
{
    public abstract class Setting
    {
        public string Name;
        public object Value;
        public event EventHandler<object> ValueUpdated; 
        
        public abstract void Initialize(string name, object value);
        public abstract void RenderSetting(ref int y);

        protected virtual void InvokeValueUpdated()
        {
            ValueUpdated?.Invoke(this, Value);
        }
    }
}