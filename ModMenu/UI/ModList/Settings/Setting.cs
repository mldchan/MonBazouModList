using System;

namespace ModMenu.UI.ModList.Settings
{
    public abstract class Setting
    {
        public string Name;
        public object Value;
        public string Comment;
        public string Category;
        public event EventHandler<object> ValueUpdated; 
        
        public abstract void Initialize(string name, object value, string comment, string category);
        public abstract void RenderSetting();

        protected virtual void InvokeValueUpdated()
        {
            ValueUpdated?.Invoke(this, Value);
        }
    }
}