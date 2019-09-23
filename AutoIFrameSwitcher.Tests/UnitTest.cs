using AutoIFrameSwitcher.Utilities.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace AutoIFrameSwitcher.Tests
{
    public class UnitTest
    {
        private IWebDriver _driver;

        [SetUp]
        public void InitializeBrowser()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void IframeSwitchingTest()
        {            
            _driver.Navigate().GoToUrl($@"{AppDomain.CurrentDomain.BaseDirectory}\Data\Html\index.html");
            _driver.Manage().Window.Maximize();
            var element1 = _driver.GetWebElementFromIFrame(By.CssSelector("#Grid1"));
            Assert.IsTrue(element1.GetAttribute("id").Equals("Grid1"));
            var element2 = _driver.GetWebElementFromIFrame(By.CssSelector("#Grid2"));
            Assert.IsTrue(element2.GetAttribute("id").Equals("Grid2"));
            var element3 = _driver.GetWebElementFromIFrame(By.CssSelector("#Grid3"));
            Assert.IsTrue(element3.GetAttribute("id").Equals("Grid3"));
            var elements = _driver.GetWebElementsFromIFrame(By.CssSelector("button"));
            Assert.IsTrue(elements.Count==2);          
        }
               
        [TearDown]
        public void Close()
        {
            _driver.Close();
        }
    }
}
