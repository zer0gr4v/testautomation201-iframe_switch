using OpenQA.Selenium;
using System.Collections.Generic;

namespace AutoIFrameSwitcher.Common
{
    public class IFrameElement
    {
        public IWebElement Element { get; set; }
        public string Name { get; set; }
        public string ElementId { get; set; }
        public int Index { get; set; }
        public int? Parent { get; set; }
        //public string Level { get; set;}
        public bool HasChildren { get; set; }
        public List<IWebElement> SearchedElement { get; set; }       
    }
}
