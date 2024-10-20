using UnityEngine;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーン間で通知を保持する
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowNotification(string title, string message)
    {
        // Androidネイティブで通知を表示
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject notificationManager = new AndroidJavaObject("com.example.notification.NotificationManager", activity);

            notificationManager.Call("showNotification", title, message);
        }
    }
}
