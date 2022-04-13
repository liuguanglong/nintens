﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using formbuilder;
using Microsoft.AspNetCore.Components;

namespace formbuilder.mudblazor
{
    public partial class GridLayout
    {
        [Parameter]
        public JObject LayoutDefinition { get; set; }

        public JArray Rows
        {
            get
            {
                return this.LayoutDefinition.Value<JArray>("Rows");
            }
        }

        public Dictionary<string, object> Parameters = new() { { "Label", "Button1" } };
    }
}
