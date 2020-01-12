using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMngr : MonoBehaviour {

    void OnEnable () {
        //Register to GameManager
        GameMngr.onGameEnd += Restart;
        GameMngr.onGamePause += Pause;
        Debug.Log ("scene count: " + SceneManager.sceneCountInBuildSettings);
        //SceneManager.LoadScene (2);
    }

    void OnDisable () {
        GameMngr.onGameEnd -= Restart;
        GameMngr.onGamePause -= Pause;
    }

    public void SwitchScene (string sceneName) {
        SceneManager.LoadScene (sceneName);
    }

    public void SwitchScene (int sceneIndex) {
        SceneManager.LoadScene (sceneIndex);
    }

    IEnumerator LoadYourAsyncScene (string sceneName) {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync (sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }
    }

    public void Restart () {
        SwitchScene (3);
    }

    public void Pause () {
        SwitchScene ("OptionMenu");
    }
}