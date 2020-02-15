using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SendFinalData : MonoBehaviour {
    ScoreInfos data;
    public void SendData () {
        StartCoroutine (Upload ());
    }

    IEnumerator Upload () {
        WWWForm form = new WWWForm ();
        string url = "http://localhost:5000/save-final-info";

        data = new ScoreInfos ();
        data.dateTime = new DateTime ();
        data.gameId = 2020;
        data.team_number = GameMngr.instance.team_number;
        data.team_pwd = GameMngr.instance.team_pwd;
        data.score = GameMngr.instance.final_score;

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