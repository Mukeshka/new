using System;
using System.Collections.Generic;

namespace c__test
{
  public class Pattern
  {
    public static string R = "R";
    public static string G = "G";
    public static string B = "B";

  private static Dictionary<string, string> PATTERNS = new Dictionary<string, string>() {
    {"RRR", Pattern.B},
    {"RRRRR",Pattern.B},
    {"RRRRR",Pattern.B},
    {"GGG",Pattern.G},
    {"GGGGG",Pattern.G},
    {"RGBG",Pattern.B}
  };

  public string GetPatternResult(string inputData) {
    string result = string.Empty;
    foreach(KeyValuePair<string, string> entry in PATTERNS)
    {
        // do something with entry.Value or entry.Key
        if(inputData.IndexOf(entry.Key) == 0) 
        {
          result = entry.Value;
        }
    }
    return result;
  }
  }
}