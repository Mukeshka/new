using System;
using System.Collections.Generic;

namespace c__test
{
  public enum PatternType
  {
    All,
    Low,
    Medium,
    High
  }

  public enum ColorCode
  {
    None,
    Red,
    Green,
    Blue
  }
  public class Core
  {
    private PatternType patternType;

    public Core()
    {
      this.patternType = PatternType.All;
    }
    public Core(PatternType patternType)
    {
      this.patternType = patternType;
    }

    public ColorCode PredictColor(string inputPattern)
    {
      ColorCode colorCode = ColorCode.None;
      var suggestedPatterns = GetSuggestedPatterns();
      foreach (var item in suggestedPatterns)
      {
        string key = item.Key;
        ColorCode value = item.Value;
        if (inputPattern.IndexOf(key) == 0)
        {
          colorCode = value;
        }
      }
      return colorCode;
    }
    public void ShowMessage(string message)
    {
      Console.WriteLine(message);
    }

    private Dictionary<string, ColorCode> GetSuggestedPatterns()
    {
      var dict = new Dictionary<string, ColorCode>();
      switch (this.patternType)
      {
        case PatternType.All:
          dict.Add("RRR", ColorCode.Green);
          dict.Add("GGG", ColorCode.Red);
          dict.Add("RGB", ColorCode.Green);
          dict.Add("RRRR", ColorCode.Green);
          dict.Add("GGGG", ColorCode.Red);
          dict.Add("RRRRRRRR", ColorCode.Green);
          dict.Add("GGGGGGGG", ColorCode.Red);
          break;
        case PatternType.Low:
          dict.Add("RRR", ColorCode.Green);
          dict.Add("GGG", ColorCode.Red);
          dict.Add("RGB", ColorCode.Green);
          break;
        case PatternType.Medium:
          dict.Add("RRRR", ColorCode.Green);
          dict.Add("GGGG", ColorCode.Red);
          break;
        case PatternType.High:
          dict.Add("RRRRRRRR", ColorCode.Green);
          dict.Add("GGGGGGGG", ColorCode.Red);
          break;
      }

      return dict;
    }
  }
}