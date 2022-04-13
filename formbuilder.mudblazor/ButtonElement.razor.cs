using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder.mudblazor
{
    public partial class ButtonElement
    {
        [Parameter]
        public String Label { get; set; }
    }
}
