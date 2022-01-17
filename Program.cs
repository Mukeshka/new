using System;
using System.Collections.Generic;
using System.Configuration;

namespace c__test
{
  class Program
  {
    public static bool FIRST_RUN = false;
    static void Main(string[] args)
    {
      RXCE();
    }

    private static void RXCE()
    {
      UI rxceUI = new UI("", "");

      rxceUI.Login();
      rxceUI.NavigateToMainUrl();

      Core coreApp = new Core();
      coreApp.ShowMessage("Application start");
      coreApp.ShowMessage("------------------");

      while (true)
      {
        try
        {
          string currentPattern = rxceUI.GetPattern();
          ColorCode selectedColor = coreApp.PredictColor(currentPattern);
          rxceUI.SelectColor(selectedColor);

          if (FIRST_RUN)
          {
            FIRST_RUN = false;
            Random random = new Random();
            var list = new List<ColorCode> { ColorCode.Blue, ColorCode.Red };
            int randVal = random.Next(list.Count);
            ColorCode randomColor = list[randVal];
            rxceUI.SelectColor(randomColor);
          }
          rxceUI.WaitForNextTick();
        }
        catch (System.Exception ex)
        {
          coreApp.ShowMessage("Main exception: " + ex.Message);
        }
        coreApp.ShowMessage("------------------------------------");
      }
    }
  }
}
