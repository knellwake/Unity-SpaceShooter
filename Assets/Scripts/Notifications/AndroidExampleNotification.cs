using System;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

using UnityEngine;

public class AndroidExampleNotification : MonoBehaviour
{
    [SerializeField] private int notificationId;
    [SerializeField] private string channelIdExample;

    private void Start()
    {
        Debug.Log(DateTime.Now.ToString());
    }
#if UNITY_ANDROID
    public void NotificationExample(DateTime timeToFire)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = channelIdExample,
            Name = "Example Name",
            Description = "This is just an example",
            Importance = Importance.Default
        };
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification()
        {
            Title = "Where are you?",
            Text = "Come back and play",
            SmallIcon = "default",
            LargeIcon = "default",
            FireTime = timeToFire
        };
        // AndroidNotificationCenter.SendNotification(notification, channelIdExample);
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification,channelIdExample,notificationId);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus == false)
        {
            DateTime whenToFire = DateTime.Now.AddSeconds(10);
            NotificationExample(whenToFire);
        }
        else
        {
            // AndroidNotificationCenter.CancelAllScheduledNotifications();
            AndroidNotificationCenter.CancelScheduledNotification(notificationId);
        }
    }
#endif
}