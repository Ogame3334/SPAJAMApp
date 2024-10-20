using System.Collections;
using UnityEngine;

public class BluetoothManager : MonoBehaviour
{
    private const int REQUEST_CODE_BLUETOOTH_PERMISSIONS = 1;

    void Start()
    {
        // AndroidのバージョンがS（API 31）以上の場合にBluetoothパーミッションをリクエスト
        if (AndroidVersionIsSOrAbove())
        {
            RequestBluetoothPermissions();
        }

        StartBluetoothScan();
    }

    bool AndroidVersionIsSOrAbove()
    {
        using (AndroidJavaClass versionClass = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            int sdkInt = versionClass.GetStatic<int>("SDK_INT");
            return sdkInt >= 31; // 31はAndroid 12 (S) のAPIレベル
        }
    }

    void RequestBluetoothPermissions()
    {
        using (AndroidJavaObject activity = GetUnityActivity())
        {
            string[] permissions = new string[]
            {
                "android.permission.BLUETOOTH_SCAN",
                "android.permission.BLUETOOTH_CONNECT",
                "android.permission.BLUETOOTH_ADVERTISE"
            };

            using (AndroidJavaObject permissionManager = new AndroidJavaObject("androidx.core.app.ActivityCompat"))
            {
                permissionManager.CallStatic("requestPermissions", activity, permissions, REQUEST_CODE_BLUETOOTH_PERMISSIONS);
            }
        }
    }

    void StartBluetoothScan()
    {
        try
        {
            // Android JavaインターフェースでBluetoothスキャンを開始
            using (AndroidJavaObject activity = GetUnityActivity())
            {
                using (AndroidJavaObject bluetoothManager = new AndroidJavaObject("com.example.bluetooth.BluetoothManager", activity))
                {
                    bluetoothManager.Call("startScan");
                }
            }
        }
        catch (AndroidJavaException e)
        {
            Debug.LogError("Failed to start Bluetooth scan: " + e.Message);
        }
    }

    AndroidJavaObject GetUnityActivity()
    {
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    // UnityとAndroidネイティブ間でのBluetooth検知結果の受け取り
    public void OnDeviceDetected(string deviceName, string deviceAddress)
    {
        Debug.Log("Device Detected: " + deviceName + " - " + deviceAddress);

        // すれ違った端末に基づき通知を表示する処理
        NotificationManager.Instance.ShowNotification("友人を検知しました！", "友人のデバイス: " + deviceName);
    }
}
