using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestWeb : MonoBehaviour {

    // Start is called before the first frame update
    void Start () {
        // A correct website page.
        //StartCoroutine (GetRequest ("https://jsonplaceholder.typicode.com/todos/"));
    }

    IEnumerator GetScore (string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get (uri)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest ();

            if (webRequest.isNetworkError) {
                Debug.Log (": Error: " + webRequest.error);
            } else {
                /* TestJson[] test = new TestJson[200];
                 test = JsonUtility.FromJson<TestJson[]> (webRequest.downloadHandler.text);
                 Debug.Log (test[0].id);*/
            }
        }
    }

    //TODO: Post score
}