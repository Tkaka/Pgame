/*
 * file TimeUtils.cs
 *
 * author: Pengmian
 * date:   2014/09/16 
 */

using System;

/// <summary>
/// 时间戳工具类
/// </summary>
public class TimeUtils
{   
    private static DateTime mEpochLocal = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
    private static DateTime mEpochUtc = TimeZone.CurrentTimeZone.ToUniversalTime(new DateTime(1970, 1, 1));

    // 服务器的时间
    private static long mDeltaTime = 0;
    private static long mOffsetTime = 0;

    /// <summary>
    /// 设置服务器时间，对时
    /// </summary>
    /// <param name="serverTime">服务器时间</param>
    public static void setServerTime(long serverTime)
    {
        mDeltaTime = serverTime;
        mOffsetTime = (Int64)(UnityEngine.Time.realtimeSinceStartup * 1000);
    }

    /// <summary>
    /// Unix时间戳（毫秒）
    /// </summary>
    /// <param name="utc"></param>
    /// <returns></returns>
    public static Int64 currentMilliseconds(bool utc = false, bool clientLocal = false)
    {
        bool serverTime = false;
        if (mDeltaTime != 0)
        {
            if (clientLocal == false)
                serverTime = true;
            else
                serverTime = false;
        }
        else
        {
            serverTime = false;
        }
        if (serverTime)
        {
            return (long)((long)(UnityEngine.Time.realtimeSinceStartup * 1000) + (mDeltaTime - mOffsetTime));
        }
        else
        {
            if(utc)
                return (Int64)(DateTime.Now - mEpochUtc).TotalMilliseconds;
            else
                return (Int64)(DateTime.Now - mEpochLocal).TotalMilliseconds;
        }
    }

    /// <summary>
    /// 服务当前C#时间
    /// </summary>
    public static DateTime currentServerDateTime()
    {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(currentMilliseconds()).AddHours(0);
    }

    /// <summary>
    /// 服务当前C#时间
    ///
    /// </summary>
    public static DateTime currentServerDateTime2()
    {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(currentMilliseconds()).AddHours(8);
    }

    /// <summary>
    /// java时间转C#时间
    /// </summary>
    public static DateTime javaTimeToCSharpTime(long javaMiniSeconds)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddMilliseconds(javaMiniSeconds).AddHours(8);
    }

    /// <summary>
    /// 时间格式化
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string TimeToString(long time)
    {
        return javaTimeToCSharpTime(time).ToString("yy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
    }
    public static string TimeToString(long time, string str)
    {
        return javaTimeToCSharpTime(time).ToString(string.Format(str, "yy","MM", "dd"), System.Globalization.DateTimeFormatInfo.InvariantInfo);
    }

    public static string TimeToStringFormat(long time, string str)
    {
        return javaTimeToCSharpTime(time).ToString(string.Format(str, "yyyy", "MM", "dd"), System.Globalization.DateTimeFormatInfo.InvariantInfo);
    }

    //秒钟转分钟
    public static long SecondsToMin(long seconds)
    {
        return seconds / 60;
    }

    //秒钟转小时
    public static long SecondsToHour(long seconds)
    {
        return seconds / 3600;
    }

    /// <summary>
    /// 分钟转小时
    /// </summary>
    public static long MinToHour(long min)
    {
        return min / 60;
    }

    /// <summary>
    /// 分钟转天数
    /// </summary>
    public static long MinToDay(long min)
    {
        long hour = MinToHour(min);
        return hour / 24;
    }

    /// <summary>
    /// 分钟转天数
    /// </summary>
    /// <param name="hour"></param>
    /// <returns></returns>
    public static long HourToDay(long hour)
    {
        return hour / 24;
    }

    /// <summary>
    /// 分钟转月数
    /// </summary>
    public static long MinToMonth(long min)
    {
        long month = MinToDay(min);
        return month / 30;
    }

    /// <summary>
    /// 计算剩余时间
    /// </summary>
    /// <param name="time">时间</param>
    /// <returns></returns>
    public static long CalculateDelta(long time)
    {
        long delta = TimeUtils.currentMilliseconds() - time;
        return delta;
    }


    //秒转为h:m:s
    public static string FormatTime(int time)
    {
        int hour = time / 3600;
        int min = (time / 60) % 60;
        int sceond = time % 60;
        string sHour, sMin, sSecond;
        if (hour < 10)
            sHour = "0" + hour;
        else if (hour == 0)
            sHour = "00";
        else
            sHour = "" + hour;

        if (min < 10)
            sMin = "0" + min;
        else if (min == 0)
            sMin = "00";
        else
            sMin = "" + min;

        if (sceond < 10)
            sSecond = "0" + sceond;
        else if (sceond == 0)
            sSecond = "00";
        else
            sSecond = "" + sceond;

        return string.Format("{0}:{1}:{2}", sHour, sMin, sSecond);
    }

    //秒转为m:s
    public static string FormatTime2(int time)
    {
        //int hour = time / 3600;
        int min = (time / 60) % 60;
        int sceond = time % 60;
        string sMin, sSecond;
        //if (hour < 10)
        //    sHour = "0" + hour;
        //else if (hour == 0)
        //    sHour = "00";
        //else
        //    sHour = "" + hour;

        if (min < 10)
            sMin = "0" + min;
        else if (min == 0)
            sMin = "00";
        else
            sMin = "" + min;

        if (sceond < 10)
            sSecond = "0" + sceond;
        else if (sceond == 0)
            sSecond = "00";
        else
            sSecond = "" + sceond;

        return string.Format("{0}:{1}", sMin, sSecond);
    }
    /// <summary>
    /// 秒转 HH：mm
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string FormatTime3(int time)
    {
        int hour = time / 3600;
        int min = (time / 60) % 60;
        string sHour, sMin;
        if (hour < 10)
            sHour = "0" + hour;
        else if (hour == 0)
            sHour = "00";
        else
            sHour = "" + hour;

        if (min < 10)
            sMin = "0" + min;
        else if (min == 0)
            sMin = "00";
        else
            sMin = "" + min;

        return string.Format("{0}:{1}", sHour, sMin);
    }

    /// <summary>
    /// 秒转 天：h:m:s
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static string FormatTime4(int time)
    {
        int day = time / (60 * 60 * 24);
        int hour = (time % (60*60*24)) / 3600;
        int min = (time / 60) % 60;
        int sceond = time % 60;
        string sHour, sMin, sSecond;
        if (hour < 10)
            sHour = "0" + hour;
        else if (hour == 0)
            sHour = "00";
        else
            sHour = "" + hour;

        if (min < 10)
            sMin = "0" + min;
        else if (min == 0)
            sMin = "00";
        else
            sMin = "" + min;

        if (sceond < 10)
            sSecond = "0" + sceond;
        else if (sceond == 0)
            sSecond = "00";
        else
            sSecond = "" + sceond;

        return string.Format("{0}天:{1}:{2}:{3}",day, sHour, sMin, sSecond);
    }
}