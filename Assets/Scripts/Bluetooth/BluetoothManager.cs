using System.Collections;
using UnityEngine;
using UnityEngine.Android;

public class BluetoothManager : MonoBehaviour
{
    void Start()
    {
        // Android用Bluetoothパーミッションのリクエスト
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S)
        {
            ActivityCompat.requestPermissions(this,
            arrayOf(
                Manifest.permission.BLUETOOTH_SCAN,
                Manifest.permission.BLUETOOTH_CONNECT,
                Manifest.permission.BLUETOOTH_ADVERTISE),
            REQUEST_CODE_BLUETOOTH_PERMISSIONS
        );
        }
        StartBluetoothScan();
    }

    void StartBluetoothScan()
    {
        // Android JavaインターフェースでBluetoothスキャンを開始
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject bluetoothManager = new AndroidJavaObject("com.example.bluetooth.BluetoothManager", activity);

            bluetoothManager.Call("startScan");
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
