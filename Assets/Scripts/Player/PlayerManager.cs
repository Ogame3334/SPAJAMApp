using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    // ユーザーが登録した友人のデバイス情報
    private List<string> friendDevices = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterFriendDevice(string deviceAddress)
    {
        if (!friendDevices.Contains(deviceAddress))
        {
            friendDevices.Add(deviceAddress);
            Debug.Log("Device Registered: " + deviceAddress);
        }
    }

    public bool IsFriendDevice(string deviceAddress)
    {
        return friendDevices.Contains(deviceAddress);
    }
}
