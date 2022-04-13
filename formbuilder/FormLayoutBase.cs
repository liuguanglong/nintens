using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder
{
    public class FormLayoutBase : OwningComponentBase
    {
        [Parameter] 
        public JObject LayoutDefinition { get; set; }
    }
}
