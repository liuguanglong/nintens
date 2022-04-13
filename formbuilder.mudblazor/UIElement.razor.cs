using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder.mudblazor
{
    public partial class UIElement
    {
        [Parameter]
        public int Span { get; set; }

        [Parameter]
        public Type componentType { get; set; }

        [Parameter]
        public IDictionary<string, object> parameters { get; set; }
    }
}
