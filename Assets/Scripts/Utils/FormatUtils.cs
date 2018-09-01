using UnityEngine;

public static class FormatUtils
{
  public static string FormatMinutesSeconds(this System.TimeSpan timespan)
  {
    return string.Format("{0}:{1:00}",
      (int) timespan.TotalMinutes,
      timespan.Seconds);
  }
}