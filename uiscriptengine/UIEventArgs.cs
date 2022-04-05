using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiscriptengine
{
    public class UIEventArgs :EventArgs 
    {
        public String ControlName;
        public String EventName;
        public EventArgs Args;

        public UIEventArgs(String controlName, String eventName,EventArgs args)
        {
            this.ControlName = controlName; 
            this.EventName = eventName;
            this.Args = args;
        }
    }
}
