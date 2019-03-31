using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public GameObject winScreen, pauseMenu, gameOverScreen;

    private bool paused = false;

    private void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.H))
            GameOver();
    }

    public void LoadScene(string scene) {
        try {
            Time.timeScale = 1;
            paused = false;
            SceneManager.LoadScene(scene);
        } catch {
            Debug.LogError("Attempting to load invalid scene " + scene);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void OnWin() {
        GetComponent<AudioSource>().Play();
        StartCoroutine(WinScreen(winScreen));
    }

    public void GameOver() {
        StartCoroutine(WinScreen(gameOverScreen));
    }

    private IEnumerator WinScreen(GameObject obj) {
        obj.SetActive(true);
        Image a = obj.GetComponent<Image>();
        Text b = obj.transform.Find("Thanks").GetComponent<Text>();
        Text c = obj.transform.Find("Score").GetComponent<Text>();
        Image d = obj.transform.Find("Button").GetComponent<Image>();
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
        if(paused) {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        } else {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        paused = !paused;
    }

}
