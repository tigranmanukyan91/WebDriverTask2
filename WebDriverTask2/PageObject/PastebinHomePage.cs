using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace WebDriverTask2.PageObject
{
	public class PastebinHomePage
	{
		private readonly IWebDriver _driver;
		private readonly WebDriverWait _wait;

		public PastebinHomePage(IWebDriver driver)
		{
			_driver = driver;
			_wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
		}

		private IWebElement NewPasteTextArea => _wait.Until(e=>e.FindElement(By.Id("postform-text")));
		private IWebElement SyntaxHighlightingDropdown => _wait.Until(e => e.FindElement(By.Id("select2-postform-format-container")));
		private IWebElement PasteExpirationDropdown => _wait.Until(e => e.FindElement(By.Id("select2-postform-expiration-container")));
		private IWebElement PasteNameInput => _wait.Until(e => e.FindElement(By.Id("postform-name")));
		private IWebElement CreateNewPasteButton => _wait.Until(e => e.FindElement(By.XPath("//button[text()='Create New Paste']")));

		public void EnterCode(string code)
		{
			NewPasteTextArea.SendKeys(code);
		}

		public void SelectSyntaxHighlighting(string syntax)
		{
			SyntaxHighlightingDropdown.Click();
			_wait.Until(e=>e.FindElement(By.XPath($"//li[text()='{syntax}']"))).Click();
		}

		public void SelectPasteExpiration(string expiration)
		{
			PasteExpirationDropdown.Click();
			_wait.Until(e => e.FindElement(By.XPath($"//li[text()='{expiration}']"))).Click();
		}

		public void EnterPasteName(string name)
		{
			PasteNameInput.SendKeys(name);
		}

		public void CreateNewPaste()
		{
			CreateNewPasteButton.Click();
		}

		public void MoveToElement()
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor) _driver;
			js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
		}
	}
}
