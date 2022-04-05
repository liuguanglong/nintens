using System.Reflection;

namespace uiscriptengine
{
    [System.AttributeUsage(System.AttributeTargets.Method)
]
    public class EventAttribute : System.Attribute
    {
        public string controlName;
        public string eventName;

        public EventAttribute(String controlName, String eventName)
        {
            this.controlName = controlName;
            this.eventName = eventName;
        }
    }

    public class BaseEventHanlder
    {
        public async Task Handle(String controlName,String eventName, EventArgs args)
        {
            MethodInfo? method = getMethod(controlName,eventName);
            if (method != null)
            {
                Task? task = (Task?)method.Invoke(this, new object[] { args });
                if(task != null)
                    await task;
            }
        }

        public MethodInfo getMethod(String controlName,String eventName)
        {
            MethodInfo[] methods = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach(var m in methods)
            {
                var attr = m.GetCustomAttribute<EventAttribute>();
                if(attr != null && attr.controlName == controlName && attr.eventName == eventName)
                {
                    return m;
                }
            }
            return null;
        }
    }
}