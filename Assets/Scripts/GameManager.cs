using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
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
