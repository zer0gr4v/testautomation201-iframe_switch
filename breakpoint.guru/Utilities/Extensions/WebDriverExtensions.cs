using AutoIFrameSwitcher.Common;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoIFrameSwitcher.Utilities.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement GetWebElementFromIFrame(this IWebDriver @this, By locatorStrategy)
        {
            @this.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(.5);
            var elements = @this.FindElements(locatorStrategy);
            if (elements.Any()) return elements.ElementAt(0);
            var iframeElements = @this.GetElementInIFrames(locatorStrategy);           
            return iframeElements.Any() ? iframeElements.ElementAt(0)
                               : throw new Exception($"No element found in iframe using {locatorStrategy}.");
        }

        public static List<IWebElement> GetWebElementsFromIFrame(this IWebDriver @this, By locatorStrategy)
        {
            @this.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(.5);
            var elements = @this.FindElements(locatorStrategy).ToList();
            if (elements.Any()) return elements;
            var iframeElements = @this.GetElementInIFrames(locatorStrategy);            
            return iframeElements.Any() ? iframeElements
                               : throw new Exception($"No elements found in iframe using {locatorStrategy}.");
        }
        
        private static List<IWebElement> GetElementInIFrames(this IWebDriver @this, By locatorStrategy, int parentIndex = 0)
        {
            if(parentIndex==0)@this.SwitchTo().DefaultContent();  
            var iFrameList = @this.GetFrameElements(parentIndex);

            foreach (var x in iFrameList)
            {
                @this.SwitchTo().Frame(x.Index);             
                var searchedElement = @this.FindElements(locatorStrategy).ToList();
                if (searchedElement.Any())                
                    return searchedElement;                
                @this.SwitchTo().ParentFrame();
            }

            //Element not found.. Check children
            foreach (var x in iFrameList)
            {               
                if (x.HasChildren)
                {
                    @this.SwitchTo().Frame(x.Index);                    
                    var searchedElement = @this.GetElementInIFrames(locatorStrategy, parentIndex + 1);
                    if (searchedElement.Any())                    
                        return searchedElement;
                    @this.SwitchTo().ParentFrame();
                }
            }
            return new List<IWebElement>();            
        }      

        private static List<IFrameElement> GetFrameElements(this IWebDriver @this, int parentIndex)
        {
            var frames = @this.FindElements(By.TagName("iframe"));
            return  frames.TakeWhile(x=> x.Displayed).Select((x, index) => new IFrameElement
            {
                Element = x,
                ElementId = x.GetAttribute("id"),
                Name = (x.GetAttribute("name")) ?? x.GetAttribute("src"),
                Index = index,
                Parent = parentIndex,
                HasChildren = frames.Any()              
            }).ToList();
        }
    }  
}