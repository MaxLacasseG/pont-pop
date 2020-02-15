using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class SendFinalData : MonoBehaviour {
    [SerializeField]
    public ScoreInfos data;
    public ScoreInfos response;
    public TMP_Text responseBox;
    public TMP_Text errorBox;

    public string month;
    public string day;
    public string hour;
    public string minute;

    public void SendData () {
        StartCoroutine (Upload ());
    }

    IEnumerator Upload () {
        responseBox.text = "";
        errorBox.text = "";

        WWWForm form = new WWWForm ();
        string url = "http://localhost:5000/save-score";

        data = new ScoreInfos ();
        data.gameId = "2020";
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
            try {
                response = JsonUtility.FromJson<ScoreInfos> (uwr.downloadHandler.text);
                if (response.msg != String.Empty) {
                    errorBox.text = response.msg;
                }

                if (response.attempts != 0) {
                    responseBox.text = "Pointage sauvegardé. Il vous reste " + response.attempts + " essais";
                    DateTime datetime = DateTime.ParseExact (response.dateTime, "YYYY-MM-DD", CultureInfo.InvariantCulture);
                    Debug.Log (response.dateTime);
                }

            } catch (System.Exception err) {
                Debug.Log (err.Message);
            }
        }
    }
}