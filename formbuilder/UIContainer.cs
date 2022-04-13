using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace formbuilder
{
    public class Grid: Container
    {
        public Dictionary<int, MudItem> items;
    }

    public class MudItem: Container
    {
        public Dictionary<int, Element> items;
    }

    public class Div: Container
    {
        public Dictionary<int, ControlElement> items;
    }

    public class Paper: Container
    {

    }

    public class Card:Container
    {
        public Dictionary<int, ControlElement> Header;
        public Dictionary<int, ControlElement> Content;
        public Dictionary<int, ControlElement> Actions;
        public String MediaFile { get; set; }
        public String MediaSize { get; set; }
    }

    public class Element
    {

    }

    public class ControlElement: Element
    {
        public String Id { get; set; }
        public Type ElementType { get; set; }
    }


    public abstract class Container : Element
    {
        public String Name { get; set; }
        public String Class { get; set; }
        public String Style { get; set; }
        public String Space { get; set; }
    }

    public class UIContainer
    {
        public String MaxWidth { get; set; }
        public Dictionary<int, Element> fields;        
    }
}
