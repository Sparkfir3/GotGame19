using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {
    
    public enum MenuType { Main, Credits };
    public MenuType currentMenu = MenuType.Main;

    public GameObject mainMenu, creditsMenu, backButton;

    public void SwapMenu() {
        if(currentMenu == MenuType.Main) {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
            backButton.SetActive(true);
            currentMenu = MenuType.Credits;
        } else {
            mainMenu.SetActive(true);
            creditsMenu.SetActive(false);
            backButton.SetActive(false);
            currentMenu = MenuType.Main;
        }
    }

}
