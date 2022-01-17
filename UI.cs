using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace c__test
{
  class UI
  {
    const string LOGIN_URL = "https://rxce.in/#login";
    const string MAIN_PAGE_URL = "https://rxce.in/#/win";
    const string GREEN_CIRCLE_STYLE = "background-color: rgb(0, 230, 118);";
    const string RED_CIRCLE_STYLE = "background-color: rgb(255, 23, 68);";
    const int GREEN_BUTTON_ID = 17;
    const int RED_BUTTON_ID = 19;
    const int BLUE_BUTTON_ID = 0;
    private string username { get; set; }
    private string password { get; set; }

    ChromeOptions options = new ChromeOptions();
    //options.AddArguments("--headless");
    private IWebDriver driver;

    public UI(string username, string password)
    {
      this.username = username;
      this.password = password;
      driver = new ChromeDriver(@"C:\Users\Dee\Downloads\chromedriver_win32", options);
    }
    public void Login()
    {
      driver.Navigate().GoToUrl(LOGIN_URL);

      IWebElement mobile = driver.FindElement(By.TagName(@"input[type=text]"));
      mobile.SendKeys(this.username);
      Sleep(1);
      IWebElement password = driver.FindElement(By.TagName(@"input[type=password]"));
      password.SendKeys(this.password);
      Sleep(1);

      driver.FindElement(By.TagName(@"button[type=submit]")).Click();

      Sleep(2);
    }

    public void NavigateToMainUrl()
    {
      driver.Navigate().GoToUrl(MAIN_PAGE_URL);

      Sleep(2);

      string cUrl = driver.Url;

      if (!cUrl.Contains("Win"))
      {
        driver.Navigate().GoToUrl(MAIN_PAGE_URL);
      }
    }

    public string GetPattern()
    {
      string result = string.Empty;
      try
      {
        result = GetCurrentPattern();
        Console.WriteLine("Pattern Fetched: " + result);
        while (result.Length < 4)
        {
          Console.WriteLine("Retrying pattern fetch...");
          driver.Navigate().Refresh();
          Sleep(3);
          result = GetCurrentPattern();

          if (result.Length > 4) break;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Pattern fetch issue: " + ex.Message);
      }
      Console.WriteLine("Pattern Fetched: " + result);
      return result;
    }

    public void SelectColor(ColorCode colorCode, int count = 1)
    {
      Console.WriteLine("Choosen color: " + colorCode.ToString());

      if(colorCode == ColorCode.None) return;
      
      driver.Navigate().Refresh();
      Sleep(2);
      ReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("button"));

      int colorButtonID = 0;
      switch (colorCode)
      {
        case ColorCode.Red:
          colorButtonID = RED_BUTTON_ID;
          break;
          case ColorCode.Green:
          colorButtonID = GREEN_BUTTON_ID;
          break;
          case ColorCode.Blue:
          colorButtonID = BLUE_BUTTON_ID;
          break;
      }

      WebDriverWait waitEnable = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      bool btnToBeClicked = waitEnable.Until(condition =>
      {
        try
        {
          return elements[colorButtonID].Enabled;
        }
        catch (System.Exception ex)
        {
          System.Console.WriteLine("Buying exception: " + ex.Message);
          return false;
        }
      });

      if (btnToBeClicked)
      {
        elements[colorButtonID].Click();
      }
      else
      {
        Console.WriteLine("Button is disabled");
      }
      Console.WriteLine("Ready to buy");
      Sleep(2);

      IWebElement btnConfirm = driver.FindElement(By.TagName(@"button[type=submit]"));
      btnConfirm.Click();

      Sleep(2);
      driver.Navigate().Refresh();
      System.Console.WriteLine("Buying completed: " + colorCode.ToString());
    }
    private string GetCurrentPattern()
    {
      string currentPattern = string.Empty;
      ReadOnlyCollection<IWebElement> elements = this.driver.FindElements(By.TagName("table"));
      IWebElement table = elements[0];
      var rows = table.FindElements(By.TagName("tr"));
      foreach (var row in rows)
      {
        if (currentPattern.Length == 10) break;
        var rowTds = row.FindElements(By.TagName("td"));
        if (rowTds.Count < 4) continue;
        currentPattern += GetColorFromRow(rowTds[3]);
      }
      return currentPattern;
    }

    private static string GetColorFromRow(IWebElement td)
    {
      var point = td.FindElements(By.ClassName("point"));
      if (point.Count == 1)
      {
        string style = point[0].GetAttribute("style");
        if (style.Equals(GREEN_CIRCLE_STYLE))
        {
          return "G";
        }
        else if (style.Equals(RED_CIRCLE_STYLE))
        {
          return "R";
        }
      }
      else if (point.Count == 2)
      {
        Console.WriteLine("Both color present");
        string res = string.Empty;

        string style = point[0].GetAttribute("style");
        if (style.Contains(GREEN_CIRCLE_STYLE))
        {
          res += "G";
        }
        else if (style.Contains(RED_CIRCLE_STYLE))
        {
          res += "R";
        }
        else
        {
          res += "B";
        }
        return res;
      }
      return string.Empty;
    }

    public IWebDriver GetChromeDriver()
    {
      return this.driver;
    }

    public void Sleep(int timeInSecond)
    {
      Thread.Sleep(1000 * timeInSecond);
    }

    public void WaitForNextTick()
    {
      Console.WriteLine("Waiting for next tick @ " + DateTime.Now.AddMinutes(3).ToLongTimeString());
      this.Sleep(190);
    }
  }
}