using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FetchJson : MonoBehaviour
{
    private UnityWebRequest request;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FetchBuildingJson()
    {
        string url = "https://testtest.com";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN_HERE");
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler != null)
                {
                    string text = webRequest.downloadHandler.text;
                }
            }
        }
    }

    IEnumerator PostSignUp(string email, string password)
    {
        string url = "https://testtest.com";
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer YOUR_TOKEN_HERE");
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler != null)
                {
                    string text = webRequest.downloadHandler.text;
                }
            }
        }
    }
}
