using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendFinalData : MonoBehaviour {
    PlayerData data;
    public void SendData () {
        StartCoroutine (Upload ());
    }

    IEnumerator Upload () {
        WWWForm form = new WWWForm ();
        string url = "http://localhost:5000/test";
        data = new PlayerData ();
        var jsonData = JsonUtility.ToJson (data);

        var uwr = new UnityWebRequest (url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding ().GetBytes (jsonData);
        uwr.uploadHandler = (UploadHandler) new UploadHandlerRaw (jsonToSend);
        uwr.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer ();
        uwr.SetRequestHeader ("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest ();

        if (uwr.isNetworkError) {
            Debug.Log ("Error While Sending: " + uwr.error);
        } else {
            Debug.Log ("Received: " + uwr.downloadHandler.text);
        }
    }
}