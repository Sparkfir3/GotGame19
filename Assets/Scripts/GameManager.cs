using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public GameObject winScreen, pauseMenu;

    private bool paused = false;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.H))
            OnWin();
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

    public void OnWin() {
        StartCoroutine(WinScreen());
    }

    private IEnumerator WinScreen() {
        winScreen.SetActive(true);
        Image a = winScreen.GetComponent<Image>();
        Text b = winScreen.transform.Find("Thanks").GetComponent<Text>();
        Text c = winScreen.transform.Find("Score").GetComponent<Text>();
        Image d = winScreen.transform.Find("Button").GetComponent<Image>();
        Text e = d.transform.Find("Text").GetComponent<Text>();
        while(a.color.a < 225) {
            a.color += new Color32(0, 0, 0, 5);
            b.color += new Color32(0, 0, 0, 5);
            c.color += new Color32(0, 0, 0, 5);
            d.color += new Color32(0, 0, 0, 5);
            e.color += new Color32(0, 0, 0, 5);
            yield return new WaitForSeconds(0.025f);
        }
        yield return null;
    }

    public void Pause() {
        paused = !paused;
        if(paused) {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        } else {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

}
