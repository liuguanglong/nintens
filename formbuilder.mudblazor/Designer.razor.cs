using MudBlazor;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder.mudblazor
{
    public class TreeItemData
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public bool IsExpanded { get; set; }

        public string ControlType { get; set; }

        public HashSet<TreeItemData> TreeItems { get; set; }
        
        public Element control { get; set; }

        public TreeItemData(string title, string icon,string controlType)
        {
            Title = title;
            Icon = icon;
            ControlType = controlType;
        }
    }

    public partial class Designer
    {
        JObject formdefine = new JObject();

        private TreeItemData ActivatedValue { get; set; }

        private HashSet<TreeItemData> SelectedValues { get; set; }

        private HashSet<TreeItemData> TreeItems { get; set; } = new HashSet<TreeItemData>();

        protected override async Task OnInitializedAsync()
        {
            //TreeItems.Add(new TreeItemData("Title", Icons.Filled.Email, "Label"));
            //TreeItems.Add(new TreeItemData("Description", Icons.Filled.Delete, "Label"));
            //TreeItems.Add(new TreeItemData("MainLayout", Icons.Filled.Label, "Grid")
            //{
            //    IsExpanded = true,
            //    TreeItems = new HashSet<TreeItemData>()
            //{
            //    new TreeItemData("Social", Icons.Filled.Group, "InputText"),
            //    new TreeItemData("Updates", Icons.Filled.Info, "InputText"),
            //    new TreeItemData("Forums", Icons.Filled.QuestionAnswer, "InputText"),
            //    new TreeItemData("Promotions", Icons.Filled.LocalOffer, "InputText")
            //}
            //});
            //TreeItems.Add(new TreeItemData("Submit", Icons.Filled.Label,"Button"));

            JObject f1 = new JObject();
            f1.Add("Name", "Field1");
            JObject f2 = new JObject();
            f2.Add("Name", "Field2");
            JObject f3 = new JObject();
            f3.Add("Name", "Field3");

            JArray row1 = new JArray();
            JArray row2 = new JArray();

            row1.Add(f1);
            row1.Add(f2);
            row2.Add(f3);

            JArray rows = new JArray();
            rows.Add(row1);
            rows.Add(row2);

            formdefine.Add("Rows", rows);
        }

        private async Task addtoRoot()
        {

        }
    }
}
