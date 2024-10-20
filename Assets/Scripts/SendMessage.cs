using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendMessage : MonoBehaviour
{
    public FetchJson fetchJson;
    public TMP_InputField inputField;
    public string id;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Send()
    {
        string message = inputField.text;
        fetchJson.PostMessage(message, id);
        Debug.Log("Message: " + message);
    }
}
