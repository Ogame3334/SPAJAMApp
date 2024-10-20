using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChatButton : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("Chat");
    }
}
