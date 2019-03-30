using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    public void LoadScene(string scene) {
        try {
            SceneManager.LoadScene(scene);
        } catch {
            Debug.LogError("Attempting to load invalid scene " + scene);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

}
