using Microsoft.AspNetCore.Components;
using MudBlazor;
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

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            Label = "test2020";
        }

        private MudButton button1;
    }
}
