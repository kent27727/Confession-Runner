using com.adjust.sdk;
using Firebase.Analytics;
using GameAnalyticsSDK;
using Moonee.MoonSDK.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;


public static class MoonSDK
{
    public const string Version = "1.1.0";
    public static string token = "";

    /// <summary>
    /// Call this method to track any custom event you want.
    /// </summary>
    /// <param name="eventName">The name of the event to track</param>
    /// <param name="eventProperties">An optional list of properties to send along with the event</param>
    /// <param name="type">type of the event</param>
    /// <param name="analyticsProviders">The list of analytics provider you want to track your custom event to. If this list is null or empty, the event will be tracked in GameAnalytics and Mixpanel (if the user is in a cohort)</param>
    //public static void TrackCustomEvent(string eventName,
    //                                    Dictionary<string, object> eventProperties = null,
    //                                    string type = null,
    //                                    AnalyticsProvider analyticsProviders = AnalyticsProvider.Firebase)
    //{
    //    //AnalyticsManager.TrackCustomEvent(eventName, eventProperties, type, analyticsProviders);

    //    FirebaseAnalytics.LogEvent(eventName, eventProperties);
    //}
    public static void TrackCustomEvent(string eventName, AnalyticsProvider analyticsProviders = AnalyticsProvider.Firebase)
    {
        if (analyticsProviders == AnalyticsProvider.Firebase)
            FirebaseAnalytics.LogEvent(eventName);
        else if (analyticsProviders == AnalyticsProvider.GameAnalytics)
            GameAnalytics.NewDesignEvent(eventName);
    }
    public static void TrackLevelEvents(LevelEvents eventType, int levelIndex)
    {
        string outputValue = "level" + String.Format("{0:D4}", levelIndex);

        if(eventType == LevelEvents.Start)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, outputValue);
        }
        else if(eventType == LevelEvents.Fail)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, outputValue);
        }
        else if (eventType == LevelEvents.Complete)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, outputValue);
        }
    }
    public static void TrackAdjustRevenueEvent(double price)
    {
        MoonSDKSettings settings = MoonSDKSettings.Load();

        AdjustEvent adjustEvent = new AdjustEvent(settings.adjustIAPRevenueToken);
        adjustEvent.setRevenue(0.01, "USD");
        adjustEvent.setTransactionId("transactionId");
        Adjust.trackEvent(adjustEvent);
    }
    public static string UpdateAdjustToken(Moonee.MoonSDK.Internal.MoonSDKSettings settings)
    {
        token = settings.adjustToken;
        return token;
    }
    public static string getToken()
    {
        Moonee.MoonSDK.Internal.MoonSDKSettings settings = Moonee.MoonSDK.Internal.MoonSDKSettings.Load();
        return settings.adjustToken.Replace(" ", string.Empty);

        return "";
    }
    public enum AnalyticsProvider
    {
        Firebase,
        GameAnalytics
    }
    public enum LevelEvents
    {
        Start, 
        Fail,
        Complete
    }
}