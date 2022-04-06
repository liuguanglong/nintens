using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uiscriptengine
{
    public class FormController
    {
        BaseEventHanlder? eventHanlder;

        Dictionary<String, String> dicEventHanlder = new Dictionary<string, string>();

        public void RegisterEventHanlder(BaseEventHanlder hanlder)
        {
            this.eventHanlder = hanlder;
        }

        private async Task HanlderUIEvent(EventArgs args)
        {
            if(args is UIEventArgs)
            {
                UIEventArgs uiEventArgs = (UIEventArgs)args;
                if(eventHanlder != null)
                {
                    await eventHanlder.Handle(uiEventArgs.ControlName, uiEventArgs.EventName, uiEventArgs.Args);
                }
            }
        }
    }
}
