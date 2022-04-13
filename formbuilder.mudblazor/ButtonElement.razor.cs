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
        public Dictionary<String,String> Properties { get; set; }

        public String Label { get; set; } = "Button";

        public Size Size { get; set; } = Size.Medium;

        public Color Color { get; set; } = Color.Default;
        public Variant Variant { get; set; } = Variant.Outlined;

        public bool Disabled { get; set; } = false;
        public String Class { get; set; } = "";

    protected override void OnParametersSet()
        {
            base.OnParametersSet();

            foreach(var p in Properties)
            {
                
            }
        }

        private MudButton button1;
    }
}
