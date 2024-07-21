using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverTask2.PageObject;
using FluentAssertions;
using OpenQA.Selenium.Interactions;

namespace WebDriverTask2.Tests
{
	public class PastebinTests
	{
		private IWebDriver _driver;
		private PastebinHomePage _homePage;
		private PastebinPastePage _pastePage;

		[SetUp]
		public void SetUp()
		{
			_driver = new ChromeDriver();
			_driver.Manage().Window.Maximize();
			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			_driver.Navigate().GoToUrl("https://pastebin.com/");
			_homePage = new PastebinHomePage(_driver);
			_pastePage = new PastebinPastePage(_driver);
		}

		[TearDown]
		public void TearDown()
		{
			_driver.Quit();
			_driver.Dispose();
		}

		[Test]
		public void CreateNewPaste_ShouldCreatePasteWithCorrectDetails()
		{
			string code = "git config --global user.name \"New Sheriff in Town\"\n" +
						  "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\n" +
						  "git push origin master --force";
			string pasteName = "how to gain dominance among developers";
			string paragraph2 = "git reset $(git commit-tree HEAD^{tree} -m \"Legacy code\")\n";

			_homePage.EnterCode(code);
			_homePage.MoveToElement();
			_homePage.SelectSyntaxHighlighting("Bash");
			_homePage.SelectPasteExpiration("10 Minutes");
			_homePage.EnterPasteName(pasteName);
			_homePage.CreateNewPaste();

			string pageTitle = _pastePage.GetPageTitle();
			pageTitle.Should().Contain(pasteName);

			string syntaxHighlightingClass = _pastePage.GetSyntaxHighlighting();
			syntaxHighlightingClass.Should().Contain("bash");

			string pasteCode = _pastePage.GetPasteCode();
			pasteCode.Should().ContainAny(paragraph2);
		}
	}
}
