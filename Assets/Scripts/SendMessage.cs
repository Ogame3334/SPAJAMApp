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

    public string to;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Send()
    {
        string message = inputField.text;
        fetchJson.PostMessage(message, id, to);
        Debug.Log("Message: " + message);
    }
}
