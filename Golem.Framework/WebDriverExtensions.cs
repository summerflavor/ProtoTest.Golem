﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA;
using System.Drawing;
using System.Collections.ObjectModel;


namespace Golem.Framework
{
    public static class WebDriverExtensions
    {
        public static IWebElement WaitForElement(this IWebDriver driver, By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            return wait.Until<IWebElement>((d) =>
            {
                return d.FindElement(by);
            });
        }

        public static IWebElement FindElementWithText(this IWebDriver driver, string text)
        {
            return driver.FindElement(By.XPath("//*[text()='" + text + "']"));
        }
        public static IWebElement WaitForElementWithText(this IWebDriver driver, string text)
        {
            return driver.WaitForElement(By.XPath("//*[text()='" + text + "']"));
        }
        public static void VerifyElementPresent(this IWebDriver driver, By by, bool isPresent=true)
        {
            int count = driver.FindElements(by).Count;
            Verify(isPresent && count == 0,"VerifyElementPresent Failed : Element : " + by.ToString() +
                                          (isPresent == true ? " found" : " not found"));
        }

        public static void Verify(bool condition, string message)
        {
            if (!condition)
            {
                TestBaseClass.AddVerificationError(message);
            }
            else
            {
                TestContext.CurrentContext.IncrementAssertCount();

            }
        }

        public static void VerifyElementVisible(this IWebDriver driver, By by, bool isVisible = true)
        {
            ReadOnlyCollection<IWebElement> elements = driver.FindElements(by);
            int count = elements.Count;
            bool visible = false;
            if (isVisible && count != 0)
            {
                foreach(IWebElement element in elements)
                {
                    if (element.Displayed)
                        visible = true;
                }
            }
            Verify(isVisible != visible,
                   "VerifyElementVisible Failed : Element : " + by.ToString() +
                   (isVisible == true ? " visible" : " not visible"));

        }

        public static void VerifyElementText(this IWebDriver driver, By by, string expectedText)
        {
            string actualText = driver.FindElement(by).Text;
            Verify(actualText != expectedText,
                   "VerifyElementText Failed : Expected : " + by.ToString() + " Expected text : '" + expectedText +
                   "' + Actual '" + actualText);
        }
        
        public static Image GetScreenshot(this IWebDriver driver)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(ss.AsByteArray);
            return System.Drawing.Image.FromStream(ms);
        }

    }
}