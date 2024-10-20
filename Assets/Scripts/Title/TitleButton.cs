using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public void OnStartTap()
    {
        SceneManager.LoadScene("SPAJAMApp");
        GameManager.Instance.GenerateField();
    }
}
