using AutoIFrameSwitcher.Utilities.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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

            var errorList = new List<string>();
            //verify grids
            for (int i = 1; i <= 5; i++)
            {
                if (!_driver.GetWebElementFromIFrame(By.CssSelector($"#Grid{i}")).GetAttribute("id").Equals($"Grid{i}"))
                    errorList.Add($"Grid with id #Grid{i} was not found.");
            }          
            Assert.IsTrue(errorList.Count == 0, $"{string.Join("\n", errorList.ToArray())}");

            //verify button
            var btn1 = _driver.GetWebElementsFromIFrame(By.CssSelector("button.sosyal"));
            Assert.IsTrue(btn1.Count==2);
            var btn2 = _driver.GetWebElementsFromIFrame(By.CssSelector("button.noclass"));
            Assert.IsTrue(btn2.Count == 2);
        }

        [TearDown]
        public void Close()
        {
            _driver.Close();
        }}
}
