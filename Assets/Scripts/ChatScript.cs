using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatScript : MonoBehaviour
{
    public TextMeshProUGUI[] messages;
    public string[] m_chatText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < messages.Length; i++)
        {
            messages[i].text = m_chatText[i];
        }
    }
}
