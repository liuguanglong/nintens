using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder
{
    public class dynamicform<TElement, TLayout> : OwningComponentBase
        where TElement : FormLayoutBase
        where TLayout : FormLayoutBase
    {
        [CascadingParameter] EditContext CascadedEditContext { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            JObject define = (JObject)CascadedEditContext.Model;

            //创建layout控件,传递layout布局信息给Formlayout Component
            builder.OpenComponent(0, typeof(TLayout));
            builder.AddAttribute(1, nameof(FormLayoutBase.LayoutDefinition), define);
            builder.CloseComponent();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}
