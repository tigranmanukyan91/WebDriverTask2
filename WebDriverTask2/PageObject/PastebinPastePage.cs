using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace WebDriverTask2.PageObject
{
	public class PastebinPastePage
	{
		private IWebDriver _driver;
		private WebDriverWait _wait;

		public PastebinPastePage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		}

		public string GetPageTitle()
		{
			return _driver.Title;
		}

		public string GetSyntaxHighlighting()
		{
			var element = _wait.Until(e=>e.FindElement(By.ClassName("bash")));
			return element.GetAttribute("class");
		}

		public string GetPasteCode()
		{
			return _wait.Until(e=>e.FindElement(By.CssSelector("ol.bash li:nth-of-type(2) div.de1"))).Text;
		}
	}
}

